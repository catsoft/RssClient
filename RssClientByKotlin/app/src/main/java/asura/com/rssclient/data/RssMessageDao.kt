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

    @Insert
    fun insertMessage(rssItem: RssMessage) : Long

    @Update
    fun updateMessage(rssItem: RssMessage)

    @Delete
    fun deleteMessage(rssItem: RssMessage)

    @Query("SELECT * FROM rss_message WHERE rss_message_id = :rssMessageId")
    fun getMessageById(rssMessageId : String) : LiveData<RssMessage>
}