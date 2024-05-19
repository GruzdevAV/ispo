package com.example.drivingschoolappandroidclient.api.client

import androidx.lifecycle.MutableLiveData
import com.example.drivingschoolappandroidclient.api.models.LoginResponse
import com.google.gson.Gson
import com.google.gson.GsonBuilder
import okhttp3.OkHttpClient
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory


class Controller {
    lateinit var api : ApiModel
    fun start() {
        val gson: Gson = GsonBuilder()
            .setLenient()
            .setDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'")
            .create()
        val client = OkHttpClient().newBuilder()
            .followRedirects(true)
            .followSslRedirects(true)
            .build()
        val retrofit: Retrofit = Retrofit.Builder()
            .baseUrl(BASE_URL)
            .client(client)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build()
        api = retrofit.create(ApiModel::class.java)
    }
    var loginResponse: MutableLiveData<LoginResponse?> = MutableLiveData(null)
    companion object{
        const val BASE_URL = "https://gruzdevav.somee.com/api/"
    }
}