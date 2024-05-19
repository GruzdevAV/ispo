package com.example.drivingschoolappandroidclient.ui.schedules

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.fragment.findNavController
import com.example.drivingschoolappandroidclient.App
import com.example.drivingschoolappandroidclient.api.models.ClassModel
import com.example.drivingschoolappandroidclient.api.models.Instructor
import com.example.drivingschoolappandroidclient.api.models.InstructorScheduleModel
import com.example.drivingschoolappandroidclient.databinding.FragmentAddInnerScheduleBinding
import java.time.LocalDate

class AddInnerScheduleFragment : Fragment() {

    private lateinit var binding: FragmentAddInnerScheduleBinding
    private lateinit var viewModel: AddInnerScheduleViewModel
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        viewModel = ViewModelProvider(this)[AddInnerScheduleViewModel::class.java]
        binding = FragmentAddInnerScheduleBinding.inflate(layoutInflater)
        viewModel.instructorAdapter = viewModel.instructorAdapter ?: ArrayAdapter(
            requireContext(),
            android.R.layout.simple_spinner_item,
            App.instructors.value!!
        )
        with(binding){
            btnInnerCancel.setOnClickListener {
                findNavController().popBackStack()
            }
            App.instructors.observe(viewLifecycleOwner){
                viewModel.instructorAdapter?.notifyDataSetChanged()
            }
            spnInnerInstructors.adapter = viewModel.instructorAdapter
            btnInnerOk.setOnClickListener {
                val instructor = spnInnerInstructors.selectedItem as Instructor
                val classes : MutableList<ClassModel> = mutableListOf()
                if(cbClass1.isChecked) classes.add(ClassModel(
                    null, "09:00", "01:30"
                ))
                if(cbClass2.isChecked) classes.add(ClassModel(
                    null, "10:30", "01:30"
                ))
                if(cbClass3.isChecked) classes.add(ClassModel(
                    null, "12:00", "01:30"
                ))
                if(cbClass4.isChecked) classes.add(ClassModel(
                    null, "13:30", "01:30"
                ))
                if(cbClass5.isChecked) classes.add(ClassModel(
                    null, "15:30", "01:30"
                ))
                if(cbClass6.isChecked) classes.add(ClassModel(
                    null, "17:00", "01:30"
                ))
                if(cbClass7.isChecked) classes.add(ClassModel(
                    null, "18:30", "01:30"
                ))
                App.controller.api.setInnerSchedule(
                    InstructorScheduleModel(
                        instructorId = instructor.instructorId,
                        App.dateToString(LocalDate.of(dpInnerDate.year,dpInnerDate.month+1,dpInnerDate.dayOfMonth)),
                        classes.toTypedArray()
                    ),
                    App.controller.loginResponse.value!!.authHead
                ).enqueue(App.SetInnerScheduleCallback())
                findNavController().popBackStack()
            }
            return root
        }
    }


}