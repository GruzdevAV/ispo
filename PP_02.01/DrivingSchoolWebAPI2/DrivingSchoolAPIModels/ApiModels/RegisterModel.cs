using System.ComponentModel.DataAnnotations;

namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Модель для регистрации нового пользователя
    /// </summary>
    public class RegisterModel : Person
    {
        /// <summary>
        /// Электронная почта
        /// </summary>
        [EmailAddress]
        [Required(ErrorMessage = "Требуется адрес электронной почты")]
        public string Email { get; set; }
        /// <summary>
        /// Пароль
        /// </summary
        [Required(ErrorMessage = "Требуется пароль")]
        public string Password { get; set; }
        /// <summary>
        /// Номер телефона
        /// </summary>
        [Phone]
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        [Required(ErrorMessage = "Требуется имя")]
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        [Required(ErrorMessage = "Требуется фамилия")]
        public string LastName { get; set; }

    }
}
