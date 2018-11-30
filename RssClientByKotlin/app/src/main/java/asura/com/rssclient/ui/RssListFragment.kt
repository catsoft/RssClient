package asura.com.rssclient.ui

import android.os.Bundle
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProviders
import androidx.navigation.fragment.findNavController
import asura.com.rssclient.adapters.RssItemAdapter
import asura.com.rssclient.databinding.FragmentRssListBinding
import asura.com.rssclient.viewmodels.RssListViewModel
import asura.com.rssclient.R
import android.view.*
import asura.com.rssclient.ui.recyclerview.LongClickRecyclerView
import java.lang.NullPointerException

class RssListFragment : Fragment() {

    private lateinit var viewModel: RssListViewModel
    private lateinit var adapter: RssItemAdapter

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        val binding = FragmentRssListBinding.inflate(inflater, container, false)

        viewModel = ViewModelProviders.of(this).get(RssListViewModel::class.java)

        adapter = RssItemAdapter()
        binding.rssList.adapter = adapter
        subscribeUi(adapter)

        binding.addButton.setOnClickListener {
            findNavController().navigate(R.id.rss_create_fragment)
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
            if (item.itemId == R.id.context_menu_remove) {
                val menuInfo = item.menuInfo
                val castMenuInfo = menuInfo as LongClickRecyclerView.RecyclerContextMenuInfo

                val rssItem = adapter.getRssItem(castMenuInfo.position)
                viewModel.removeItem(rssItem)
            }
        }

        return super.onContextItemSelected(item)
    }

    private fun subscribeUi(adapter: RssItemAdapter) {
        viewModel.getRssList().observe(viewLifecycleOwner, Observer {
            if (it != null) {
                adapter.submitList(it.toMutableList())
            }
        })
    }
}


