package com.example.drivingschoolappandroidclient.ui.classes

import android.widget.ArrayAdapter
import androidx.lifecycle.ViewModel
import com.example.drivingschoolappandroidclient.api.models.Student

class ClassesViewModel : ViewModel() {
    val classesAdapter = ClassesAdapter()
    var studentAdapter : ArrayAdapter<Student>? = null
}