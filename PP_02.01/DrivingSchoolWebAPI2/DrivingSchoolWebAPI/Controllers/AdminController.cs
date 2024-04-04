using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DrivingSchoolAPIModels;
using System.Text;

namespace DrivingSchoolWebAPI.Controllers
{
    /// <summary>
    /// Контроллер для администратора
    /// </summary>
    [Authorize(Roles = $"{UserRoles.Admin}")]
    [Route("api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        /// <summary>
        /// Получить список пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetUsers))]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.ToListAsync();
        }
        /// <summary>
        /// Зарегистрировать ученика
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(RegisterStudent))]
        public async Task<IActionResult> RegisterStudent([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Пользователь уже существует!" });

            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = model.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var sb = new StringBuilder();
                foreach (var err in result.Errors)
                    sb.AppendJoin('\n', err.Description);
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = $"{sb}" });
            }
            await _userManager.AddToRoleAsync(user, UserRoles.Student);
            // Добавление студента в базу данных
            await _context.Students.AddAsync(new Student
            {
                UserId = user.Id,
                User = user,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Patronym = model.Patronym
            });
            await _context.SaveChangesAsync();
            return Ok(new Response { Status = "Success", Message = "Пользователь создан успешно!" });
        }
        /// <summary>
        /// Зарегистрировать инструктора
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(RegisterInstructor))]
        public async Task<IActionResult> RegisterInstructor([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Пользователь уже существует!" });

            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = model.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var sb = new StringBuilder();
                foreach (var err in result.Errors)
                    sb.AppendJoin('\n', err.Description);
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = $"{sb}" });
            }
            await _userManager.AddToRoleAsync(user, UserRoles.Instructor);
            // Добавление инструктора в базу данных
            await _context.Instructors.AddAsync(new Instructor
            {
                UserId = user.Id,
                User = user,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Patronym = model.Patronym
            });
            await _context.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = "Пользователь создан успешно!" });
        }
        /// <summary>
        /// Назначить инструктора ученику
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route(nameof(SetInstructorToStudent))]
        public async Task<IActionResult> SetInstructorToStudent(InstructorStudentPairModel model)
        {
            var student = await _context.Students.FindAsync(model.StudentId);
            var instructor = await _context.Instructors.FindAsync(model.InstructorId);
            if (student == null || instructor == null)
                return NotFound(new Response
                {
                    Status = "Failure",
                    Message = $"Студент или инструктор не найдены."
                });
            student.Instructor = instructor;
            student.InstructorId = model.InstructorId;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        /// <summary>
        /// Установить внутреннее расписание
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(SetInnerSchedule))]
        public async Task<IActionResult> SetInnerSchedule(InstructorScheduleModel model)
        {
            var instructor = await _context.Instructors.FindAsync(model.InstructorId);
            if (instructor == null)
                return NotFound(new Response
                {
                    Status = "Failure",
                    Message = "Инструктор не найден."
                });
            //if (model.DatetimeBeginPlan.Date != model.DatetimeEndPlan.Date)
            //    return BadRequest(new Response
            //    {
            //        Status = "Failure",
            //        Message = "Смена инструктора должны начинаться и оканчиваться в один и тот же день."
            //    });
            //if ((model.DatetimeEndPlan.TimeOfDay - model.DatetimeBeginPlan.TimeOfDay).TotalMinutes < 30)
            //    return BadRequest(new Response
            //    {
            //        Status = "Failure",
            //        Message = "Смена инструктора должна длиться минимум 30 минут."
            //    });
            // TODO: Добавить проверку занятий на корректность
            var schedule = new InnerScheduleOfInstructor
            {
                InstructorId = model.InstructorId,
                DayOfWork = model.DayOfWork,
            };
            // Проверка на наличие расписания у текущего инструктора, которое совпадает дню с новым
            var schedules = await _context.InnerSchedulesOfInstructors
                .Where(x => x.InstructorId == schedule.InstructorId)
                .Where(x => x.DayOfWork == schedule.DayOfWork)
                .ToListAsync();
            // Если есть хоть 1, то это ошибка
            if (schedules.Count>0)
                return BadRequest(new Response
                {
                    Status = "Failure",
                    Message = "У данного инструктора уже есть расписание, занимающее этот день."
                });
            var t = await _context.InnerSchedulesOfInstructors.AddAsync(schedule);
            await _context.SaveChangesAsync();
            schedule = t.Entity;
            var classes = new List<Class>();
            foreach (var c in model.Classes)
            {
                classes.Add(
                    new Class
                    {
                        InnerScheduleOfInstructorId = schedule.InnerScheduleOfInstructorId,
                        StartTime = TimeOnly.FromTimeSpan(c.StartTime),
                        Duration = c.Duration,
                    }
                    );
            }
            await _context.Classes.AddRangeAsync(classes);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
