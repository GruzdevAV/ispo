package com.example.drivingschoolappandroidclient.api.models

import com.google.gson.annotations.SerializedName

class GradeByStudentToInstructor(
    classId: Int,
    grade: Byte,
    comment: String?,
    @SerializedName("class")
    val _class: Class
) : GradeByStudentToInstructorModel(classId, grade, comment) {
}