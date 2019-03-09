package com.visma.autoexcavator

import android.net.Uri
import android.os.Bundle
import android.support.design.widget.BottomNavigationView
import android.support.v7.app.AppCompatActivity

class MainActivity : AppCompatActivity(), ControlFragment.OnFragmentInteractionListener,
    ApiKeyFragment.OnFragmentInteractionListener {

    override fun onFragmentInteraction(uri: Uri) {
        TODO("not implemented") //To change body of created functions use File | Settings | File Templates.
    }

    private val mOnNavigationItemSelectedListener = BottomNavigationView.OnNavigationItemSelectedListener { item ->
        when (item.itemId) {
            R.id.navigation_video -> {
                clearAllFragments()
                addApiKeyFragment()
                return@OnNavigationItemSelectedListener true
            }
            R.id.navigation_control -> {
                clearAllFragments()
                addControlFragment()
                return@OnNavigationItemSelectedListener true
            }
            R.id.navigation_notifications -> {
                return@OnNavigationItemSelectedListener true
            }
        }
        false
    }

    private fun addControlFragment() {
        supportFragmentManager
            .beginTransaction()
            .add(R.id.container, ControlFragment(), "controlFragment")
            .commit()
    }

    private fun addApiKeyFragment() {
        supportFragmentManager
            .beginTransaction()
            .add(R.id.container, ApiKeyFragment(), "apiKeyFragment")
            .commit()
    }

    private fun clearAllFragments() {
        for (fragment in supportFragmentManager.fragments) {
            supportFragmentManager.beginTransaction().remove(fragment).commit()
        }
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        addControlFragment()
    }
}
