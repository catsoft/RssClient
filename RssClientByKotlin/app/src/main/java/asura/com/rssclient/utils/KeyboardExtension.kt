package asura.com.rssclient.utils

import android.app.Activity
import android.view.inputmethod.InputMethodManager
import androidx.fragment.app.Fragment
import com.google.android.material.textfield.TextInputEditText

fun Fragment.hideKeyboard() {
    doHideKeyboard(activity)
}

fun Activity.hideKeyboard() {
    doHideKeyboard(this)
}

private fun doHideKeyboard(activity: Activity?) {
    activity?.let {
        val inputManager = it.getSystemService(android.content.Context.INPUT_METHOD_SERVICE) as InputMethodManager
        inputManager.hideSoftInputFromWindow(it.currentFocus?.windowToken, 0)
    }
}


fun Fragment.showKeyboard(input: TextInputEditText) {
    doShowKeyboard(activity, input)
}

fun Activity.showKeyboard(input: TextInputEditText) {
    doShowKeyboard(this, input)
}

private fun doShowKeyboard(activity: Activity?, input: TextInputEditText){
    input.requestFocus()
    activity?.let {
        val inputManager = it.getSystemService(android.content.Context.INPUT_METHOD_SERVICE) as InputMethodManager
        inputManager.toggleSoftInput(InputMethodManager.SHOW_FORCED, InputMethodManager.SHOW_FORCED)
    }
}