package asura.com.rssclient.adapters

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.DiffUtil
import androidx.recyclerview.widget.ListAdapter
import androidx.recyclerview.widget.RecyclerView
import asura.com.rssclient.data.RssItem
import asura.com.rssclient.databinding.ListItemRssBinding

class RssItemAdapter : ListAdapter<RssItem, RssItemAdapter.RssItemViewHolder>(RssItemDiffCallback()) {

    override fun onBindViewHolder(holder: RssItemViewHolder, position: Int) {
        val item = getItem(position)
        holder.bind(item)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): RssItemViewHolder {
        val binding = ListItemRssBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        return RssItemViewHolder(binding)
    }

    class RssItemViewHolder(private val binding : ListItemRssBinding) : RecyclerView.ViewHolder(binding.root) {
        fun bind(item: RssItem) {
            binding.apply {
                executePendingBindings()
            }
        }
    }

    class RssItemDiffCallback : DiffUtil.ItemCallback<RssItem>() {
        override fun areContentsTheSame(oldItem: RssItem, newItem: RssItem): Boolean {
            return oldItem == newItem
        }

        override fun areItemsTheSame(oldItem: RssItem, newItem: RssItem): Boolean {
            return oldItem.rssId == newItem.rssId
        }
    }
}