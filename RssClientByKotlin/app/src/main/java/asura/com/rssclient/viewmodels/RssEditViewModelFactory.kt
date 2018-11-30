package asura.com.rssclient.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider

/**
 * Factory for creating a [RssEditViewModel] with a constructor that takes a [rssItemId].
 */
class RssEditViewModelFactory(private val rssItemId : String) : ViewModelProvider.NewInstanceFactory() {

    @Suppress("UNCHECKED_CAST")
    override fun <T : ViewModel?> create(modelClass: Class<T>): T {
        return RssEditViewModel(rssItemId) as T
    }
}