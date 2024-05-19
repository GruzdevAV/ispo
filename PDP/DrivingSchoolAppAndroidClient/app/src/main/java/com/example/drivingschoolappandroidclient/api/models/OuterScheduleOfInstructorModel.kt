package com.example.drivingschoolappandroidclient.api.models

open class OuterScheduleOfInstructorModel(
    val googleSheetId: String,
    val googleSheetPageName: String,
    val timesOfClassesRange: String,
    val datesOfClassesRange: String,
    val classesRange: String,
    val freeClassExampleRange: String,
    val notFreeClassExampleRange: String?,
    val yearRange: String
)

