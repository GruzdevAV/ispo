using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DrivingSchoolAPIModels;
using System.Diagnostics;
using System.Text;

namespace DrivingSchoolWebAPI.Controllers
{
    /// <summary>
    /// Контроллер для инструктора
    /// </summary>
    [Authorize(Roles = $"{UserRoles.Instructor}")]
    [Route("api")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public InstructorController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Найти всех моих учеников
        /// </summary>
        /// <param name="instructorUserId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(GetMyStudents))]
        public async Task<ActionResult<IEnumerable<Student>>> GetMyStudents([FromBody] string instructorUserId)
        {
            return await _context.Students
                .Where(x => x.Instructor != null && x.Instructor.User.Id == instructorUserId)
                .ToListAsync();
        }
        /// <summary>
        /// Найти все мои занятия
        /// </summary>
        /// <param name="instructorUserId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(GetClassesOfInstructor))]
        public async Task<ActionResult<IEnumerable<Class>>> GetClassesOfInstructor([FromBody] string instructorUserId)
        {
            return await _context.Classes
                .Include(x => x.Student)
                .Include(x => x.InnerScheduleOfInstructor)
                .Include(x => x.InnerScheduleOfInstructor.Instructor)
                .Where(x => x.InnerScheduleOfInstructor.Instructor.User.Id == instructorUserId)
                .ToListAsync();
        }
        /// <summary>
        /// Найти все мои расписания
        /// </summary>
        /// <param name="instructorUserId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(GetMyInnerSchedules))]
        public async Task<ActionResult<IEnumerable<InnerScheduleOfInstructor>>> GetMyInnerSchedules([FromBody] string instructorUserId)
        {
            return await _context.InnerSchedulesOfInstructors
                .Include(x => x.Instructor)
                .Include(x => x.OuterScheduleOfInstructor)
                .Where(x => x.Instructor.User.Id == instructorUserId)
                .ToListAsync();
        }
        /// <summary>
        /// Поставить оценку инструктору за занятие
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(PostGradeToStudentForClass))]
        public async Task<IActionResult> PostGradeToStudentForClass(GradeByInstructorToStudentModel model)
        {
            var grade = new GradeByInstructorToStudent
            {
                ClassId = model.ClassId,
                Grade = model.Grade,
                Comment = model.Comment
            };
            // Сначала отметить, что занятие завершено
            if (await _context.GradesByInstructorToStudent.AnyAsync(x => x.ClassId == grade.ClassId))
                return BadRequest(new Response
                {
                    Status = "Failed",
                    Message = "Отметка за данное занятие уже была поставлена."
                });

            var @class = await _context.Classes.FindAsync(grade.ClassId);
            @class.Status = ClassStatus.Завершено;
            var update = _context.Classes.Update(@class);
            await _context.SaveChangesAsync();
            // А потом добавить оценку
            await _context.GradesByInstructorToStudent.AddAsync(grade);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Получить оценки на меня
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(GetGradesToInstructor))]
        public async Task<ActionResult<IEnumerable<GradeByStudentToInstructor>>> GetGradesToInstructor([FromBody] string instructorUserId)
        {
            return await _context.GradesByStudentToInstructor
                .Include(x => x.Class)
                .Include(x => x.Class.Student)
                .Include(x => x.Class.InnerScheduleOfInstructor)
                .Include(x => x.Class.InnerScheduleOfInstructor.Instructor)
                .Where(x => x.Class.InnerScheduleOfInstructor.Instructor.User.Id == instructorUserId)
                .ToListAsync();
        }
        /// <summary>
        /// Получить оценки от меня
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(GetGradesByInstructor))]
        public async Task<ActionResult<IEnumerable<GradeByInstructorToStudent>>> GetGradesByInstructor([FromBody] string instructorUserId)
        {
            return await _context.GradesByInstructorToStudent
                .Include(x => x.Class)
                .Include(x => x.Class.Student)
                .Include(x => x.Class.InnerScheduleOfInstructor)
                .Include(x => x.Class.InnerScheduleOfInstructor.Instructor)
                .Where(x => x.Class.InnerScheduleOfInstructor.Instructor.User.Id == instructorUserId)
                .ToListAsync();
        }

        [HttpPost]
        [Route(nameof(AddOuterScheduleToMe))]
        public async Task<IActionResult> AddOuterScheduleToMe(AddOuterScheduleModel model)
        {
            var oSchedule = OuterScheduleOfInstructor.Constructor(model);
            oSchedule.InstructorId = await _context.Instructors
                .Where(x => x.UserId == model.UserId)
                .Select(x => x.InstructorId)
                .FirstAsync();
            var osch = await _context.OuterSchedulesOfInstructors.AddAsync(oSchedule);
            await _context.SaveChangesAsync();
            oSchedule = (osch).Entity;
            var classesPerDay = oSchedule.GetClassesPerDay(DefaultData.ApiClient);
            foreach (var day in classesPerDay)
            {
                var iSchedule = new InnerScheduleOfInstructor
                {
                    DayOfWork = day.Key,
                    OuterScheduleId = oSchedule.OuterScheduleId, 
                    InstructorId = oSchedule.InstructorId
                };
                var sch = await _context.InnerSchedulesOfInstructors.AddAsync(iSchedule);
                await _context.SaveChangesAsync();
                iSchedule = sch.Entity;
                //iSchedules.Add(iSchedule);
                //classes.Add(new());
                for (int i = 0; i < day.Value.Count; i++)
                {
                    Class @class = day.Value[i];
                    @class.InnerScheduleOfInstructorId = iSchedule.InnerScheduleOfInstructorId;
                    var cl = await _context.Classes.AddAsync(@class);
                    await _context.SaveChangesAsync();
                    @class = cl.Entity;
                    //classes.Last().Add(@class);
                }
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
