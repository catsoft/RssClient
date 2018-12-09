package asura.com.rssclient.viewmodels

import androidx.lifecycle.ViewModel
import asura.com.rssclient.data.RssItem
import asura.com.rssclient.data.RssItemRepository
import asura.com.rssclient.ui.RssApplication
import asura.com.rssclient.ui.RssCreateFragment
import io.reactivex.Observable
import io.reactivex.schedulers.Schedulers
import javax.inject.Inject

/**
 * The ViewModel for [RssCreateFragment].
 */
class RssCreateViewModel : ViewModel() {
    @Inject
    lateinit var repository: RssItemRepository

    init {
        RssApplication.appComponent.inject(this)
    }

    fun addItem(name: String, url: String) {
        val item = RssItem(url, name, "date2", "date")
        repository.insertItem(item)
    }
}