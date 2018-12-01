package asura.com.rssclient.viewmodels

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import asura.com.rssclient.api.RssApiService
import asura.com.rssclient.data.RssItem
import asura.com.rssclient.data.RssItemRepository
import asura.com.rssclient.ui.RssApplication
import io.reactivex.Observable
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.schedulers.Schedulers
import me.toptas.rssconverter.RssFeed
import javax.inject.Inject

class RssDetailViewModel(rssItemId: String) : ViewModel() {
    @Inject
    lateinit var repository: RssItemRepository

    @Inject
    lateinit var rssApiService: RssApiService

    var rssItem: LiveData<RssItem>

    private var loadData: MutableLiveData<RssFeed>
    private var errorMessage : MutableLiveData<String>

    init {
        RssApplication.appComponent.inject(this)
        rssItem = repository.getItemById(rssItemId)

        loadData = MutableLiveData()
        errorMessage = MutableLiveData()
    }

    fun loadItems() {
        Observable.just(rssApiService)
            .observeOn(Schedulers.io())
            .map {
                rssApiService.getQuery(rssItem.value?.url ?: "").execute()
            }
            .observeOn(AndroidSchedulers.mainThread())
            .subscribe({
                it?.body()?.let {
                    loadData.value = it
                }
            }, {
                errorMessage.value = it.message
            })
    }

    fun getData(): LiveData<RssFeed> = loadData
    fun getError() : LiveData<String> = errorMessage

    fun deleteItem() {
        Observable.just(repository)
            .subscribeOn(Schedulers.io())
            .map {
                rssItem.value?.let {
                    repository.deleteItem(it)
                }
            }
            .subscribe()
    }
}