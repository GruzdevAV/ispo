package com.example.drivingschoolappandroidclient.ui.classes

import android.widget.ArrayAdapter
import androidx.lifecycle.ViewModel
import com.example.drivingschoolappandroidclient.api.models.GradeByInstructorToStudent
import com.example.drivingschoolappandroidclient.api.models.GradesByStudentsToInstructors

class PostGradeViewModel : ViewModel() {
    var gradesByStudentAdapter : ArrayAdapter<GradesByStudentsToInstructors>? = null
    var gradeByInstructorAdapter : ArrayAdapter<GradeByInstructorToStudent>? = null
}