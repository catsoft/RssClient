package asura.com.rssclient.ui

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import asura.com.rssclient.databinding.FragmentRssDetailBinding

class RssDetailFragment : Fragment(){

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        val binding = FragmentRssDetailBinding.inflate(inflater, container, false)

        return binding.root
    }
}