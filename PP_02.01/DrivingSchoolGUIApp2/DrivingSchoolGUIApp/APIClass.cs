using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using DrivingSchoolAPIModels;
using DrivingSchoolAPIModels.ApiModels;
using Newtonsoft.Json;

namespace DrivingSchoolGUIApp
{
    /// <summary>
    /// Класс для работы с моим веб-API
    /// </summary>
    static class APIClass
    {
        public const string url = "https://localhost:7017/api/";
        public static LoginResponse LoginResponse { get; set; }
        public static HttpClient Client { get; set; } = new HttpClient();
        static APIClass()
        {
            Client.BaseAddress = new Uri(url);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public static async Task<string?> GetErrorsFromContent(HttpContent content)
        {
            if (content != null) return GetErrorsFromContent(await content.ReadAsStringAsync());
            return null;
        }
        public static string? GetErrorsFromContent(string content)
        {
            string? message = null;
            try
            {
            var table = JsonConvert.DeserializeAnonymousType(content, new { errors = new Hashtable() });
            if (table?.errors?.Count > 0)
            {
                var enumerator = table.errors.GetEnumerator();
                var stringBuilder = new StringBuilder();
                while (enumerator.MoveNext())
                    stringBuilder.Append($"{enumerator.Key} : {enumerator.Value}\n");
                message = stringBuilder.ToString();
            }
            // Если ↑ дало null, то взять значение отсюда ↓
            message ??= JsonConvert.DeserializeObject<Response>(content)?.Message;

            }
            catch (Exception ex)
            {
                message = $"Error while parsing an error: {ex.Message}";
            }
            return message;
        }
        #region Authenticate
        public static async Task<Response> Login(LoginModel model)
        {
            try
            {
                var response = await Client.PostAsJsonAsync(nameof(Login), model);

                var contentString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var message = GetErrorsFromContent(contentString);

                    return new Response
                    {
                        Status = $"{response.StatusCode}",
                        Message = message
                    };
                }
                LoginResponse = JsonConvert.DeserializeObject<LoginResponse>(contentString);
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginResponse.Token);
                return new Response
                {
                    Status = $"{HttpStatusCode.OK}",
                    Message = "Вход успешен"
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Status = "Вызвано исключение",
                    Message = ex.Message
                };
            }
        }
        public static async Task<bool> Ping()
        {
            try
            {
                var res = await Client.PostAsync(nameof(Ping), null);
                return res.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }

        }
        #endregion
        #region Instructor
        public static async Task<List<Student>> GetMyStudents()
        {
            var response = await Client.PostAsJsonAsync("GetMyStudents", LoginResponse.Id);
            if (!response.IsSuccessStatusCode)
                return null;
            return await response.Content.ReadFromJsonAsync<List<Student>>();
        }
        public static async Task<List<Class>> GetClassesOfInstructor()
        {
            var response = await Client.PostAsJsonAsync("GetClassesOfInstructor", LoginResponse.Id);
            if (!response.IsSuccessStatusCode)
                return null;
            return await response.Content.ReadFromJsonAsync<List<Class>>();
        }
        public static async Task<List<InnerScheduleOfInstructor>> GetMyInnerSchedules()
        {
            var response = await Client.PostAsJsonAsync(nameof(GetMyInnerSchedules), LoginResponse.Id);
            if (!response.IsSuccessStatusCode)
                return null;
            return await response.Content.ReadFromJsonAsync<List<InnerScheduleOfInstructor>>();
        }
        public static async Task<HttpResponseMessage> PostGradeToStudentForClass(GradeByInstructorToStudentModel grade)
        {
            return await Client.PostAsJsonAsync("PostGradeToStudentForClass", grade);
        }
        public static async Task<List<GradeByStudentToInstructor>> GetGradesToInstructor()
        {
            var response = await Client.PostAsJsonAsync("GetGradesToInstructor", LoginResponse.Id);
            if (!response.IsSuccessStatusCode)
                return null;
            return await response.Content.ReadFromJsonAsync<List<GradeByStudentToInstructor>>();
        }
        public static async Task<List<GradeByInstructorToStudent>> GetGradesByInstructor()
        {
            var response = await Client.PostAsJsonAsync("GetGradesByInstructor", LoginResponse.Id);
            if (!response.IsSuccessStatusCode)
                return null;
            return await response.Content.ReadFromJsonAsync<List<GradeByInstructorToStudent>>();
        }
        public static async Task<HttpResponseMessage> AddOuterScheduleToMe(AddOuterScheduleModel model)
        {
            model.UserId = LoginResponse.Id;
            return await Client.PostAsJsonAsync(nameof(AddOuterScheduleToMe), model);
        }

        #endregion
        #region InstructorOrStudent
        public static async Task<HttpResponseMessage> SetClass(ClassStudentPairModel model)
        {
            return await Client.PostAsJsonAsync(nameof(SetClass), model);
        }
        public static async Task<HttpResponseMessage> CancelClass(int classId)
        {
            return await Client.PostAsJsonAsync(nameof(CancelClass), classId);
        }
        #endregion
        #region Student 
        public static async Task<Instructor> GetMyInstructor()
        {
            var response = await Client.PostAsJsonAsync("GetMyInstructor", LoginResponse.Id);
            if (!response.IsSuccessStatusCode)
                throw new Exception(await GetErrorsFromContent(response.Content));
            return await response.Content.ReadFromJsonAsync<Instructor>();

        }
        public static async Task<List<Class>> GetClassesOfStudent()
        {
            var response = await Client.PostAsJsonAsync(nameof(GetClassesOfStudent), LoginResponse.Id);
            if (!response.IsSuccessStatusCode)
                throw new Exception(await GetErrorsFromContent(response.Content));
            return await response.Content.ReadFromJsonAsync<List<Class>>();
        }
        public static async Task<List<Class>> GetClassesOfMyInstructor()
        {
            var response = await Client.PostAsJsonAsync("GetClassesOfMyInstructor", LoginResponse.Id);
            if (!response.IsSuccessStatusCode)
                throw new Exception(await GetErrorsFromContent(response.Content));
            return await response.Content.ReadFromJsonAsync<List<Class>>();
        }
        public static async Task<List<InnerScheduleOfInstructor>> GetInnerSchedulesOfMyInstructor()
        {
            var response = await Client.PostAsJsonAsync(nameof(GetInnerSchedulesOfMyInstructor), LoginResponse.Id);
            if (!response.IsSuccessStatusCode)
                throw new Exception(await GetErrorsFromContent(response.Content));
            return await response.Content.ReadFromJsonAsync<List<InnerScheduleOfInstructor>>();
        }
        public static async Task<List<InnerScheduleOfInstructor>> GetInnerSchedulesOfStudentsInstructor(string studentId)
        {
            var response = await Client.PostAsJsonAsync(nameof(GetInnerSchedulesOfMyInstructor), studentId);
            if (!response.IsSuccessStatusCode)
                throw new Exception(await GetErrorsFromContent(response.Content));
            return await response.Content.ReadFromJsonAsync<List<InnerScheduleOfInstructor>>();
        }
        public static async Task<HttpResponseMessage> PostGradeToInstructorForClass(GradeByStudentToInstructorModel grade)
        {
            return await Client.PostAsJsonAsync("PostGradeToInstructorForClass", grade);
        }
        public static async Task<List<GradeByInstructorToStudent>> GetGradesToStudent()
        {
            var response = await Client.PostAsJsonAsync("GetGradesToStudent", LoginResponse.Id);
            if (!response.IsSuccessStatusCode)
                throw new Exception(await GetErrorsFromContent(response.Content));
            return await response.Content.ReadFromJsonAsync<List<GradeByInstructorToStudent>>();
        }
        public static async Task<List<GradeByStudentToInstructor>> GetGradesByStudent()
        {
            var response = await Client.PostAsJsonAsync("GetGradesByStudent", LoginResponse.Id);
            if (!response.IsSuccessStatusCode)
                throw new Exception(await GetErrorsFromContent(response.Content));
            return await response.Content.ReadFromJsonAsync<List<GradeByStudentToInstructor>>();
        }
        #endregion
        #region User
        public static async Task<List<Instructor>> GetInstructors()
        {
            var response = await Client.GetAsync("GetInstructors");
            if (!response.IsSuccessStatusCode)
                throw new Exception(await GetErrorsFromContent(response.Content));
            return await response.Content.ReadFromJsonAsync<List<Instructor>>();
        }
        public static async Task<List<Student>> GetStudents()
        {
            var response = await Client.GetAsync("GetStudents");
            if (!response.IsSuccessStatusCode)
                throw new Exception(await GetErrorsFromContent(response.Content));
            return await response.Content.ReadFromJsonAsync<List<Student>>();
        }
        public static async Task<Instructor> GetInstructor(int instructorId)
        {
            var response = await Client.GetAsync($"GetInstructor?instructorId={instructorId}");
            if (!response.IsSuccessStatusCode)
                throw new Exception(await GetErrorsFromContent(response.Content));
            return await response.Content.ReadFromJsonAsync<Instructor>();
        }
        public static async Task<Student> GetStudent(int studentId)
        {
            var response = await Client.GetAsync($"GetInstructor?studentId={studentId}");
            if (!response.IsSuccessStatusCode)
                throw new Exception(await GetErrorsFromContent(response.Content));
            return await response.Content.ReadFromJsonAsync<Student>();
        }
        public static async Task<List<Class>> GetClasses()
        {
            var response = await Client.GetAsync("GetClasses");
            if (!response.IsSuccessStatusCode)
                throw new Exception(await GetErrorsFromContent(response.Content));
            return await response.Content.ReadFromJsonAsync<List<Class>>();
        }
        public static async Task<List<InnerScheduleOfInstructor>> GetInnerSchedules()
        {
            var response = await Client.GetAsync(nameof(GetInnerSchedules));
            if (!response.IsSuccessStatusCode)
                throw new Exception(await GetErrorsFromContent(response.Content));
            return await response.Content.ReadFromJsonAsync<List<InnerScheduleOfInstructor>>();
        }
        public static async Task<List<InstructorRating>> GetInstructorRatings()
        {
            var response = await Client.GetAsync("GetInstructorRatings");
            if (!response.IsSuccessStatusCode)
                throw new Exception(await GetErrorsFromContent(response.Content));
            return await response.Content.ReadFromJsonAsync<List<InstructorRating>>();
        }
        public static async Task<List<StudentRating>> GetStudentRatings()
        {
            var response = await Client.PostAsync("GetStudentRatings", null);
            if (!response.IsSuccessStatusCode)
                throw new Exception(await GetErrorsFromContent(response.Content));
            return await response.Content.ReadFromJsonAsync<List<StudentRating>>();
        }
        public static async Task<List<StudentRating>> GetMyStudentsRatings()
        {
            var response = await Client.PostAsJsonAsync("GetStudentRatings", LoginResponse.Id);
            if (!response.IsSuccessStatusCode)
                throw new Exception(await GetErrorsFromContent(response.Content));
            return await response.Content.ReadFromJsonAsync<List<StudentRating>>();
        }
        public static async Task<object> GetMe()
        {
            var response = await Client.PostAsJsonAsync("GetMe", LoginResponse.Id);
            if (!response.IsSuccessStatusCode)
                throw new Exception(await GetErrorsFromContent(response.Content));
            switch (LoginResponse.Role)
            {
                case UserRoles.Admin:
                    return await response.Content.ReadFromJsonAsync<ApplicationUser>();
                case UserRoles.Student:
                    return await response.Content.ReadFromJsonAsync<Student>();
                case UserRoles.Instructor:
                    return await response.Content.ReadFromJsonAsync<Instructor>();
            }
            return await response.Content.ReadAsStringAsync();
        }
        public static async Task<HttpResponseMessage> EditMe(EditMeModel model)
        {
            return await Client.PatchAsJsonAsync(nameof(EditMe), model);
        }
        #endregion
        #region Admin
        public static async Task<List<ApplicationUser>> GetUsers()
        {
            var response = await Client.GetAsync(nameof(GetUsers));
            if (!response.IsSuccessStatusCode)
                throw new Exception(await GetErrorsFromContent(response.Content));
            return await response.Content.ReadFromJsonAsync<List<ApplicationUser>>();
        }
        public static async Task<HttpResponseMessage> RegisterStudent(RegisterModel model)
        {
            return await Client.PostAsJsonAsync(nameof(RegisterStudent), model);
        }
        public static async Task<HttpResponseMessage> RegisterInstructor(RegisterModel model)
        {
            return await Client.PostAsJsonAsync(nameof(RegisterInstructor), model);
        }
        public static async Task<HttpResponseMessage> SetInstructorToStudent(InstructorStudentPairModel model)
        {
            return await Client.PatchAsJsonAsync(nameof(SetInstructorToStudent), model);
        }
        public static async Task<HttpResponseMessage> SetInnerSchedule(InstructorScheduleModel scheduleModel)
        {
            return await Client.PostAsJsonAsync(nameof(SetInnerSchedule), scheduleModel);
        }
        #endregion
        // Удалить из времени секунды
        public static DateTime RemoveSeconds(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0, dt.Kind);
        }
    }
}
