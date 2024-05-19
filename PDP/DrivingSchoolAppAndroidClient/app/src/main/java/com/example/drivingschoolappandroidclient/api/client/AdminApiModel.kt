package com.example.drivingschoolappandroidclient.api.client

import com.example.drivingschoolappandroidclient.api.models.ApplicationUser
import com.example.drivingschoolappandroidclient.api.models.Instructor
import com.example.drivingschoolappandroidclient.api.models.InstructorScheduleModel
import com.example.drivingschoolappandroidclient.api.models.InstructorStudentPairModel
import com.example.drivingschoolappandroidclient.api.models.MyResponse
import com.example.drivingschoolappandroidclient.api.models.RegisterModel
import com.example.drivingschoolappandroidclient.api.models.Student
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.Header
import retrofit2.http.POST

interface AdminApiModel {
    @GET("GetUsers")
    fun getUsers(@Header("Authorization") authHead: String) : Call<MyResponse<List<ApplicationUser>>>
    @POST("SetInstructorToStudent")
    fun setInstructorToStudent(
        @Body model: InstructorStudentPairModel,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<Any?>>
    @POST("RegisterStudent")
    fun registerStudent(
        @Body model: RegisterModel,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<Student>>
    @POST("RegisterInstructor")
    fun registerInstructor(
        @Body model: RegisterModel,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<Instructor>>
    @POST("SetInnerSchedule")
    fun setInnerSchedule(
        @Body model: InstructorScheduleModel,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<Any?>>

}