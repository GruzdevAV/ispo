package com.example.drivingschoolappandroidclient.api.models

/**
 * Модель для передачи данных оценки на сервер
 */
open class GradeByStudentToInstructorModel(
    var classId: Int,
    var grade: Byte,
    var comment: String?,
){
    var gradeEnum : GradesByStudentsToInstructors
        get() = GradesByStudentsToInstructors.ofValue(grade)
        set(value) {
            grade = value.value
        }
}
