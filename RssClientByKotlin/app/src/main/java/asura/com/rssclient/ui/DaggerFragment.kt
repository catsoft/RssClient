package asura.com.rssclient.ui

import android.os.Bundle
import androidx.fragment.app.Fragment

open class DaggerFragment : Fragment() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        RssApplication.appComponent.inject(this)
    }

}