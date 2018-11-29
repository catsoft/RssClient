package asura.com.rssclient.ui

import android.os.Bundle
import android.os.Looper
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.navigation.findNavController
import androidx.navigation.fragment.findNavController
import asura.com.rssclient.databinding.FragmentRssListBinding
import asura.com.rssclient.R
import asura.com.rssclient.data.RssItem
import asura.com.rssclient.data.RssItemRepository
import io.reactivex.Observable
import io.reactivex.Observer
import io.reactivex.Scheduler
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.disposables.Disposable
import io.reactivex.schedulers.Schedulers
import javax.inject.Inject
import kotlin.random.Random


class RssListFragment : DaggerFragment(){
    @Inject lateinit var repository : RssItemRepository

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        val binding = FragmentRssListBinding.inflate(inflater, container, false)

        RssApplication.appComponent.inject(this)

        binding.noItems.setOnClickListener {

            Observable.just(repository)
                .subscribeOn(Schedulers.io())
                .map {
                    var id = Random(10000).nextLong()
                    val item = RssItem(id, "url", "name", "date", "date")
                    it.insertItem(item)
                }
                .subscribeOn(AndroidSchedulers.mainThread())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe {
                    Toast.makeText(context, it.toString(), Toast.LENGTH_LONG).show()
                }
        }

        binding.addButton.setOnClickListener{
            Observable.just(repository)
                .subscribeOn(Schedulers.io())
                .observeOn(Schedulers.io())
                .map {
                    it.getItems()
                }
                .observeOn(AndroidSchedulers.mainThread())
                .map {
                    var data = it
                    Toast.makeText(context, data.value?.size.toString(), Toast.LENGTH_LONG).show()
                }
                .subscribe()
        }

        return binding.root
    }
}


