package asura.com.rssclient.viewmodels

import androidx.lifecycle.ViewModel
import asura.com.rssclient.data.RssItem
import asura.com.rssclient.data.RssItemRepository
import asura.com.rssclient.ui.RssApplication
import io.reactivex.Observable
import io.reactivex.schedulers.Schedulers
import javax.inject.Inject
import kotlin.random.Random

class RssCreateViewModel : ViewModel() {
    @Inject
    lateinit var repository: RssItemRepository

    init {
        RssApplication.appComponent.inject(this)
    }

    fun addItem(name: String, url: String) {
        Observable.just(repository)
            .subscribeOn(Schedulers.io())
            .map {
                val item = RssItem(url, name, "date2", "date")
                it.insertItem(item)
            }.subscribe()
    }
}