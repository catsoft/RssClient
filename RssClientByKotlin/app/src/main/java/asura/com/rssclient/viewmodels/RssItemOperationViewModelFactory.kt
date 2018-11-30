package asura.com.rssclient.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider

class RssEditViewModelFactory(private val rssItemId : String) : ViewModelProvider.NewInstanceFactory() {

    override fun <T : ViewModel?> create(modelClass: Class<T>): T {
        return RssEditViewModel(rssItemId) as T
    }
}