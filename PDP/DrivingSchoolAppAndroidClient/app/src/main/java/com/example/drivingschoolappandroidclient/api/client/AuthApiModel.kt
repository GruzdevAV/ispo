package com.example.drivingschoolappandroidclient.api.client

import com.example.drivingschoolappandroidclient.api.models.LoginModel
import com.example.drivingschoolappandroidclient.api.models.LoginResponse
import com.example.drivingschoolappandroidclient.api.models.MyResponse
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.POST

interface AuthApiModel {
    @POST("Login")
    fun login(@Body model: LoginModel) : Call<MyResponse<LoginResponse>>
    @POST("Ping")
    fun ping() : Call<MyResponse<Any?>>
}