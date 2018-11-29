package asura.com.rssclient.data

import androidx.lifecycle.LiveData

class RssItemRepository private constructor(private val rssItemDao: RssItemDao){



    fun unsertItem(item : RssItem) = rssItemDao.insertRssItem(item)

    fun deleteItem(item : RssItem) = rssItemDao.deleteRssItem(item)

    fun getItems() : LiveData<List<RssItem>>
    {
        return rssItemDao.getRssItemsList()
    }

    companion object {

        // For Singleton instantiation
        @Volatile private var instance: RssItemRepository? = null

        fun getInstance(rssItemDao: RssItemDao) =
            instance ?: synchronized(this) {
                instance ?: RssItemRepository(rssItemDao).also { instance = it }
            }
    }
}