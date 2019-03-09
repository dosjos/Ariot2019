package com.visma.autoexcavator

import android.content.Context
import android.net.Uri
import android.os.AsyncTask
import android.os.Bundle
import android.support.v4.app.Fragment
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import kotlinx.android.synthetic.main.fragment_control.view.*
import java.net.URL


class ControlFragment : Fragment() {
    private var listener: OnFragmentInteractionListener? = null


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        val view =  inflater.inflate(R.layout.fragment_control, container, false)
        initListeners(view)
        return view
    }

    // TODO: Rename method, update argument and hook method into UI event
    fun onButtonPressed(uri: Uri) {
        listener?.onFragmentInteraction(uri)
    }

    override fun onAttach(context: Context) {
        super.onAttach(context)
        if (context is OnFragmentInteractionListener) {
            listener = context
        } else {
            throw RuntimeException(context.toString() + " must implement OnFragmentInteractionListener")
        }
    }

    override fun onDetach() {
        super.onDetach()
        listener = null
    }

    fun initListeners(view: View) {
        view.buttonDown.setOnClickListener {
            makeRequest("backwards")
        }
        view.buttonUp.setOnClickListener {
            makeRequest("forward")
        }
        view.buttonLeft.setOnClickListener {
            makeRequest("left")
        }
        view.buttonRight.setOnClickListener {
            makeRequest("right")
        }
        view.buttonStop.setOnClickListener {
            makeRequest("stop")
        }
    }

    class Request : AsyncTask<String, Unit, Unit>() {
        override fun doInBackground(vararg params: String?) {

            URL("https://excavator.azurewebsites.net/api/${params[0]}c").readText()
        }
    }

    fun makeRequest(direction: String) {
        Request().execute(direction)
    }

    /**
     * This interface must be implemented by activities that contain this
     * fragment to allow an interaction in this fragment to be communicated
     * to the activity and potentially other fragments contained in that
     * activity.
     *
     *
     * See the Android Training lesson [Communicating with Other Fragments]
     * (http://developer.android.com/training/basics/fragments/communicating.html)
     * for more information.
     */
    interface OnFragmentInteractionListener {
        // TODO: Update argument type and name
        fun onFragmentInteraction(uri: Uri)
    }

}
