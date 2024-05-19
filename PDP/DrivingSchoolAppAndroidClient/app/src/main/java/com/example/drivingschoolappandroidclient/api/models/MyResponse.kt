package com.example.drivingschoolappandroidclient.api.models

/// <summary>
/// Модель ответа на запрос, когда нужно передать сообщение
/// </summary>
data class MyResponse<TPackage>(
    val status: String,
    val message: String,
    val `package`: TPackage?
)