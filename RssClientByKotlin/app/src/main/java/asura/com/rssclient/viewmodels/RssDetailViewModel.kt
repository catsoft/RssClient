package asura.com.rssclient.viewmodels

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import asura.com.rssclient.api.RssApiService
import asura.com.rssclient.data.RssItem
import asura.com.rssclient.data.RssItemRepository
import asura.com.rssclient.stated.InvalidStateData
import asura.com.rssclient.stated.LoadStateData
import asura.com.rssclient.stated.NormalStateData
import asura.com.rssclient.stated.StatedViewModel
import asura.com.rssclient.ui.RssApplication
import io.reactivex.Observable
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.schedulers.Schedulers
import kotlinx.coroutines.delay
import me.toptas.rssconverter.RssFeed
import java.util.concurrent.TimeUnit
import javax.inject.Inject

class RssDetailViewModel(rssItemId: String) : StatedViewModel() {
    @Inject
    lateinit var repository: RssItemRepository

    @Inject
    lateinit var rssApiService: RssApiService

    var rssItem: LiveData<RssItem>

    private var loadData: MutableLiveData<RssFeed>
    fun getData(): LiveData<RssFeed> = loadData

    init {
        RssApplication.appComponent.inject(this)
        rssItem = repository.getItemById(rssItemId)

        loadData = MutableLiveData()
    }

    fun loadItems() {
        setLoadState(LoadStateData())
        Observable.just(rssApiService)
            .observeOn(Schedulers.io())
            .map {
                rssApiService.getQuery(rssItem.value?.url ?: "").execute()
            }
            .delay(2000, TimeUnit.MILLISECONDS)
            .observeOn(AndroidSchedulers.mainThread())
            .subscribe({
                it?.body()?.let {
                    setNormalState(NormalStateData())
                    loadData.value = it
                }
            }, {
                setInvalidState(InvalidStateData(it.message))
            })
    }

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