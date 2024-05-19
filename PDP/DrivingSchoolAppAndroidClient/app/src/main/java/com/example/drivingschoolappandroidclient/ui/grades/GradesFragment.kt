package com.example.drivingschoolappandroidclient.ui.grades

import androidx.lifecycle.ViewModelProvider
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.drivingschoolappandroidclient.App
import com.example.drivingschoolappandroidclient.R
import com.example.drivingschoolappandroidclient.api.models.UserRoles
import com.example.drivingschoolappandroidclient.databinding.FragmentGradesBinding

class GradesFragment : Fragment() {

    private lateinit var binding: FragmentGradesBinding
    private lateinit var viewModel: GradesViewModel

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        viewModel = ViewModelProvider(this).get(GradesViewModel::class.java)
        binding = FragmentGradesBinding.inflate(layoutInflater)
        with(binding){
            rvGradesByMe.layoutManager = LinearLayoutManager(
                context, LinearLayoutManager.VERTICAL, false
            )
            rvGradesToMe.layoutManager = LinearLayoutManager(
                context, LinearLayoutManager.VERTICAL, false
            )
            when(App.controller.loginResponse.value!!.role){
                UserRoles.student -> {
                    rvGradesByMe.adapter = viewModel.gradesByStudentsAdapter
                    rvGradesToMe.adapter = viewModel.gradesByInstructorsAdapter
                }
                UserRoles.instructor -> {
                    rvGradesByMe.adapter = viewModel.gradesByInstructorsAdapter
                    rvGradesToMe.adapter = viewModel.gradesByStudentsAdapter
                }
            }
            return root
        }
    }
}