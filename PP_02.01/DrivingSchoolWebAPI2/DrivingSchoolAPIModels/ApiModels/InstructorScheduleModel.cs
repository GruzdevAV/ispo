using System.ComponentModel.DataAnnotations;

namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Модель для передачи данных расписания инструктора на сервер
    /// </summary>
    public class InstructorScheduleModel
    {
        [Required]
        public int InstructorId { get; set; }
        [Required]
        public DateOnly DayOfWork { get; set; }
        [Required]
        public ClassModel[] Classes { get; set; }
    }
}
