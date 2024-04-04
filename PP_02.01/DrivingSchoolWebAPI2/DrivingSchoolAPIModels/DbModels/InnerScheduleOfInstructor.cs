using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPIModels
{
    public class InnerScheduleOfInstructor
    {
        [Key]
        public int InnerScheduleOfInstructorId { get; set; }
        public int InstructorId { get; set; }
        [ForeignKey(nameof(InstructorId))]
        public Instructor Instructor { get; set; }
        public DateOnly DayOfWork { get; set; }
        public int? OuterScheduleId { get; set; }
        [ForeignKey(nameof(OuterScheduleId))]
        public OuterScheduleOfInstructor? OuterScheduleOfInstructor { get; set; }
        public bool IsOuterSchedule => OuterScheduleId != null;
    }
}
