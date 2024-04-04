namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Студент с рейтингом
    /// </summary>
    public class StudentRating : Student
    {
        public int NumberOfGrades { get; set; }
        public float Grade { get; set; }
    }
}
