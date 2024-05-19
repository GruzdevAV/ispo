package com.example.drivingschoolappandroidclient.ui.login

import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.example.drivingschoolappandroidclient.App
import com.example.drivingschoolappandroidclient.NavigationDrawerActivity
import com.example.drivingschoolappandroidclient.api.models.LoginModel
import com.example.drivingschoolappandroidclient.api.models.LoginResponse
import com.example.drivingschoolappandroidclient.api.models.MyResponse
import com.example.drivingschoolappandroidclient.databinding.LoginActivityBinding
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import retrofit2.Call
import retrofit2.Response


class LoginActivity : AppCompatActivity(){

    private lateinit var binding: LoginActivityBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = LoginActivityBinding.inflate(layoutInflater)
        App.getLoginResponse(this)?.let{
            login(it)
            return
        }
        with(binding){
            setContentView(root)
            App.blocked.observe(this@LoginActivity){
                if(it){
                    App.showLoadingScreen(window,layoutInflater)
                } else {
                    App.hideLoadingScreen(window)
                }
            }
            btnLogin.setOnClickListener {
                App.blocked.value=true
                CoroutineScope(Dispatchers.IO).launch {
                    val model = LoginModel(
                        email = etEmail.text.toString(),
                        password = etPassword.text.toString()
                    )
                    App.controller.api.login(model).enqueue(object:
                        App.MyCallback<MyResponse<LoginResponse>>() {
                        override fun onResponse(
                            p0: Call<MyResponse<LoginResponse>>,
                            p1: Response<MyResponse<LoginResponse>>
                        ) {
                            val response = p1.body()
                            App.blocked.value = false
                            if(response==null){
                                Toast.makeText(
                                    this@LoginActivity,
                                    "Ошибка входа",
                                    Toast.LENGTH_LONG
                                ).show()
                                return
                            }
                            login(response.`package`!!)
                        }
                    })
                }
            }
        }
    }

    override fun onDestroy() {
        super.onDestroy()
    }
    private fun login(loginResponse: LoginResponse) {
        App.controller.loginResponse.value = loginResponse
        App.loginSaveToPreferences(loginResponse, this)
        val intent = Intent(this@LoginActivity,NavigationDrawerActivity::class.java)
        startActivity(intent)
        finish()
    }
}