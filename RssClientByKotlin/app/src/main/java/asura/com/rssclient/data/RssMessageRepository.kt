package asura.com.rssclient.data

import androidx.lifecycle.LiveData

/**
 * Repository module for handling data operations with [RssMessage]
 */
class RssMessageRepository private constructor(private val rssMessageDao: RssMessageDao){

    fun insertItem(item : RssMessage) = rssMessageDao.insertMessage(item)

    fun updateItem(item : RssMessage) = rssMessageDao.updateMessage(item)

    fun deleteItem(item : RssMessage) = rssMessageDao.deleteMessage(item)

    fun getItemById(rssId : String) = rssMessageDao.getMessageById(rssId)

    fun getItems() : LiveData<List<RssMessage>>
    {
        return rssMessageDao.getMessageList()
    }

    companion object {

        @Volatile private var instance: RssMessageRepository? = null

        fun getInstance(rssMessageDao: RssMessageDao) =
            instance ?: synchronized(this) {
                instance ?: RssMessageRepository(rssMessageDao).also { instance = it }
            }
    }
}