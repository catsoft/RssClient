package asura.com.rssclient.ui

import android.content.Intent
import android.os.Bundle
import android.view.*
import android.widget.TextView
import androidx.core.view.isGone
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProviders
import androidx.navigation.fragment.findNavController
import asura.com.rssclient.R
import asura.com.rssclient.adapters.RssListAdapter
import asura.com.rssclient.databinding.FragmentRssListBinding
import asura.com.rssclient.ui.recyclerview.LongClickRecyclerView
import asura.com.rssclient.viewmodels.RssListViewModel

class RssListFragment : Fragment() {

    private lateinit var viewModel: RssListViewModel
    private lateinit var adapter: RssListAdapter

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        val binding = FragmentRssListBinding.inflate(inflater, container, false)

        viewModel = ViewModelProviders.of(this).get(RssListViewModel::class.java)

        adapter = RssListAdapter()
        binding.rssList.adapter = adapter
        subscribeAdapter(adapter)
        subscribeEmptyView(binding.noItems)

        binding.addButton.setOnClickListener {
            val direction = RssListFragmentDirections.ActionRssListFragmentToRssCreateFragment()
            findNavController().navigate(direction)
        }

        registerForContextMenu(binding.rssList)

        return binding.root
    }

     override fun onCreateContextMenu(menu: ContextMenu?, v: View?, menuInfo: ContextMenu.ContextMenuInfo?) {
        super.onCreateContextMenu(menu, v, menuInfo)

        val menuInflater = this.activity?.menuInflater ?: throw NullPointerException("Not found menuInflater")
        menuInflater.inflate(R.menu.context_menu_list_rss, menu)
    }

    override fun onContextItemSelected(item: MenuItem?): Boolean {
        item?.let {
            val menuInfo = item.menuInfo
            val castMenuInfo = menuInfo as LongClickRecyclerView.RecyclerContextMenuInfo
            val rssItem = adapter.getRssItem(castMenuInfo.position)

            when(item.itemId){
                R.id.context_menu_rss_list_edit -> {
                    val direction = RssListFragmentDirections.ActionRssListFragmentToRssEditFragment(rssItem.rssId)
                    findNavController().navigate(direction)
                }
                R.id.context_menu_rss_list_remove -> {
                    viewModel.removeItem(rssItem)
                }
                R.id.context_menu_rss_list_share -> {
                    val shareIntent = Intent()
                    shareIntent.action = Intent.ACTION_SEND
                    shareIntent.type = "text/plain"
                    shareIntent.putExtra(Intent.EXTRA_TEXT, rssItem.url)
                    startActivity(Intent.createChooser(shareIntent, getString(R.string.send_to)))
                }
            }
        }

        return super.onContextItemSelected(item)
    }

    private fun subscribeAdapter(adapter: RssListAdapter) {
        viewModel.getRssList().observe(viewLifecycleOwner, Observer {
            if (it != null) {
                adapter.submitList(it.toMutableList())
            }
        })
    }

    private fun subscribeEmptyView(noItems: TextView) {
        viewModel.getRssList().observe(viewLifecycleOwner, Observer {
            noItems.isGone = it?.isNotEmpty() == true
        })
    }
}


