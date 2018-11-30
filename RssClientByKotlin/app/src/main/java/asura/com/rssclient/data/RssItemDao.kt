package asura.com.rssclient.data

import androidx.lifecycle.LiveData
import androidx.room.*

/**
 * The Data Access Object for the [RssItem] class
 */
@Dao
interface RssItemDao {

    @Query("Select * FROM rss_items")
    fun getRssItemsList() : LiveData<List<RssItem>>

    @Insert
    fun insertRssItem(rssItem: RssItem) : Long

    @Update
    fun updateRssItem(rssItem: RssItem)

    @Delete
    fun deleteRssItem(rssItem: RssItem)

    @Query("SELECT * FROM rss_items WHERE rss_id = :rssId")
    fun getItemById(rssId : String) : LiveData<RssItem>
}