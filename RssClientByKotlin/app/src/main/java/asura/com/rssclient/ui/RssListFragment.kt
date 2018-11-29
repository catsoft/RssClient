package asura.com.rssclient.ui

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProviders
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import asura.com.rssclient.adapters.RssItemAdapter
import asura.com.rssclient.databinding.FragmentRssListBinding
import asura.com.rssclient.viewmodels.RssListViewModel


class RssListFragment : Fragment(){

    private lateinit var viewModel: RssListViewModel

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        val binding = FragmentRssListBinding.inflate(inflater, container, false)

        viewModel = ViewModelProviders.of(this).get(RssListViewModel::class.java)

        binding.noItems.setOnClickListener { viewModel.addItem() }
        val adapter = RssItemAdapter()
        binding.rssList.adapter = adapter
        binding.rssList.layoutManager = LinearLayoutManager(context, RecyclerView.VERTICAL, false)
        subscribeUi(adapter)

        return binding.root
    }

    private fun subscribeUi(adapter: RssItemAdapter) {
        viewModel.getRssList().observe(viewLifecycleOwner, Observer {
            if (it != null) {
                adapter.submitList(it.toMutableList())
            }
        })
    }
}


