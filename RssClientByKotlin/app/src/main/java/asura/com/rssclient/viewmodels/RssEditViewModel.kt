package asura.com.rssclient.viewmodels

import androidx.lifecycle.LiveData
import androidx.lifecycle.ViewModel
import asura.com.rssclient.data.RssItem
import asura.com.rssclient.data.RssItemRepository
import asura.com.rssclient.ui.RssApplication
import io.reactivex.Observable
import io.reactivex.schedulers.Schedulers
import javax.inject.Inject

class RssEditViewModel(rssId: String) : ViewModel() {
    @Inject
    lateinit var repository: RssItemRepository

    var rssItem : LiveData<RssItem>

    init {
        RssApplication.appComponent.inject(this)

        rssItem = repository.getItemById(rssId)
    }

    fun updateItem(url : String, name : String){
        rssItem.value?.let {
            val copy = it.copy(name = name, url = url)
            Observable.just(repository)
                .observeOn(Schedulers.io())
                .subscribe {
                    repository.updateItem(copy)
                }
        }
    }
}