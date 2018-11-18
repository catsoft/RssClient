package asura.com.rssclient.data

import androidx.room.Dao
import androidx.room.Delete
import androidx.room.Insert

/**
 * The Data Access Object for the [RssItem] class
 */
@Dao
interface RssItemDao {

    @Insert
    fun insertRssItem(rssItem: RssItem) : Long

    @Delete
    fun deleteRssItem(rssItem: RssItem)
}