package com.example.drivingschoolappandroidclient.api.models

class StudentRating(
    studentId: Int,
    userId: String,
    user: ApplicationUser,
    var numberOfGrades: Int?,
    var grade: Float?
) : Student(studentId, userId, user) {
}