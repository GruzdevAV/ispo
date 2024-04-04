using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPIModels
{
    public class OuterScheduleOfInstructor : OuterScheduleOfInstructorModel
    {
        /// <summary>
        /// Id внешнего расписания
        /// </summary>
        [Key]
        public int OuterScheduleId { get; set; }

        /// <summary>
        /// Id инструктора, которому принадлежит это внешнее расписание
        /// </summary>
        public int InstructorId { get; set; }
        /// <summary>
        /// Инструктор, которому принадлежит это внешнее расписание
        /// </summary>
        [ForeignKey(nameof(InstructorId))]
        public Instructor Instructor { get; set; }

        //public OuterScheduleOfInstructor(OuterScheduleOfInstructorModel model)
        //{
        //    InstructorId = model.InstructorId;
        //    GoogleSheetId = model.GoogleSheetId;
        //    GoogleSheetPageName = model.GoogleSheetPageName;
        //    TimesOfClassesRange = model.TimesOfClassesRange;
        //    DatesOfClassesRange = model.DatesOfClassesRange;
        //    ClassesRange = model.ClassesRange;
        //    FreeClassExampleRange = model.FreeClassExampleRange;
        //    NotFreeClassExampleRange = model.NotFreeClassExampleRange;
        //    YearRange = model.YearRange;
        //}
        public static OuterScheduleOfInstructor Constructor(OuterScheduleOfInstructorModel model)
        {
            return new OuterScheduleOfInstructor
            {
                GoogleSheetId = model.GoogleSheetId,
                GoogleSheetPageName = model.GoogleSheetPageName,
                TimesOfClassesRange = model.TimesOfClassesRange,
                DatesOfClassesRange = model.DatesOfClassesRange,
                ClassesRange = model.ClassesRange,
                FreeClassExampleRange = model.FreeClassExampleRange,
                NotFreeClassExampleRange = model.NotFreeClassExampleRange,
                YearRange = model.YearRange
            };
    }
        public Dictionary<DateOnly, List<Class>> GetClassesPerDay(GoogleSheetApiClient apiClient)
        {
            var helper = new OuterScheduleHelper
            {
                OuterScheduleOfInstructor = this,
                ApiClient = apiClient,
            };
            var oClasses = helper.OuterSchedule;
            var classesPerDay = new Dictionary<DateOnly, List<Class>>();
            foreach (var @class in oClasses.OuterClasses)
            {
                if (!@class.Value.Free) continue;
                var day = @class.Value.Date.Date;
                if (!classesPerDay.ContainsKey(day)) classesPerDay[day] = new();
                classesPerDay[day].Add(new Class
                {
                    StartTime = @class.Value.Time.Start,
                    Duration = @class.Value.Time.HowLongLasts
                });
            }
            return classesPerDay;
        }
        public bool MarkClass(Student? student, DateOnly date, TimeOnly time, GoogleSheetApiClient apiClient)
        {
            try
            {
                var helper = new OuterScheduleHelper
                {
                    OuterScheduleOfInstructor = this,
                    ApiClient = apiClient,
                };
                var oSchedule = helper.OuterSchedule;
                var mDate = oSchedule.Dates
                    .Where(x => x.Value.Date == date)
                    .Select(x => x.Key)
                    .Last();
                var mTime = oSchedule.Times
                    .Where(x => x.Value.Start == time)
                    .Select(x => x.Key)
                    .Last();
                if (student == null)
                    helper.FreeClass(mDate, mTime);
                else
                    helper.SetClass(mDate, mTime, student!.ToString());
            }
            catch {  return false; }
            return true;
        }
        // TODO: добавить все методы по получению данных, добавлению и проверке внутренних расписаний сюда
    }
}
