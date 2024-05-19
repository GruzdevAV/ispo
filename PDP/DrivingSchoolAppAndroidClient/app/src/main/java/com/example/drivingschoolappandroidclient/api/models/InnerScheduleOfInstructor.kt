package com.example.drivingschoolappandroidclient.api.models

import com.example.drivingschoolappandroidclient.App
import java.sql.Date
import java.time.LocalDate

data class InnerScheduleOfInstructor(
var innerScheduleOfInstructorId : Int,
var instructorId : Int,
var instructor : Instructor,
var dayOfWorkJson : String,
var outerScheduleId : Int?,
var outerScheduleOfInstructor : OuterScheduleOfInstructor?,

){
    var dayOfWork : LocalDate
        get() = App.dateFromString(dayOfWorkJson)
        set(value) {
            dayOfWorkJson = App.dateToString(value)
        }
    val IsOuterSchedule = outerScheduleId != null
}
