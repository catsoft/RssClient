package asura.com.rssclient.viewmodels

import androidx.lifecycle.MediatorLiveData
import androidx.lifecycle.ViewModel
import asura.com.rssclient.data.RssItem
import asura.com.rssclient.data.RssItemRepository
import asura.com.rssclient.ui.RssApplication
import asura.com.rssclient.ui.RssListFragment
import io.reactivex.Observable
import io.reactivex.schedulers.Schedulers
import javax.inject.Inject

/**
 * The ViewModel for [RssListFragment].
 */
class RssListViewModel : ViewModel() {
    @Inject
    lateinit var repository: RssItemRepository

    private val rssList = MediatorLiveData<List<RssItem>>()

    init {
        RssApplication.appComponent.inject(this)

        rssList.addSource(repository.getItems(), rssList::setValue)
    }

    fun getRssList() = rssList

    fun removeItem(item: RssItem) {
        repository.deleteItem(item)
    }
}