package asura.com.rssclient.data

import asura.com.rssclient.utilites.runOnIoThread

/**
 * Repository module for handling data operations with [RssMessage]
 */
class RssMessageRepository private constructor(private val rssMessageDao: RssMessageDao) {

    fun insertItem(item: RssMessage) = runOnIoThread { rssMessageDao.insertMessage(item) }

    fun deleteItem(item: RssMessage) = runOnIoThread { rssMessageDao.deleteMessage(item) }

    fun getItemById(rssId: Long) = rssMessageDao.getMessageById(rssId)

    fun getItems(rssId: Long) = rssMessageDao.getMessagesForRss(rssId)

    companion object {

        @Volatile
        private var instance: RssMessageRepository? = null

        fun getInstance(rssMessageDao: RssMessageDao) =
            instance ?: synchronized(this) {
                instance ?: RssMessageRepository(rssMessageDao).also { instance = it }
            }
    }
}