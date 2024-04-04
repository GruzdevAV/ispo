using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Студент
    /// </summary>
    public class Student : Person
    {
        public int StudentId { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
        public int? InstructorId { get; set; }
        [ForeignKey(nameof(InstructorId))]
        public Instructor? Instructor { get; set; }
        public override string ToString()
        {
            return $"{LastName} {FirstName}{(Patronym != null ? ' ' + Patronym : "")}";
        }
    }
}
