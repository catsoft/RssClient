package asura.com.rssclient.viewmodels

import androidx.lifecycle.ViewModel
import asura.com.rssclient.data.RssItem
import asura.com.rssclient.data.RssItemRepository
import asura.com.rssclient.ui.App
import asura.com.rssclient.ui.RssCreateFragment
import javax.inject.Inject

/**
 * The ViewModel for [RssCreateFragment].
 */
class RssCreateViewModel : ViewModel() {
    @Inject
    lateinit var repository: RssItemRepository

    init {
        App.appComponent.inject(this)
    }

    fun addItem(name: String, url: String) {
        val item = RssItem(url, name, "date2", "date", "date3")
        repository.insertItem(item)
    }
}