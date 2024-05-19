package com.example.drivingschoolappandroidclient.api.models

import java.sql.Date

data class LoginResponse(
    val token: String,
    val expiration: Date,
    val role: String,
    val id: String,
) {
    val authHead get() = "Bearer $token"
}