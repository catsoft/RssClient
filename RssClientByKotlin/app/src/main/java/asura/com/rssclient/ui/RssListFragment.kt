package asura.com.rssclient.ui

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProviders
import asura.com.rssclient.databinding.FragmentRssListBinding
import asura.com.rssclient.viewmodels.RssListViewModel


class RssListFragment : Fragment(){

    private lateinit var viewModel: RssListViewModel

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        val binding = FragmentRssListBinding.inflate(inflater, container, false)

        viewModel = ViewModelProviders.of(this).get(RssListViewModel::class.java)
        viewModel.getRssList().observe(this,androidx.lifecycle.Observer  {
            Toast.makeText(context, it.size.toString(), Toast.LENGTH_LONG).show()
        })

        binding.noItems.setOnClickListener { viewModel.addItem() }

        return binding.root
    }
}


