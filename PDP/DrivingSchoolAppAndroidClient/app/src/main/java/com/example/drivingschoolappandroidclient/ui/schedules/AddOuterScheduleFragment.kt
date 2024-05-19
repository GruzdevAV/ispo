package com.example.drivingschoolappandroidclient.ui.schedules

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.fragment.findNavController
import com.example.drivingschoolappandroidclient.App
import com.example.drivingschoolappandroidclient.api.models.AddOuterScheduleModel
import com.example.drivingschoolappandroidclient.databinding.FragmentAddOuterScheduleBinding

class AddOuterScheduleFragment : Fragment() {

    private lateinit var viewModel: AddOuterScheduleViewModel
    private lateinit var binding: FragmentAddOuterScheduleBinding

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentAddOuterScheduleBinding.inflate(layoutInflater)
        viewModel = ViewModelProvider(this).get(AddOuterScheduleViewModel::class.java)
        with(binding){
            btnAddOuterScheduleOk.setOnClickListener {
                App.blocked.value = true
                App.controller.api.addOuterScheduleToMe(
                    AddOuterScheduleModel(
                        googleSheetId = etIdGoogleSheet.text.toString(),
                        googleSheetPageName = etPageName.text.toString(),
                        timesOfClassesRange = etTimeRange.text.toString(),
                        datesOfClassesRange = etDateRange.text.toString(),
                        classesRange = etClassesRange.text.toString(),
                        freeClassExampleRange = etFreeRange.text.toString(),
                        notFreeClassExampleRange = etNotFreeRange.text.toString(),
                        yearRange = etYearRange.text.toString(),
                        userId = App.controller.loginResponse.value!!.id
                    ),
                    App.controller.loginResponse.value?.authHead ?: ""
                ).enqueue(App.AddOuterScheduleCallback())
                findNavController().popBackStack()
            }
            btnAddOuterScheduleCancel.setOnClickListener {
                findNavController().popBackStack()
            }
            return root
        }
    }

}