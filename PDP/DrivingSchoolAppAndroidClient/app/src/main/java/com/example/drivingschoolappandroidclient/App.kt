package com.example.drivingschoolappandroidclient

import android.app.Application
import android.content.Context
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.Window
import android.view.WindowManager
import android.widget.FrameLayout
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.MutableLiveData
import com.example.drivingschoolappandroidclient.api.client.Controller
import com.example.drivingschoolappandroidclient.api.models.ApplicationUser
import com.example.drivingschoolappandroidclient.api.models.Class
import com.example.drivingschoolappandroidclient.api.models.GradeByInstructorToStudent
import com.example.drivingschoolappandroidclient.api.models.GradeByStudentToInstructor
import com.example.drivingschoolappandroidclient.api.models.InnerScheduleOfInstructor
import com.example.drivingschoolappandroidclient.api.models.Instructor
import com.example.drivingschoolappandroidclient.api.models.InstructorRating
import com.example.drivingschoolappandroidclient.api.models.LoginResponse
import com.example.drivingschoolappandroidclient.api.models.MyResponse
import com.example.drivingschoolappandroidclient.api.models.Student
import com.example.drivingschoolappandroidclient.api.models.StudentRating
import com.example.drivingschoolappandroidclient.api.models.UserRoles
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import java.sql.Date
import java.time.LocalDate
import java.time.LocalTime
import java.time.format.DateTimeFormatter
import java.util.Calendar

class App : Application() {
    class GetMeCallback: MyCallback<MyResponse<ApplicationUser>>() {
        override fun onResponse(p0: Call<MyResponse<ApplicationUser>>, p1: Response<MyResponse<ApplicationUser>>) {
            Log.d("GetMe","${me.value} $p1")
            me.value = p1.body()?.`package`
            blocked.value = false
        }



    }
    class GetClassesCallback: MyCallback<MyResponse<List<Class>>>() {

        override fun onResponse(p0: Call<MyResponse<List<Class>>>, p1: Response<MyResponse<List<Class>>>) {
            Log.d("GetClasses","${p1.body()?.`package`?.size} $p1")
            classes.value = p1.body()?.`package`
            blocked.value = false
        }

    }

    class GetInnerSchedulesCallback :
        MyCallback<List<InnerScheduleOfInstructor>>() {
        override fun onResponse(
            p0: Call<List<InnerScheduleOfInstructor>>,
            p1: Response<List<InnerScheduleOfInstructor>>
        ) {
            Log.d("GetInnerSchedules","${p1.body()} $p1")
            schedules.value = p1.body()
            blocked.value = false
        }


    }
    companion object{
        fun loginSaveToPreferences(it: LoginResponse?, activity: AppCompatActivity) {
            val preferences = activity.getSharedPreferences("login", Context.MODE_PRIVATE)
            val editor = preferences.edit()
            if(it==null) {
                editor.remove("id")
                    .remove("role")
                    .remove("token")
                    .remove("expiration")
                    .commit()
                return
            }
            editor.putString("id", it.id)
                .putString("role", it.role)
                .putString("token", it.token)
                .putLong("expiration", it.expiration.time)
                .commit()
        }
        private var loadingView: View? = null
        fun showLoadingScreen(window: Window, layoutInflater: LayoutInflater) {
            loadingView = layoutInflater.inflate(R.layout.loading, null)
            window.setFlags(WindowManager.LayoutParams.FLAG_NOT_TOUCHABLE, WindowManager.LayoutParams.FLAG_NOT_TOUCHABLE)
            val rootView = window.decorView.findViewById<FrameLayout>(android.R.id.content)
            rootView.addView(loadingView)
        }

        fun hideLoadingScreen(window: Window) {
            window.clearFlags(WindowManager.LayoutParams.FLAG_NOT_TOUCHABLE)
            val rootView = window.decorView.findViewById<FrameLayout>(android.R.id.content)
            rootView.removeView(loadingView)
            loadingView = null
        }

        fun reloadStudents() {
            CoroutineScope(Dispatchers.IO).launch {
                with(controller){
                    api.getStudents(loginResponse.value!!.authHead).enqueue(GetStudentsCallback())
                    api.getStudentRatings(loginResponse.value!!.authHead).enqueue(GetStudentRatingsCallBack())
                    if(loginResponse.value!!.role == UserRoles.instructor){
                        api.getMyStudents(loginResponse.value!!.id,loginResponse.value!!.authHead).enqueue(GetMyStudentsCallback())
                        api.getMyStudentRatings(loginResponse.value!!.id,loginResponse.value!!.authHead).enqueue(GetMyStudentRatingsCallBack())
                    }
                }
            }
        }

        fun reloadInstructors() {
            CoroutineScope(Dispatchers.IO).launch {
                with(controller){
                    api.getInstructors(loginResponse.value!!.authHead).enqueue(GetInstructorsCallback())
                    api.getInstructorRatings(loginResponse.value!!.authHead).enqueue(GetInstructorRatingsCallback())
                    if(loginResponse.value!!.role == UserRoles.student){
                        api.getMyInstructor(loginResponse.value!!.id,loginResponse.value!!.authHead).enqueue(GetMyInstructorCallback())
                    }
                }
            }
        }
        fun timeFromString(time: String) : LocalTime{
            val str = if(time.indexOfFirst { it==':' }<2)
                "0$time"
            else time
            return LocalTime.parse(str,timeFormat)
        }
        fun dateFromString(date: String) =
            LocalDate.parse(date, dateFormat)
        fun dateToString(date: LocalDate) =
            dateFormat.format(date)
        fun timeToString(time: LocalTime) =
            timeFormat.format(time)
        fun reloadMe() {
            controller.api.getMe(id,authHead).enqueue(GetMeCallback())
        }

        fun reloadClasses() {
            controller.api.getClasses(authHead).enqueue(GetClassesCallback())
            when (controller.loginResponse.value!!.role){
                UserRoles.student ->
                    controller.api.getClassesOfMyInstructor(id, authHead)
                        .enqueue(GetMyClassesCallback())
                UserRoles.instructor ->
                    controller.api.getClassesOfInstructor(id, authHead)
                        .enqueue(GetMyClassesCallback())
            }

        }

        fun reloadInnerSchedules() {
            controller.api.getInnerSchedules(authHead).enqueue(GetInnerSchedulesCallback())
            when (controller.loginResponse.value!!.role){
                UserRoles.student ->
                    controller.api.getInnerSchedulesOfMyInstructor(id, authHead)
                        .enqueue(GetMySchedulesCallback())
                UserRoles.instructor -> reloadMyInnerSchedules()
            }

        }

        fun reloadMyInnerSchedules() {
            controller.api.getMyInnerSchedules(id, authHead).enqueue(GetMySchedulesCallback())
        }

        fun onFailure(p1: Throwable) {
            p1.printStackTrace()
            response.value = MyResponse(
                status = "Возникла ошибка",
                message = p1.message?:"",
                `package` = null
            )
            blocked.value = false
        }

        fun getLoginResponse(activity: AppCompatActivity): LoginResponse? {
            val preferences = activity.getSharedPreferences("login", Context.MODE_PRIVATE)
            val token = preferences.getString("token",null)
            val expiration = Date(preferences.getLong("expiration",-1L))
            val id = preferences.getString("id",null)
            val role = preferences.getString("role",null)
            if(token==null||id==null||role==null||expiration.time < Calendar.getInstance().time.time)
                return null
            return LoginResponse(token,
                expiration,
                role,
                id,
            )
        }

        fun reloadGrades() {
            when (controller.loginResponse.value!!.role){
                UserRoles.student -> {
                    controller.api.getGradesToStudent(id, authHead)
                        .enqueue(GetGradesByInstructorsToStudentCallback())
                    controller.api.getGradesByStudent(id, authHead)
                        .enqueue(GetGradesByStudentsToInstructorCallback())
                }
                UserRoles.instructor -> {
                    controller.api.getGradesByInstructor(id, authHead)
                        .enqueue(GetGradesByInstructorsToStudentCallback())
                    controller.api.getGradesToInstructor(id, authHead)
                        .enqueue(GetGradesByStudentsToInstructorCallback())
                }
            }

        }

        var blocked = MutableLiveData(false)
        var response = MutableLiveData<MyResponse<Any?>>(null)
        val id get() = controller.loginResponse.value!!.id
        val authHead get() = controller.loginResponse.value!!.authHead
        val dateFormat = DateTimeFormatter.ofPattern("dd.MM.yyyy")
        val timeFormat = DateTimeFormatter.ISO_LOCAL_TIME
        lateinit var controller: Controller
        var students : MutableLiveData<List<Student>?> = MutableLiveData(null)
        var studentRatings : MutableLiveData<List<StudentRating>?> = MutableLiveData(null)
        var instructors : MutableLiveData<List<Instructor>?> = MutableLiveData(null)
        var instructorRatings : MutableLiveData<List<InstructorRating>?> = MutableLiveData(null)
        var classes : MutableLiveData<List<Class>?> = MutableLiveData(null)
        var myClasses : MutableLiveData<List<Class>?> = MutableLiveData(null)
        var schedules : MutableLiveData<List<InnerScheduleOfInstructor>?> = MutableLiveData(null)
        var mySchedules : MutableLiveData<List<InnerScheduleOfInstructor>?> = MutableLiveData(null)
        var gradesByInstructorsToStudent : MutableLiveData<List<GradeByInstructorToStudent>?> = MutableLiveData(null)
        var gradesByStudentsToInstructor : MutableLiveData<List<GradeByStudentToInstructor>?> = MutableLiveData(null)
        var myInstructor: MutableLiveData<Instructor?> = MutableLiveData(null)
        var myInstructorRating: MutableLiveData<InstructorRating?> = MutableLiveData(null)
        var myStudents: MutableLiveData<List<Student>?> = MutableLiveData(null)
        var myStudentRatings: MutableLiveData<List<StudentRating>?> = MutableLiveData(null)
        var me : MutableLiveData<ApplicationUser?> = MutableLiveData(null)
    }


    class GetMyInstructorCallback :
        MyCallback<Instructor>() {
        override fun onResponse(p0: Call<Instructor>, p1: Response<Instructor>) {
            Log.d("GetMyInstructor","$myInstructor $p1")
            myInstructor.value = p1.body()
            blocked.value = false
        }



    }

    class GetInstructorRatingsCallback :
        MyCallback<List<InstructorRating>>() {
        override fun onResponse(
            p0: Call<List<InstructorRating>>,
            p1: Response<List<InstructorRating>>
        ) {
            Log.d("GetInstructorRatings","$instructorRatings $p1")
            instructorRatings.value = p1.body()
            if (controller.loginResponse.value?.role == UserRoles.student)
                myInstructorRating.value = instructorRatings.value?.find {
                    it.instructorId == myInstructor.value?.instructorId
                }
            blocked.value = false
        }



    }

    class GetInstructorsCallback :
        MyCallback<List<Instructor>>() {
        override fun onResponse(p0: Call<List<Instructor>>, p1: Response<List<Instructor>>) {
            Log.d("GetInstructors","$instructors $p1")
            instructors.value = p1.body()
            blocked.value = false
        }



    }

    class GetMyStudentRatingsCallBack :
        MyCallback<List<StudentRating>>() {
        override fun onResponse(p0: Call<List<StudentRating>>, p1: Response<List<StudentRating>>) {
            Log.d("GetMyStudentRatings","$myStudentRatings $p1")
            myStudentRatings.value = p1.body()
            blocked.value = false
        }



    }

    class GetMyStudentsCallback :
        MyCallback<List<Student>>() {
        override fun onResponse(p0: Call<List<Student>>, p1: Response<List<Student>>) {
            Log.d("GetMyStudents","$myStudents $p1")
            myStudents.value = p1.body()
            blocked.value = false
        }



    }

    class GetStudentRatingsCallBack :
        MyCallback<List<StudentRating>>() {
        override fun onResponse(p0: Call<List<StudentRating>>, p1: Response<List<StudentRating>>) {
            Log.d("GetStudentRatings","$studentRatings $p1")
            studentRatings.value = p1.body()
            blocked.value = false
        }



    }

    class GetStudentsCallback :
        MyCallback<List<Student>>() {
        override fun onResponse(p0: Call<List<Student>>, p1: Response<List<Student>>) {
            Log.d("GetStudents", "$students $p1")
            students.value = p1.body()
            blocked.value = false
        }



    }

    class SetInstructorToStudentCallback :
        MyCallback<MyResponse<Any?>>() {
        override fun onResponse(
            p0: Call<MyResponse<Any?>>,
            p1: Response<MyResponse<Any?>>
        ) {
            Log.d("SetInstructorToStudent",p1.toString())
            response.value = p1.body()
            blocked.value = false
            reloadStudents()
        }



    }

    class AddOuterScheduleCallback :
        MyCallback<MyResponse<Any?>>() {
        override fun onResponse(
            p0: Call<MyResponse<Any?>>,
            p1: Response<MyResponse<Any?>>
        ) {
            Log.d("AddOuterSchedule",p1.toString())
            reloadInnerSchedules()
            reloadMyInnerSchedules()
            blocked.value = false
        }



    }

    class SetInnerScheduleCallback :
        MyCallback<MyResponse<Any?>>() {
        override fun onResponse(
            p0: Call<MyResponse<Any?>>,
            p1: Response<MyResponse<Any?>>
        ) {
            Log.d("SetInnerSchedule",p1.toString()+'\n'+p1.body())
            reloadInnerSchedules()
            reloadClasses()
            blocked.value = false
        }


    }

    class GetMySchedulesCallback :
        MyCallback<List<InnerScheduleOfInstructor>>() {
        override fun onResponse(
            p0: Call<List<InnerScheduleOfInstructor>>,
            p1: Response<List<InnerScheduleOfInstructor>>
        ) {
            Log.d("GetMySchedules", "$mySchedules $p1")
            mySchedules.value = p1.body()
            blocked.value = false
        }


    }

    class GetMyClassesCallback :
        MyCallback<List<Class>>() {
        override fun onResponse(p0: Call<List<Class>>, p1: Response<List<Class>>) {
            Log.d("GetMyClasses", "$myClasses $p1")
            myClasses.value = p1.body()
            blocked.value = false
        }



    }

    class GetGradesByInstructorsToStudentCallback :
        MyCallback<List<GradeByInstructorToStudent>>() {
        override fun onResponse(
            p0: Call<List<GradeByInstructorToStudent>>,
            p1: Response<List<GradeByInstructorToStudent>>
        ) {
            Log.d("GetGradesByInstructorsToStudent", "$gradesByInstructorsToStudent $p1")
            gradesByInstructorsToStudent.value = p1.body()
            blocked.value = false
        }



    }

    class GetGradesByStudentsToInstructorCallback :
        MyCallback<List<GradeByStudentToInstructor>>() {
        override fun onResponse(
            p0: Call<List<GradeByStudentToInstructor>>,
            p1: Response<List<GradeByStudentToInstructor>>
        ) {
            Log.d("GetGradesByStudentsToInstructor", "$gradesByInstructorsToStudent $p1")
            gradesByStudentsToInstructor.value = p1.body()
            blocked.value = false
        }

    }
    override fun onCreate() {
        super.onCreate()
        controller = Controller()
        controller.start()
    }
    abstract class MyCallback<T> : Callback<T>{
        override fun onFailure(p0: Call<T>, p1: Throwable) = onFailure(p1)
    }

    class SetClassCallback : MyCallback<MyResponse<Any?>>() {
        override fun onResponse(p0: Call<MyResponse<Any?>>, p1: Response<MyResponse<Any?>>) {
            Log.d("SetClass", "$p1")
            reloadClasses()
            blocked.value = false
        }

    }

    class CancelClassCallback : MyCallback<MyResponse<Any?>>() {
        override fun onResponse(p0: Call<MyResponse<Any?>>, p1: Response<MyResponse<Any?>>) {
            Log.d("CancelClass", "$p1")
            reloadClasses()
            blocked.value = false
        }

    }
}