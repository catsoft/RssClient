package asura.com.rssclient.data

import androidx.room.ColumnInfo
import androidx.room.Entity
import androidx.room.PrimaryKey
import java.util.*

@Entity(tableName = "rss_message")
data class RssMessage(
    val title: String?,
    val data: String?,
    val text: String?,
    val id: String?,
    val url: String?,
    val isNew: Boolean = true,
    val isViewed: Boolean = false,

    @ColumnInfo(name = "rss_message_id")
    @PrimaryKey()
    val messageId: String = UUID.randomUUID().toString()
)