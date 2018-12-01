package asura.com.rssclient.ui

import android.annotation.SuppressLint
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.webkit.WebView
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProviders
import asura.com.rssclient.databinding.FragmentRssDetailBinding
import asura.com.rssclient.viewmodels.RssDetailViewModel
import asura.com.rssclient.viewmodels.RssDetailViewModelFactory

class RssDetailFragment : Fragment() {

    private lateinit var viewModel: RssDetailViewModel
    private lateinit var args: RssEditFragmentArgs

    @SuppressLint("SetJavaScriptEnabled")
    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        val binding = FragmentRssDetailBinding.inflate(inflater, container, false)
        args = RssEditFragmentArgs.fromBundle(arguments)

        val factory = RssDetailViewModelFactory(args.rssItemId)
        viewModel = ViewModelProviders.of(this, factory).get(RssDetailViewModel::class.java)

        viewModel.getData().observe(viewLifecycleOwner, Observer {
            val webView = WebView(context)
            webView.settings.javaScriptEnabled = true
            webView.loadDataWithBaseURL("", it, "text/html", "URF-8", "")
            (binding.root as ViewGroup).addView(webView)
        })

        viewModel.rssItem.observe(viewLifecycleOwner, Observer {
            viewModel.loadItems()
        })

        return binding.root
    }
}