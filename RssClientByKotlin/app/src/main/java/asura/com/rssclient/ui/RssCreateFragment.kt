package asura.com.rssclient.ui

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProviders
import androidx.navigation.fragment.findNavController
import asura.com.rssclient.databinding.FragmentRssCreateBinding
import asura.com.rssclient.utils.hideKeyboard
import asura.com.rssclient.utils.showKeyboard
import asura.com.rssclient.viewmodels.RssCreateViewModel

class RssCreateFragment : Fragment() {

    private lateinit var viewModel : RssCreateViewModel

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        val binding = FragmentRssCreateBinding.inflate(inflater, container, false)

        viewModel = ViewModelProviders.of(this).get(RssCreateViewModel::class.java)

        binding.createListener = View.OnClickListener{
            viewModel.addItem(binding.nameTextInput.text.toString(), binding.urlTextInput.text.toString())
            findNavController().navigateUp()
        }

        showKeyboard(binding.nameTextInput )

        return binding.root
    }

    override fun onDetach() {
        hideKeyboard()
        super.onDetach()
    }
}