package asura.com.rssclient.data

import androidx.lifecycle.LiveData
import androidx.room.Dao
import androidx.room.Delete
import androidx.room.Insert
import androidx.room.Query

/**
 * The Data Access Object for the [RssItem] class
 */
@Dao
interface RssItemDao {

    @Query("Select * From rss_item Order by createDate")
    fun getRssItemsList() : LiveData<List<RssItem>>

    @Insert
    fun insertRssItem(rssItem: RssItem) : Long

    @Delete
    fun deleteRssItem(rssItem: RssItem)
}