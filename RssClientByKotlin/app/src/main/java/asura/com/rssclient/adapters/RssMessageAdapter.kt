package asura.com.rssclient.adapters

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.DiffUtil
import androidx.recyclerview.widget.ListAdapter
import androidx.recyclerview.widget.RecyclerView
import asura.com.rssclient.data.RssMessage
import asura.com.rssclient.databinding.ListRssMessagesItemBinding
import asura.com.rssclient.ui.RssDetailFragment
import org.jetbrains.anko.browse

/**
 * Adapter for the [RecyclerView] in [RssDetailFragment].
 */
class RssMessageAdapter : ListAdapter<RssMessage, RssMessageAdapter.RssMessageViewHolder>(RssMessageAdapter.RssItemDiffCallback()) {

    override fun onBindViewHolder(holder: RssMessageViewHolder, position: Int) {
        val item = getItem(position)
        holder.bind(item)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): RssMessageViewHolder {
        val binding = ListRssMessagesItemBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        return RssMessageAdapter.RssMessageViewHolder(binding)
    }

    class RssMessageViewHolder(private val binding : ListRssMessagesItemBinding) : RecyclerView.ViewHolder(binding.root){
        fun bind(rssItem: RssMessage) {
            binding.apply {
                clickListener = View.OnClickListener {
                    root.context.browse(item?.url ?: "", false)
                }
                item = rssItem
                executePendingBindings()
            }
            itemView.isLongClickable = true
        }
    }

    class RssItemDiffCallback : DiffUtil.ItemCallback<RssMessage>() {
        override fun areContentsTheSame(oldItem: RssMessage, newItem: RssMessage): Boolean {
            return oldItem == newItem
        }

        override fun areItemsTheSame(oldItem: RssMessage, newItem: RssMessage): Boolean {
            return oldItem.id == newItem.id
        }
    }
}