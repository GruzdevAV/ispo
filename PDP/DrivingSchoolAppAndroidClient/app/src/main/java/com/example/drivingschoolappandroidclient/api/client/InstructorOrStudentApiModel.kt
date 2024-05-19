package com.example.drivingschoolappandroidclient.api.client

import com.example.drivingschoolappandroidclient.api.models.ClassStudentPairModel
import com.example.drivingschoolappandroidclient.api.models.MyResponse
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.Header
import retrofit2.http.POST

interface InstructorOrStudentApiModel {
    @POST("SetClass")
    fun setClass(
        @Body model: ClassStudentPairModel,
        @Header("Authorization") authHead: String): Call<MyResponse<Any?>>
    @POST("CancelClass")
    fun cancelClass(
        @Body classId: Int,
        @Header("Authorization") authHead: String): Call<MyResponse<Any?>>

}
