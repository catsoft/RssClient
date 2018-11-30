package asura.com.rssclient.data

import androidx.room.ColumnInfo
import androidx.room.Entity
import androidx.room.PrimaryKey
import java.util.*

@Entity(tableName = "rss_items")
data class RssItem(
    val url: String,
    val name: String,
    val lastOpenDate: String,
    val createDate: String,

    @ColumnInfo(name = "rss_id")
    @PrimaryKey(autoGenerate = true)
    val rssId: Long = 0
) {
    override fun toString() = "$name $url"
}