package com.example.drivingschoolappandroidclient.ui.students

import android.widget.ArrayAdapter
import android.widget.SpinnerAdapter
import androidx.lifecycle.ViewModel
import com.example.drivingschoolappandroidclient.api.models.Instructor

class StudentsViewModel : ViewModel() {
    val studentsAdapter = StudentAdapter()
    var instructorAdapter : ArrayAdapter<Instructor>? = null
}