package asura.com.rssclient.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider

/**
 * Factory for creating a [RssDetailViewModel] with a constructor that takes a [rssItemId].
 */
class RssDetailViewModelFactory(private val rssItemId : Long) : ViewModelProvider.NewInstanceFactory() {

    @Suppress("UNCHECKED_CAST")
    override fun <T : ViewModel?> create(modelClass: Class<T>): T {
        return RssDetailViewModel(rssItemId) as T
    }
}