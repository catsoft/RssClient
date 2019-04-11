package asura.com.rssclient.data

import androidx.lifecycle.LiveData
import asura.com.rssclient.utilites.runOnIoThread

/**
 * Repository module for handling data operations with [RssItem]
 */
class RssItemRepository private constructor(private val rssItemDao: RssItemDao) {

    fun insertItem(item: RssItem) = runOnIoThread { rssItemDao.insertRssItem(item) }

    fun deleteItem(item: RssItem) = runOnIoThread { rssItemDao.deleteRssItem(item) }

    fun getItemById(rssId: Long) = rssItemDao.getItemById(rssId)

    fun getItems(): LiveData<List<RssItem>> {
        return rssItemDao.getRssItemsList()
    }

    companion object {

        @Volatile
        private var instance: RssItemRepository? = null

        fun getInstance(rssItemDao: RssItemDao) =
            instance ?: synchronized(this) {
                instance ?: RssItemRepository(rssItemDao).also { instance = it }
            }
    }
}