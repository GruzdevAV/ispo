using Microsoft.AspNetCore.Mvc;
using DrivingSchoolAPIModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace DrivingSchoolWebAPI.Controllers
{
    /// <summary>
    /// Контроллер для всех зарегистрированных пользователей
    /// </summary>
    [Authorize]
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить список всех инструкторов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetInstructors))]
        public async Task<ActionResult<IEnumerable<Instructor>>> GetInstructors()
        {
            return await _context.Instructors
                .ToListAsync();
        }
        /// <summary>
        /// Получить список всех студентов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetStudents))]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students
                .Include(x=>x.User)
                .Include(x=>x.Instructor)
                .Select(x=> new Student
                {
                    StudentId = x.StudentId,
                    LastName= x.LastName,
                    FirstName= x.FirstName,
                    Patronym= x.Patronym,
                    InstructorId= x.InstructorId,
                    Instructor= x.Instructor,
                    User= new ApplicationUser
                    {
                        Email=x.User.Email,
                        PhoneNumber=x.User.PhoneNumber
                    }
                })
                .ToListAsync();

        }
        /// <summary>
        /// Получить список всех инструкторов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        [Route(nameof(GetInstructor))]
        public async Task<ActionResult<Instructor>> GetInstructor(int instructorId)
        {
            if (_context.Instructors == null)
                return NotFound();
            return await _context.Instructors
                .Where(x => x.InstructorId == instructorId)
                .FirstAsync();
        }
        /// <summary>
        /// Получить список всех студентов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetStudent))]
        public async Task<ActionResult<Student>> GetStudent(int studentId)
        {
            return await _context.Students
                .Where(x => x.StudentId == studentId)
                .FirstAsync();

        }
        /// <summary>
        /// Получить список всех занятий
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetClasses))]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
        {
            return await _context.Classes
                .Include(x=>x.Student)
                .Include(x=>x.InnerScheduleOfInstructor)
                .Include(x=>x.InnerScheduleOfInstructor.OuterScheduleOfInstructor)
                .Include(x=>x.InnerScheduleOfInstructor.Instructor)
                .ToListAsync();
        }
        /// <summary>
        /// Получить список всех расписаний
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetInnerSchedules))]
        public async Task<ActionResult<IEnumerable<InnerScheduleOfInstructor>>> GetInnerSchedules()
        {
            if (_context.InnerSchedulesOfInstructors == null)
                return NotFound();
            return await _context.InnerSchedulesOfInstructors
                .Include(x=>x.Instructor)
                .ToListAsync();
        }
        /// <summary>
        /// Получить список инструкторов с рейтингом
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetInstructorRatings))]
        public async Task<ActionResult<IEnumerable<InstructorRating>>> GetInstructorRatings()
        {
            // Написал SQL запрос с помощью SQL Management Studio,
            // затем попросил ChatGPT перевести это в LINQ запрос.
            // Потом слегка отредактировал и исправил ошибки.

            var instructorRatings = (from instructor in _context.Instructors
                                     join user in _context.Users on instructor.UserId equals user.Id
                                     join instructorSchedule in _context.InnerSchedulesOfInstructors on instructor.InstructorId equals instructorSchedule.InstructorId into schedules
                                     from schedule in schedules.DefaultIfEmpty()
                                     join classObj in _context.Classes on schedule.InnerScheduleOfInstructorId equals classObj.InnerScheduleOfInstructorId into classes
                                     from classItem in classes.DefaultIfEmpty()
                                     join grade in _context.GradesByStudentToInstructor on classItem.ClassId equals grade.ClassId into grades
                                     from gradeItem in grades.DefaultIfEmpty()
                                     group new { instructor, gradeItem, user } by new
                                     {
                                         instructor.InstructorId,
                                         instructor.UserId,
                                         instructor.FirstName,
                                         instructor.LastName,
                                         instructor.Patronym,
                                         user.Email,
                                         user.PhoneNumber
                                     } into g
                                     select new InstructorRating
                                     {
                                         InstructorId = g.Key.InstructorId,
                                         UserId = g.Key.UserId,
                                         FirstName = g.Key.FirstName,
                                         LastName = g.Key.LastName,
                                         Patronym = g.Key.Patronym,
                                         Grade = g.Average(x => (float?)x.gradeItem.Grade) ?? 0.0f,
                                         NumberOfGrades = g.Count(x => x.gradeItem != null),
                                         User = new ApplicationUser
                                         {
                                             Email = g.Key.Email,
                                             PhoneNumber = g.Key.PhoneNumber
                                         }
                                     });
            if (instructorRatings == null) return NotFound();
            return await instructorRatings
                .ToListAsync();
        }
        /// <summary>
        /// Получить список учеников с рейтингом
        /// </summary>
        /// <param name="instructorId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(GetStudentRatings))]
        public async Task<ActionResult<IEnumerable<StudentRating>>> GetStudentRatings([FromBody] string? instructorId=null)
        {
            // Написал SQL запрос с помощью SQL Management Studio,
            // затем попросил ChatGPT перевести это в LINQ запрос.
            // Потом слегка отредактировал и исправил ошибки.
            var studentRatings = from students in _context.Students 
                                 join user in _context.Users on students.UserId equals user.Id 
                                 join @class in _context.Classes on students.StudentId equals @class.StudentId into studentClasses
                                 from studentClass in studentClasses.DefaultIfEmpty()
                                 join grades in _context.GradesByInstructorToStudent on studentClass.ClassId equals grades.ClassId into studentGrades
                                 from studentGrade in studentGrades.DefaultIfEmpty()
                                 join instructor in _context.Instructors on students.InstructorId equals instructor.InstructorId into studentInstructors
                                 from studentInstructor in studentInstructors.DefaultIfEmpty()
                                 where instructorId == null ||
                                 ( students.Instructor!=null && students.Instructor.UserId == instructorId)
                                 group new { students, studentGrade, user, studentInstructor } by new
                                 {
                                     students.UserId,
                                     students.StudentId,
                                     students.InstructorId,
                                     students.FirstName,
                                     students.LastName,
                                     students.Patronym,
                                     user.Email,
                                     user.PhoneNumber
                                 } into g
                                 select new StudentRating
                                 {
                                     StudentId = g.Key.StudentId,
                                     UserId = g.Key.UserId,
                                     InstructorId = g.Key.InstructorId,
                                     FirstName = g.Key.FirstName,
                                     LastName = g.Key.LastName,
                                     Patronym = g.Key.Patronym,
                                     Grade = g.Average(x => (float?)x.studentGrade.Grade) ?? 0.0F,
                                     NumberOfGrades = g.Count(x => x.studentGrade != null),
                                     User = new ApplicationUser
                                     {
                                         Email = g.Key.Email,
                                         PhoneNumber = g.Key.PhoneNumber
                                     },
                                     Instructor = g.Select(x => x.studentInstructor).FirstOrDefault()
                                 };
            if (studentRatings == null) return NotFound();
            return await studentRatings
                .ToListAsync();
        }
        /// <summary>
        /// Получить данные о себе
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMe")]
        public async Task<ActionResult<object>> GetMe([FromBody] string userId)
        {
            // Найти себя
            var user = await _context.Users.Where(x => x.Id == userId).FirstAsync();
            if (user == null) return NotFound(new Response
            {
                Status = "Failure",
                Message = "Вас нет"
            });
            var roleId = await _context.UserRoles
                .Where(x => x.UserId == userId)
                .Select(x => x.RoleId)
                .FirstAsync();
            // Узнать свою роль
            var role = await _context.Roles
                .Where(x => x.Id == roleId)
                .Select(x => x.Name)
                .FirstAsync();
            // В зависимости от своей роли найти себя точнее
            switch (role)
            {
                case UserRoles.Instructor:
                    var instructor = await _context.Instructors
                        .Where(x => x.UserId == userId)
                        .Include(x => x.User)
                        .FirstAsync();
                    if (instructor != null) return instructor;
                    break;
                case UserRoles.Student:
                    var student = await _context.Students
                        .Where(x => x.UserId == userId)
                        .Include(x => x.User)
                        .FirstAsync();
                    if (student != null) return student;
                    break;
                case UserRoles.Admin: break;
                default:
                    return NotFound(new Response
                    {
                        Status = "Failure",
                        Message = "Вашей роли нет"
                    });
            }
            return user;
        }
        /// <summary>
        /// Изменить данные о себе
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("EditMe")]
        public async Task<IActionResult> EditMe([FromBody] EditMeModel model)
        {
            // Найти себя
            var user = await _context.Users
                .FindAsync(model.Id);
            if (user == null) return NotFound(new Response
            {
                Status="Failure",
                Message="Вас нет"
            });
            if (model.EmailAddress != null) user.Email = model.EmailAddress;
            if (model.PhoneNumber!=null) user.PhoneNumber = model.PhoneNumber;
            var roleId = await _context.UserRoles
                .Where(x => x.UserId == user.Id)
                .Select(x => x.RoleId)
                .FirstAsync();
            // Узнать свою роль
            var role = await _context.Roles
                .Where(x=>x.Id== roleId)
                .Select(x=>x.Name)
                .FirstAsync();
            // В зависимости от своей роли изменить себя
            switch (role)
            {
                case UserRoles.Student:
                    var student = await _context.Students
                        .Where(x => x.UserId == model.Id)
                        .FirstAsync();
                    if (student == null) return NotFound();
                    if (model.LastName != null) student.LastName = model.LastName;
                    if (model.FirstName != null) student.FirstName = model.FirstName;
                    if (model.Patronym != null) student.Patronym = model.Patronym;
                    _context.Students.Update(student);
                    break;
                case UserRoles.Instructor:
                    var instructor = await _context.Instructors
                        .Where(x => x.UserId == model.Id)
                        .FirstAsync();
                    if (instructor == null) return NotFound();
                    if (model.LastName != null) instructor.LastName = model.LastName;
                    if (model.FirstName != null) instructor.FirstName = model.FirstName;
                    if (model.Patronym != null) instructor.Patronym = model.Patronym;
                    _context.Instructors.Update(instructor);
                    break;
                case UserRoles.Admin: break;
                default:
                    return NotFound(new Response
                    {
                        Status = "Failure",
                        Message = "Вашей роли нет"
                    });
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
