package com.example.drivingschoolappandroidclient.ui.schedules

import android.widget.ArrayAdapter
import androidx.lifecycle.ViewModel
import com.example.drivingschoolappandroidclient.api.models.Instructor

class SchedulesViewModel : ViewModel() {
    val schedulesAdapter = SchedulesAdapter()
}