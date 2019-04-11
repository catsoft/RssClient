package asura.com.rssclient.ui

import android.annotation.SuppressLint
import android.content.Intent
import android.os.Bundle
import android.view.*
import android.webkit.WebView
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProviders
import androidx.navigation.fragment.findNavController
import asura.com.rssclient.R
import asura.com.rssclient.adapters.RssMessageListAdapter
import asura.com.rssclient.databinding.FragmentRssMessagesListBinding
import asura.com.rssclient.stated.StatedFragment
import asura.com.rssclient.viewmodels.RssDetailViewModel
import asura.com.rssclient.viewmodels.RssDetailViewModelFactory

class RssMessageInRssListFragment : StatedFragment() {

    private lateinit var webView : WebView
    private lateinit var viewModel: RssDetailViewModel
    private lateinit var args: RssEditFragmentArgs
    private lateinit var adapter : RssMessageListAdapter

    @SuppressLint("SetJavaScriptEnabled")
    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        val binding = FragmentRssMessagesListBinding.inflate(inflater, container, false)
        args = RssEditFragmentArgs.fromBundle(arguments)

        val factory = RssDetailViewModelFactory(args.rssItemId)
        viewModel = ViewModelProviders.of(this, factory).get(RssDetailViewModel::class.java)

        subscribeOnState(viewModel)

        webView = WebView(context)
        webView.settings.javaScriptEnabled = true
        (binding.root as ViewGroup).addView(webView)

        adapter = RssMessageListAdapter(viewModel)
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
        menuInflater.inflate(R.menu.messages_rss_list_menu, menu)
    }

    override fun onOptionsItemSelected(item: MenuItem?): Boolean {
        item?.let {
            val navController = findNavController()
            when(it.itemId){
                R.id.messages_rss_list_menu_edit -> {
                    val id = viewModel.rssItem.value?.rssId
                    id?.let {
                        val toEdit = RssMessageInRssListFragmentDirections.ActionRssDetailFragmentToRssEditFragment(id)
                        navController.navigate(toEdit)
                    }
                }
                R.id.messages_rss_list_menu_remove -> {
                    viewModel.deleteItem()
                    navController.navigateUp()
                }
                R.id.messages_rss_list_menu_share -> {
                    val shareIntent = Intent()
                    shareIntent.action = Intent.ACTION_SEND
                    shareIntent.type = "text/plain"
                    shareIntent.putExtra(Intent.EXTRA_TEXT, viewModel.rssItem.value?.url)
                    startActivity(Intent.createChooser(shareIntent, getString(R.string.send_to)))
                }
                else -> {}
            }
        }

        return super.onContextItemSelected(item)
    }
}