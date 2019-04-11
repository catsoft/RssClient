package asura.com.rssclient.viewmodels

import androidx.lifecycle.LiveData
import androidx.lifecycle.ViewModel
import asura.com.rssclient.data.RssItem
import asura.com.rssclient.data.RssItemRepository
import asura.com.rssclient.ui.App
import asura.com.rssclient.ui.RssEditFragment
import javax.inject.Inject

/**
 * The ViewModel for [RssEditFragment].
 */
class RssEditViewModel(rssId: Long) : ViewModel() {
    @Inject
    lateinit var repository: RssItemRepository

    var rssItem : LiveData<RssItem>

    init {
        App.appComponent.inject(this)

        rssItem = repository.getItemById(rssId)
    }

    fun updateItem(url : String, name : String){
        rssItem.value?.let {
            val copy = it.copy(name = name, url = url)
            copy.rssId = it.rssId
            repository.insertItem(copy)
        }
    }
}