package com.example.drivingschoolappandroidclient.api.client

import com.example.drivingschoolappandroidclient.api.models.ApplicationUser
import com.example.drivingschoolappandroidclient.api.models.Class
import com.example.drivingschoolappandroidclient.api.models.EditMeModel
import com.example.drivingschoolappandroidclient.api.models.InnerScheduleOfInstructor
import com.example.drivingschoolappandroidclient.api.models.Instructor
import com.example.drivingschoolappandroidclient.api.models.InstructorRating
import com.example.drivingschoolappandroidclient.api.models.MyResponse
import com.example.drivingschoolappandroidclient.api.models.SetMeImageModel
import com.example.drivingschoolappandroidclient.api.models.Student
import com.example.drivingschoolappandroidclient.api.models.StudentRating
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.Header
import retrofit2.http.POST
import retrofit2.http.Query

interface UserApiModel {
    @GET("GetInstructors")
    fun getInstructors(@Header("Authorization") authHead: String) : Call<List<Instructor>>
    @GET("GetStudents")
    fun getStudents(@Header("Authorization") authHead: String) : Call<List<Student>>
    @GET("GetInstructor")
    fun getInstructor(
        @Query("instructorId") instructorId: Int,
        @Header("Authorization") authHead: String
    ) : Call<Instructor>
    @GET("GetStudent")
    fun getStudent(
        @Query("studentId") studentId: Int,
        @Header("Authorization") authHead: String
    ) : Call<Student>
    @GET("GetClasses")
    fun getClasses(@Header("Authorization") authHead: String) : Call<MyResponse<List<Class>>>
    @GET("GetInnerSchedules")
    fun getInnerSchedules(@Header("Authorization") authHead: String) : Call<List<InnerScheduleOfInstructor>>
    @GET("GetInstructorRatings")
    fun getInstructorRatings(@Header("Authorization") authHead: String) : Call<List<InstructorRating>>
    @POST("GetStudentRatings")
    fun getStudentRatings(@Header("Authorization") authHead: String) : Call<List<StudentRating>>
    @POST("GetStudentRatings")
    fun getMyStudentRatings(
        @Body instructorId: String,
        @Header("Authorization") authHead: String
    ) : Call<List<StudentRating>>
    @POST("GetMe")
    fun getMe(@Body userId: String,@Header("Authorization") authHead: String) : Call<MyResponse<ApplicationUser>>
    @POST("EditMe")
    fun editMe(
        @Body model: EditMeModel,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<Any?>>
    @POST("SetMeImage")
    fun setMeImage(
        @Body model: SetMeImageModel,
        @Header("Authorization") authHead: String
    ): Call<MyResponse<Any?>>



}
