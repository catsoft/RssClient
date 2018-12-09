package asura.com.rssclient.data

import androidx.lifecycle.LiveData
import androidx.room.*

/**
 * The Data Access Object for the [RssMessage] class
 */
@Dao
interface RssMessageDao {

    @Query("SELECT * FROM rss_message")
    fun getMessageList() : LiveData<List<RssMessage>>

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    fun insertMessage(rssItem: RssMessage) : Long

    @Delete
    fun deleteMessage(rssItem: RssMessage)

    @Query("SELECT * FROM rss_message WHERE rss_message_id = :rssMessageId")
    fun getMessageById(rssMessageId : Long) : LiveData<RssMessage>

    @Query("SELECT * FROM rss_message WHERE rss_id = :rssId")
    fun getMessagesForRss(rssId: Long): LiveData<List<RssMessage>>
}