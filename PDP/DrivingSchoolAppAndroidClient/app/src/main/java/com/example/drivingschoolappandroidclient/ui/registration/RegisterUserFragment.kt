package com.example.drivingschoolappandroidclient.ui.registration

import android.graphics.drawable.BitmapDrawable
import androidx.lifecycle.ViewModelProvider
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import com.example.drivingschoolappandroidclient.App
import com.example.drivingschoolappandroidclient.api.models.Instructor
import com.example.drivingschoolappandroidclient.api.models.RegisterModel
import com.example.drivingschoolappandroidclient.api.models.MyResponse
import com.example.drivingschoolappandroidclient.api.models.Student
import com.example.drivingschoolappandroidclient.databinding.FragmentRegisterUserBinding
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class RegisterUserFragment : Fragment() {

    private lateinit var binding: FragmentRegisterUserBinding
    private lateinit var viewModel: RegisterUserViewModel

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        class RegisterInstructorCallback : Callback<MyResponse<Instructor>> {
            override fun onResponse(p0: Call<MyResponse<Instructor>>, p1: Response<MyResponse<Instructor>>) {
                Toast.makeText(this@RegisterUserFragment.context,p1.body().toString(), Toast.LENGTH_LONG).show()
                App.reloadInstructors()
            }

            override fun onFailure(p0: Call<MyResponse<Instructor>>, p1: Throwable) = App.onFailure(p1)

        }
        binding = FragmentRegisterUserBinding.inflate(layoutInflater)
        viewModel = ViewModelProvider(this)[RegisterUserViewModel::class.java]

        with(binding){

            btnRegister.setOnClickListener {
                val model = RegisterModel(
                    email = etRegEmail.text.toString(),
                    lastName = etRegLastnaem.text.toString(),
                    patronymic = etRegPatronymic.text.toString(),
                    firstName = etRegFirstname.text.toString(),
                    password = etRegPassword.text.toString(),
                    phoneNumber = etRegPhone.text.toString(),
                    profilePictureBytes = null
                )
//                model.profilePicture = (ibRegImage.drawable as BitmapDrawable).bitmap

                if (rbInstructor.isChecked)
                    App.controller.api.registerInstructor(
                        model,App.controller.loginResponse.value!!.authHead
                    ).enqueue(RegisterInstructorCallback())
                else if (rbStudent.isChecked)
                    App.controller.api.registerStudent(
                        model,App.controller.loginResponse.value!!.authHead
                    ).enqueue(RegisterStudentCallback())
            }
            return root
        }
    }

    class RegisterStudentCallback :
        Callback<MyResponse<Student>> {
        override fun onResponse(p0: Call<MyResponse<Student>>, p1: Response<MyResponse<Student>>) {
            App.reloadStudents()
        }

        override fun onFailure(p0: Call<MyResponse<Student>>, p1: Throwable) = App.onFailure(p1)

    }
}