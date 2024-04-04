namespace DrivingSchoolAPIModels
{
    public class ClassModel
    {
        public int? StudentId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public int? InstructorId {  get; set; }
        public int? InnerScheduleOfInstructorId { get; set; }
    }
}