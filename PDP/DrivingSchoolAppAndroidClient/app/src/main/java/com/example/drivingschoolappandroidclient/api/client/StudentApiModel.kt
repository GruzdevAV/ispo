package com.example.drivingschoolappandroidclient.api.client

import com.example.drivingschoolappandroidclient.api.models.Class
import com.example.drivingschoolappandroidclient.api.models.GradeByInstructorToStudent
import com.example.drivingschoolappandroidclient.api.models.GradeByStudentToInstructor
import com.example.drivingschoolappandroidclient.api.models.GradeByStudentToInstructorModel
import com.example.drivingschoolappandroidclient.api.models.InnerScheduleOfInstructor
import com.example.drivingschoolappandroidclient.api.models.Instructor
import com.example.drivingschoolappandroidclient.api.models.MyResponse
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.Header
import retrofit2.http.POST

interface StudentApiModel {
    @POST("GetMyInstructor")
    fun getMyInstructor(
        @Body studentUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<Instructor>
    @POST("GetClassesOfStudent")
    fun getClassesOfStudent(
        @Body studentUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<List<Class>>
    @POST("GetClassesOfMyInstructor")
    fun getClassesOfMyInstructor(
        @Body studentUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<List<Class>>
    @POST("GetInnerSchedulesOfMyInstructor")
    fun getInnerSchedulesOfMyInstructor(
        @Body studentUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<List<InnerScheduleOfInstructor>>
    @POST("PostGradeToInstructorForClass")
    fun postGradeToInstructorForClass(
        @Body model: GradeByStudentToInstructorModel,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<Any?>>
    @POST("GetGradesToStudent")
    fun getGradesToStudent(
        @Body studentUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<List<GradeByInstructorToStudent>>
    @POST("GetGradesByStudent")
    fun getGradesByStudent(
        @Body studentUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<List<GradeByStudentToInstructor>>

}
