package asura.com.rssclient.ui

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.navigation.findNavController
import asura.com.rssclient.databinding.FragmentRssListBinding
import asura.com.rssclient.R


class RssListFragment : Fragment(){
    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        val binding = FragmentRssListBinding.inflate(inflater, container, false)

        binding.noItems.setOnClickListener {
            it.findNavController().navigate(R.id.rss_detail_fragment)
        }

        return binding.root
    }
}