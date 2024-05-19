package com.example.drivingschoolappandroidclient.api.models

class InstructorRating(
    instructorId: Int,
    userId: String,
    user: ApplicationUser,
    var numberOfGrades: Int,
    var grade: Float
) : Instructor(instructorId, userId, user) {
}