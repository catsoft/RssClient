package asura.com.rssclient.data

import androidx.lifecycle.LiveData

/**
 * Repository module for handling data operations with [RssItem]
 */
class RssItemRepository private constructor(private val rssItemDao: RssItemDao){

    fun insertItem(item : RssItem) = rssItemDao.insertRssItem(item)

    fun updateItem(item : RssItem) = rssItemDao.updateRssItem(item)

    fun deleteItem(item : RssItem) = rssItemDao.deleteRssItem(item)

    fun getItemById(rssId : String) = rssItemDao.getItemById(rssId)

    fun getItems() : LiveData<List<RssItem>>
    {
        return rssItemDao.getRssItemsList()
    }

    companion object {

        @Volatile private var instance: RssItemRepository? = null

        fun getInstance(rssItemDao: RssItemDao) =
            instance ?: synchronized(this) {
                instance ?: RssItemRepository(rssItemDao).also { instance = it }
            }
    }
}