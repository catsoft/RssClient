package asura.com.rssclient.data

import androidx.room.ColumnInfo
import androidx.room.Entity
import androidx.room.ForeignKey
import androidx.room.PrimaryKey

@Entity(
    tableName = "rss_message",
    foreignKeys = [ForeignKey(entity = RssItem::class, parentColumns = arrayOf("rss_id"), childColumns = arrayOf("rss_id"))],
    indices = [androidx.room.Index("rss_id")]
)
data class RssMessage(
    val title: String?,
    val data: String?,
    val text: String?,
    val id: String?,
    val url: String?,

    @ColumnInfo(name = "rss_id")
    val rssId: Long,

    val isViewed: Boolean = false
) {
    @PrimaryKey(autoGenerate = true)
    @ColumnInfo(name = "rss_message_id")
    var messageId: Long = 0
}