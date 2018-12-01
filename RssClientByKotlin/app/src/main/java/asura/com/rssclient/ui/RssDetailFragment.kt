package asura.com.rssclient.ui

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProviders
import asura.com.rssclient.databinding.FragmentRssDetailBinding
import asura.com.rssclient.viewmodels.RssDetailViewModel
import asura.com.rssclient.viewmodels.RssDetailViewModelFactory

class RssDetailFragment : Fragment(){

    private lateinit var viewModel : RssDetailViewModel
    private lateinit var args : RssEditFragmentArgs

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        val binding = FragmentRssDetailBinding.inflate(inflater, container, false)
        args = RssEditFragmentArgs.fromBundle(arguments)

        val factory = RssDetailViewModelFactory(args.rssItemId)
        viewModel = ViewModelProviders.of(this, factory).get(RssDetailViewModel::class.java)

        subscribeItem(binding.root)

        return binding.root
    }

    private fun subscribeItem(root: View) {
        viewModel.rssItem.observe(viewLifecycleOwner, Observer {
            val textView = TextView(context)
            textView.text = it.name
            (root as ViewGroup).addView(textView)
        })
    }
}