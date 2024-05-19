package com.example.drivingschoolappandroidclient.ui.classes

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import com.example.drivingschoolappandroidclient.App
import com.example.drivingschoolappandroidclient.api.models.Class
import com.example.drivingschoolappandroidclient.api.models.GradesByStudentsToInstructors
import com.example.drivingschoolappandroidclient.api.models.UserRoles
import com.example.drivingschoolappandroidclient.databinding.FragmentPostGradeBinding

class PostGradeFragment : Fragment() {

    private lateinit var binding: FragmentPostGradeBinding
    private lateinit var viewModel: PostGradeViewModel
    private lateinit var `class` : Class
    private var fromWhoId : Int? = null
    private var toWhomId : Int? = null
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val classId = arguments?.getInt("classId")
        `class` = App.classes.value!!.first { it.classId == classId }

        when(App.controller.loginResponse.value?.role){
            UserRoles.student -> {}
            UserRoles.instructor -> {}
        }
        viewModel = ViewModelProvider(this).get(PostGradeViewModel::class.java)
        with(viewModel){
            gradesByStudentAdapter = gradesByStudentAdapter ?: ArrayAdapter(
                requireContext(),
                android.R.layout.simple_spinner_item,
                GradesByStudentsToInstructors.values()
            )
        }
        binding = FragmentPostGradeBinding.inflate(layoutInflater)
        with(binding){
            btnPostGradeCancel.setOnClickListener {

            }
            btnPostGradeOk.setOnClickListener {

            }
            tvPostGradeToWhoText
            return root
        }
    }
}