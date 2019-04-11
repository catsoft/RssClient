package asura.com.rssclient.ui

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProviders
import androidx.navigation.fragment.findNavController
import asura.com.rssclient.databinding.FragmentRecommendationCategoriesListBinding
import asura.com.rssclient.databinding.FragmentRssEditBinding
import asura.com.rssclient.utils.hideKeyboard
import asura.com.rssclient.utils.showKeyboard
import asura.com.rssclient.viewmodels.RssEditViewModel
import asura.com.rssclient.viewmodels.RssEditViewModelFactory

class RecommendationCategoriesListFragment : Fragment() {

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        val binding = FragmentRecommendationCategoriesListBinding.inflate(inflater, container, false)

        return binding.root
    }
}