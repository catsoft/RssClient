package asura.com.rssclient.ui

import android.os.Bundle
import android.view.*
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProviders
import androidx.navigation.findNavController
import androidx.navigation.fragment.findNavController
import asura.com.rssclient.R
import asura.com.rssclient.databinding.FragmentAllMessagesBinding
import asura.com.rssclient.databinding.FragmentRssEditBinding
import asura.com.rssclient.utils.hideKeyboard
import asura.com.rssclient.utils.showKeyboard
import asura.com.rssclient.viewmodels.RssEditViewModel
import asura.com.rssclient.viewmodels.RssEditViewModelFactory

class AllMessagesFragment : Fragment() {

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        val binding = FragmentAllMessagesBinding.inflate(inflater, container, false)

        setHasOptionsMenu(true)

        return binding.root
    }

    override fun onCreateOptionsMenu(menu: Menu?, inflater: MenuInflater?) {
        super.onCreateOptionsMenu(menu, inflater)

        val menuInflater = this.activity?.menuInflater ?: throw NullPointerException("Not found menuInflater")
        menuInflater.inflate(R.menu.all_messages_list_menu, menu)
    }

    override fun onOptionsItemSelected(item: MenuItem?): Boolean {
        item?.let {
            when(item.itemId){
                R.id.all_messages_list_menu_change -> {
                    view?.findNavController()?.navigate(R.id.rss_list_fragment)
                }
                else -> {}
            }
        }

        return super.onOptionsItemSelected(item)
    }
}