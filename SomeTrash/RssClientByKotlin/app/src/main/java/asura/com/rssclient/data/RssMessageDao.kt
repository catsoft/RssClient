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

    @Insert(onConflict = OnConflictStrategy.IGNORE)
    fun insertMessage(rssItem: RssMessage) : Long

    @Update
    fun updateMessage(rssItem: RssMessage)

    @Delete
    fun deleteMessage(rssItem: RssMessage)

    @Query("SELECT * FROM rss_message WHERE data = :date ORDER BY data")
    fun getMessageById(date : String) : LiveData<RssMessage>

    @Query("SELECT * FROM rss_message WHERE rss_id = :rssId ORDER BY data")
    fun getMessagesForRss(rssId: Long): LiveData<List<RssMessage>>
}