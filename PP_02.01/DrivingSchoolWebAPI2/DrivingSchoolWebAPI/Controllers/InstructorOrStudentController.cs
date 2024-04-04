using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using DrivingSchoolAPIModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DrivingSchoolAPIModels.ApiModels;

namespace DrivingSchoolWebAPI.Controllers
{
    /// <summary>
    /// Контроллер для входа в систему
    /// </summary>
    //[Route("api/[controller]")]
    [Authorize(Roles = $"{UserRoles.Student}, {UserRoles.Instructor}")]
    [Route("api")]
    [ApiController]
    public class InstructorOrStudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public InstructorOrStudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Установить занятие
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(SetClass))]
        public async Task<IActionResult> SetClass(ClassStudentPairModel model)
        {
            var @class = await _context.Classes
                .Include(x => x.InnerScheduleOfInstructor)
                .Include(x => x.InnerScheduleOfInstructor.OuterScheduleOfInstructor)
                .Include(x => x.Student)
                .Where(x => x.ClassId == model.ClassId)
                .FirstAsync();
            if (@class == null)
                return NotFound(new Response
                {
                    Message = "Занятие с таким id не найдено",
                    Status = "Failure"
                });
            var student = await _context.Students
                .Where(x => x.StudentId == model.StudentId)
                .FirstAsync();
            if (@class == null)
                return NotFound(new Response
                {
                    Message = "Ученик с таким id не найден",
                    Status = "Failure"
                });
            @class.StudentId = model.StudentId;
            var cl = _context.Classes.Update(@class);
            await _context.SaveChangesAsync();
            @class = cl.Entity;
            if (@class.IsOuterClass)
            {
               bool isMarked = @class.MakeSureOuterMarked(DefaultData.ApiClient);
            }
            return NoContent();
        }
        /// <summary>
        /// Отменить занятие
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(CancelClass))]
        public async Task<IActionResult> CancelClass([FromBody] int classId)
        {
            var @class = await _context.Classes
                .Include(x => x.InnerScheduleOfInstructor)
                .Include(x => x.InnerScheduleOfInstructor.OuterScheduleOfInstructor)
                .Include(x => x.Student)
                .Where(x => x.ClassId == classId)
                .FirstAsync();
            if (@class == null) return NotFound(new Response
            {
                Message = "Занятие с таким id не найдено",
                Status = "Failure"
            });
            @class.StudentId = null;
            //@class.Status = ClassStatus.Отменено;
            var cl = _context.Classes.Update(@class);
            await _context.SaveChangesAsync();
            @class = cl.Entity;
            await _context.SaveChangesAsync();
            if (@class.IsOuterClass)
            {
                bool isMarked = @class.MakeSureOuterMarked(DefaultData.ApiClient);
            }
            return NoContent();
        }
    }
}
