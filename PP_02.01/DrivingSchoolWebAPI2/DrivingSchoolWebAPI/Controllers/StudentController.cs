using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DrivingSchoolAPIModels;
using Microsoft.EntityFrameworkCore.Internal;

namespace DrivingSchoolWebAPI.Controllers
{
    /// <summary>
    /// Контроллер для учеников
    /// </summary>
    [Authorize(Roles = $"{UserRoles.Student}")]
    [Route("api")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить своего инструктора
        /// </summary>
        /// <param name="studentUserId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(GetMyInstructor))]
        public async Task<ActionResult<Instructor>> GetMyInstructor([FromBody] string studentUserId)
        {
            // Найти ученика по Id
            var student = await _context.Students
                .Where(x => x.User.Id == studentUserId)
                .FirstAsync();
            if (student == null)
                return NotFound(new Response
                {
                    Status = "Failure",
                    Message = "Ученик с таким id не найден"
                });
            if (student.InstructorId == null) return new Instructor();

            // Вернуть инструктора по Id инструктора, указанного у студента
            return await _context.Instructors
                .Where(x => x.InstructorId == student.InstructorId)
                .Include(x => x.User)
                .FirstAsync();
        }
        /// <summary>
        /// Найти свои занятия
        /// </summary>
        /// <param name="studentUserId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(GetClassesOfStudent))]
        public async Task<ActionResult<IEnumerable<Class>>> GetClassesOfStudent([FromBody] string studentUserId)
        {
            var instructor = (await GetMyInstructor(studentUserId)).Value;
            if (instructor == null) return new List<Class>();
            return await _context.Classes
                .Include(x => x.InnerScheduleOfInstructor)
                .Include(x => x.InnerScheduleOfInstructor.OuterScheduleOfInstructor)
                .Include(x => x.InnerScheduleOfInstructor.Instructor)
                .Include(x => x.Student)
                .Where(x => x.InnerScheduleOfInstructor.Instructor.InstructorId == instructor.InstructorId)
                .ToListAsync();
        }
        /// <summary>
        /// Найти занятия своего инструктора
        /// </summary>
        /// <param name="studentUserId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(GetClassesOfMyInstructor))]
        public async Task<ActionResult<IEnumerable<Class>>> GetClassesOfMyInstructor([FromBody] string studentUserId)
        {
            // Найти своего инструктора
            var instructor = await GetMyInstructor(studentUserId);
            if (_context.Classes == null ||
                instructor.Value == null)
                return new List<Class>();
            // Вернуть список занятий инструктора
            return await _context.Classes
                .Include(x => x.Student)
                .Include(x => x.InnerScheduleOfInstructor)
                .Include(x => x.InnerScheduleOfInstructor.OuterScheduleOfInstructor)
                .Include(x => x.InnerScheduleOfInstructor.Instructor)
                .Where(x => x.InnerScheduleOfInstructor.InstructorId == instructor.Value.InstructorId)
                .ToListAsync();
        }
        /// <summary>
        /// Получить расписание своего инструктора
        /// </summary>
        /// <param name="studentUserId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(GetInnerSchedulesOfMyInstructor))]
        public async Task<ActionResult<IEnumerable<InnerScheduleOfInstructor>>> GetInnerSchedulesOfMyInstructor([FromBody] string studentUserId)
        {
            // Найти своего инструктора
            var instructor = await GetMyInstructor(studentUserId);
            if (instructor == null || instructor.Value == null)
                return new List<InnerScheduleOfInstructor>();
            // Вернуть список расписаний инструктора
            return await _context.InnerSchedulesOfInstructors
                .Where(x => x.Instructor.InstructorId == instructor.Value.InstructorId)
                .Include(x => x.Instructor)
                .Include(x => x.OuterScheduleOfInstructor)
                .ToListAsync();
        }
        /// <summary>
        /// Поставить оценку инструктору за занятие
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(PostGradeToInstructorForClass))]
        public async Task<IActionResult> PostGradeToInstructorForClass(GradeByStudentToInstructorModel model)
        {
            var grade = new GradeByStudentToInstructor
            {
                ClassId = model.ClassId,
                Grade = model.Grade,
                Comment = model.Comment
            };
            if (await _context.GradesByStudentToInstructor.AnyAsync(x => x.ClassId == grade.ClassId))
                return BadRequest(new Response
                {
                    Status = "Failed",
                    Message = "Отметка за данное занятие уже была поставлена."
                });
            var @class = await _context.Classes.FindAsync(grade.ClassId);
            if (@class.Status != ClassStatus.Завершено)
                return BadRequest(new Response
                {
                    Status = "Failed",
                    Message = "Студентам нельзя ставить отметку занятию, которое ещё не завершилось или было отменено."
                });
            await _context.GradesByStudentToInstructor.AddAsync(grade);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        /// <summary>
        /// Получить оценки на меня
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(GetGradesToStudent))]
        public async Task<ActionResult<IEnumerable<GradeByInstructorToStudent>>> GetGradesToStudent([FromBody] string studentUserId)
        {
            return await _context.GradesByInstructorToStudent
                .Include(x => x.Class)
                .Include(x => x.Class.Student)
                .Include(x => x.Class.InnerScheduleOfInstructor)
                .Include(x => x.Class.InnerScheduleOfInstructor.Instructor)
                .Include(x => x.Class.InnerScheduleOfInstructor.OuterScheduleOfInstructor)
                .Where(x => x.Class.Student.User.Id == studentUserId)
                .ToListAsync();
        }
        /// <summary>
        /// Получить оценки от меня
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(GetGradesByStudent))]
        public async Task<ActionResult<IEnumerable<GradeByStudentToInstructor>>> GetGradesByStudent([FromBody] string studentUserId)
        {
            return await _context.GradesByStudentToInstructor
                .Include(x => x.Class)
                .Include(x => x.Class.Student)
                .Include(x => x.Class.InnerScheduleOfInstructor)
                .Include(x => x.Class.InnerScheduleOfInstructor.Instructor)
                .Include(x => x.Class.InnerScheduleOfInstructor.OuterScheduleOfInstructor)
                .Where(x => x.Class.Student.User.Id == studentUserId)
                .ToListAsync();
        }
    }
}
