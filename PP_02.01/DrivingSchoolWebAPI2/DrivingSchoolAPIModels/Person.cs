namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Модель данных имени человек
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string? Patronym { get; set; }

    }
}