package asura.com.rssclient.ui

import android.annotation.SuppressLint
import android.os.Bundle
import android.view.*
import android.webkit.WebView
import androidx.core.view.isGone
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProviders
import androidx.navigation.fragment.findNavController
import asura.com.rssclient.R
import asura.com.rssclient.adapters.RssMessageAdapter
import asura.com.rssclient.data.RssMessage
import asura.com.rssclient.databinding.FragmentRssDetailBinding
import asura.com.rssclient.stated.StatedFragment
import asura.com.rssclient.viewmodels.RssDetailViewModel
import asura.com.rssclient.viewmodels.RssDetailViewModelFactory

class RssDetailFragment : StatedFragment() {

    private lateinit var webView : WebView
    private lateinit var viewModel: RssDetailViewModel
    private lateinit var args: RssEditFragmentArgs
    private lateinit var adapter : RssMessageAdapter

    @SuppressLint("SetJavaScriptEnabled")
    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        val binding = FragmentRssDetailBinding.inflate(inflater, container, false)
        args = RssEditFragmentArgs.fromBundle(arguments)

        val factory = RssDetailViewModelFactory(args.rssItemId)
        viewModel = ViewModelProviders.of(this, factory).get(RssDetailViewModel::class.java)

        subscribeOnState(viewModel)

        webView = WebView(context)
        webView.settings.javaScriptEnabled = true
        (binding.root as ViewGroup).addView(webView)

        adapter = RssMessageAdapter(viewModel)
        binding.recyclerView.adapter = adapter

        viewModel.rssMessages.observe(viewLifecycleOwner, Observer {
            if(it != null){
                adapter.submitList(it)
            }
        })

        viewModel.rssItem.observe(viewLifecycleOwner, Observer {
            viewModel.loadItems()
        })

        setHasOptionsMenu(true)

        return binding.root
    }

    override fun onCreateOptionsMenu(menu: Menu?, inflater: MenuInflater?) {
        super.onCreateOptionsMenu(menu, inflater)

        val menuInflater = this.activity?.menuInflater ?: throw NullPointerException("Not found menuInflater")
        menuInflater.inflate(R.menu.rss_detail_menu, menu)
    }

    override fun onOptionsItemSelected(item: MenuItem?): Boolean {
        item?.let {
            val navController = findNavController()
            when(it.itemId){
                R.id.rss_detail_menu_edit -> {
                    val id = viewModel.rssItem.value?.rssId
                    id?.let {
                        val toEdit = RssDetailFragmentDirections.ActionRssDetailFragmentToRssEditFragment(id)
                        navController.navigate(toEdit)
                    }
                }
                R.id.rss_detail_menu_remove -> {
                    viewModel.deleteItem()
                    navController.navigateUp()
                }
                else -> {

                }
            }
        }

        return super.onContextItemSelected(item)
    }
}