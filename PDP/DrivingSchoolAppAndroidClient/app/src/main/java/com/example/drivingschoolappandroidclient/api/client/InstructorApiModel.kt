package com.example.drivingschoolappandroidclient.api.client

import com.example.drivingschoolappandroidclient.api.models.AddOuterScheduleModel
import com.example.drivingschoolappandroidclient.api.models.Class
import com.example.drivingschoolappandroidclient.api.models.GradeByInstructorToStudent
import com.example.drivingschoolappandroidclient.api.models.GradeByInstructorToStudentModel
import com.example.drivingschoolappandroidclient.api.models.GradeByStudentToInstructor
import com.example.drivingschoolappandroidclient.api.models.InnerScheduleOfInstructor
import com.example.drivingschoolappandroidclient.api.models.MyResponse
import com.example.drivingschoolappandroidclient.api.models.Student
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.Header
import retrofit2.http.POST

interface InstructorApiModel {
    @POST("GetMyStudents")
    fun getMyStudents(
        @Body instructorUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<List<Student>>
    @POST("GetClassesOfInstructor")
    fun getClassesOfInstructor(
        @Body instructorUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<List<Class>>
    @POST("GetMyInnerSchedules")
    fun getMyInnerSchedules(
        @Body instructorUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<List<InnerScheduleOfInstructor>>
    @POST("PostGradeToStudentForClass")
    fun postGradeToStudentForClass(
        @Body model: GradeByInstructorToStudentModel,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<Any?>>
    @POST("GetGradesToInstructor")
    fun getGradesToInstructor(
        @Body instructorUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<List<GradeByStudentToInstructor>>
    @POST("GetGradesByInstructor")
    fun getGradesByInstructor(
        @Body instructorUserId: String,
        @Header("Authorization") authHead: String
    ) : Call<List<GradeByInstructorToStudent>>
    @POST("AddOuterScheduleToMe")
    fun addOuterScheduleToMe(
        @Body model: AddOuterScheduleModel,
        @Header("Authorization") authHead: String
    ) : Call<MyResponse<Any?>>

}
