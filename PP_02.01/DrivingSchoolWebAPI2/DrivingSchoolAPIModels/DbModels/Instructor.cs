using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Инструктор
    /// </summary>
    public class Instructor : Person
    {
        public int InstructorId { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
        public override string ToString()
        {
            return $"{LastName} {FirstName}{(Patronym!=null ? ' '+Patronym:"")}";
        }
    }
}
