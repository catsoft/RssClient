package asura.com.rssclient.viewmodels

import androidx.lifecycle.MediatorLiveData
import androidx.lifecycle.ViewModel
import asura.com.rssclient.data.RssItem
import asura.com.rssclient.data.RssItemRepository
import asura.com.rssclient.ui.RssApplication
import io.reactivex.Observable
import io.reactivex.schedulers.Schedulers
import javax.inject.Inject
import kotlin.random.Random

/**
 * The ViewModel for [RssListFragment].
 */
class RssListViewModel : ViewModel(){
    @Inject lateinit var repository: RssItemRepository
    private val random = Random(1000)

    private val rssList = MediatorLiveData<List<RssItem>>()

    init {
        RssApplication.appComponent.inject(this)

        val items = repository.getItems()
        rssList.addSource(items, rssList::setValue)
    }

    fun getRssList() = rssList

    fun addItem() {
        Observable.just(repository)
            .subscribeOn(Schedulers.io())
            .map {
                val id = random.nextLong()
                val item = RssItem(id, "url", "name", "date", "date")
                it.insertItem(item)
            }.subscribe()
    }
}