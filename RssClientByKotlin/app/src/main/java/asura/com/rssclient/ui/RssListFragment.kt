package asura.com.rssclient.ui

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.navigation.findNavController
import asura.com.rssclient.databinding.FragmentRssListBinding
import asura.com.rssclient.R
import asura.com.rssclient.data.RssItemRepository
import javax.inject.Inject


class RssListFragment : DaggerFragment(){
    @Inject lateinit var repository : RssItemRepository

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        val binding = FragmentRssListBinding.inflate(inflater, container, false)

        RssApplication.appComponent.inject(this)

        binding.noItems.setOnClickListener {
            it.findNavController().navigate(R.id.rss_detail_fragment)
        }

        binding.addButton.setOnClickListener{
            it.findNavController().navigate(R.id.rss_create_fragment)
        }

        var c = repository.getItems()
        var b = Toast.makeText(context, c.value?.size.toString(), Toast.LENGTH_LONG)
        b.show()

        return binding.root
    }
}


