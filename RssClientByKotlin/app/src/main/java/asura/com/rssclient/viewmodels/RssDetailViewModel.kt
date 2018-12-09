package asura.com.rssclient.viewmodels

import android.content.Context
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import asura.com.rssclient.api.RssApiService
import asura.com.rssclient.data.RssItem
import asura.com.rssclient.data.RssItemRepository
import asura.com.rssclient.data.RssMessage
import asura.com.rssclient.data.RssMessageRepository
import asura.com.rssclient.stated.InvalidStateData
import asura.com.rssclient.stated.LoadStateData
import asura.com.rssclient.stated.NormalStateData
import asura.com.rssclient.stated.StatedViewModel
import asura.com.rssclient.ui.RssApplication
import io.reactivex.Observable
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.schedulers.Schedulers
import me.toptas.rssconverter.RssFeed
import org.jetbrains.anko.browse
import java.util.concurrent.TimeUnit
import javax.inject.Inject

class RssDetailViewModel(val rssItemId: Long) : StatedViewModel() {
    @Inject
    lateinit var repository: RssItemRepository

    @Inject
    lateinit var rssApiService: RssApiService

    @Inject
    lateinit var context: Context

    @Inject
    lateinit var rssMessageRepository: RssMessageRepository

    var rssItem: LiveData<RssItem>
    var rssMessages: LiveData<List<RssMessage>>

    init {
        RssApplication.appComponent.inject(this)
        rssItem = repository.getItemById(rssItemId)

        rssMessages = rssMessageRepository.getItems(rssItemId)
    }

    fun loadItems() {
        setLoadState(LoadStateData())
        Observable.just(rssApiService)
            .observeOn(Schedulers.io())
            .map {
                rssApiService.getQuery(rssItem.value?.url ?: "").execute()
            }
            .delay(2000, TimeUnit.MILLISECONDS)
            .map {
                it.body()?.let {
                    it.items?.map {
                        var item = RssMessage(it.title, it.publishDate, it.description, it.title, it.link, rssItemId)
                        rssMessageRepository.insertItem(item)
                    }
                }
            }
            .observeOn(AndroidSchedulers.mainThread())
            .subscribe({
                setNormalState(NormalStateData())
            }, {
                setInvalidState(InvalidStateData(it.message))
            })
    }

    fun deleteItem() {
        rssItem.value?.let {
            repository.deleteItem(it)
        }
    }

    fun openMessage(rssMessage: RssMessage) {
        context.browse(rssMessage.url ?: "", false)
        val viewedMessage = rssMessage.copy(isViewed = true)
        viewedMessage.messageId = rssMessage.messageId
        rssMessageRepository.insertItem(viewedMessage)
    }
}