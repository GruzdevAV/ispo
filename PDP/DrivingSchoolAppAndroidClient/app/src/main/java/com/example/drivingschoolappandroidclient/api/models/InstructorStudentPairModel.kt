package com.example.drivingschoolappandroidclient.api.models

/**
 * Модель для передачи пары значений id ученика и инструктора на сервер
 */
data class InstructorStudentPairModel(
    val instructorId: Int,
    val studentId: Int,
)
