package asura.com.rssclient.ui

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProviders
import asura.com.rssclient.databinding.FragmentRssEditBinding
import asura.com.rssclient.viewmodels.RssEditViewModel
import asura.com.rssclient.viewmodels.RssEditViewModelFactory

class RssEditFragment : Fragment() {

    private lateinit var viewModel: RssEditViewModel

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        val binding = FragmentRssEditBinding.inflate(inflater, container, false)
        val arguments = RssEditFragmentArgs.fromBundle(arguments)

        val factory = RssEditViewModelFactory(arguments.rssItemId)
        viewModel = ViewModelProviders.of(this, factory).get(RssEditViewModel::class.java)

        subscribeRssItem(binding)

        binding.editListener = View.OnClickListener {
            var name = binding.nameTextInput.text.toString()
            var url = binding.nameTextInput.text.toString()
            viewModel.updateItem(url, name)
        }

        return binding.root
    }

    private fun subscribeRssItem(binding: FragmentRssEditBinding) {
        viewModel.rssItem.observe(viewLifecycleOwner, Observer {
            binding.nameTextInput.setText(it.name)
            binding.urlTextInput.setText(it.url)
        })
    }
}