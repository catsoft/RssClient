package asura.com.rssclient.data

import androidx.room.ColumnInfo
import androidx.room.Entity
import androidx.room.PrimaryKey
import java.util.*

@Entity(tableName = "rss_item")
data class RssItem (
    @ColumnInfo(name = "rss_id")
    @PrimaryKey
    val rssId : String,
    val url : String,
    val name : String,
    val lastOpenDate : String,
    val createDate : Calendar = Calendar.getInstance()

) {
    override fun toString() = "$name $url"
}