using System.ComponentModel.DataAnnotations;

namespace DrivingSchoolAPIModels
{
    public class EditMeModel : Person
    {
        [Phone]
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        public string? EmailAddress { get; set; }
        [Required]
        public string Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string? FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string? Patronym { get; set; }

    }
}
