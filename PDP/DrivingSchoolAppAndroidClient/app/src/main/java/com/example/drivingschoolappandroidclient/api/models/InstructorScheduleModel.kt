package com.example.drivingschoolappandroidclient.api.models

import com.example.drivingschoolappandroidclient.App
import java.sql.Date
import java.time.LocalDate

/**
 * Модель для передачи данных расписания инструктора на сервер
 */
data class InstructorScheduleModel(
    var instructorId: Int,
    var dayOfWorkJson: String,
    var classes: Array<ClassModel>,
){
    var dayOfWork: LocalDate
        get() = App.dateFromString(dayOfWorkJson)
        set(value) {
            dayOfWorkJson = App.dateToString(value)
        }
}
