package asura.com.rssclient.ui

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import asura.com.rssclient.databinding.FragmentRssCreateBinding

class RssCreateFragment : DaggerFragment() {

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        var binding = FragmentRssCreateBinding.inflate(inflater, container, false)

        return binding.root
    }
}