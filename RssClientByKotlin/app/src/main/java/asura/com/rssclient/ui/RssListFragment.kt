package asura.com.rssclient.ui

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import asura.com.rssclient.databinding.FragmentRssListBinding
import asura.com.rssclient.data.RssItem
import asura.com.rssclient.data.RssItemRepository
import io.reactivex.Observable
import io.reactivex.android.schedulers.AndroidSchedulers
import io.reactivex.schedulers.Schedulers
import javax.inject.Inject
import kotlin.random.Random


class RssListFragment : DaggerFragment(){
    @Inject lateinit var repository : RssItemRepository
    private val random = Random(1000)

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        val binding = FragmentRssListBinding.inflate(inflater, container, false)

        RssApplication.appComponent.inject(this)

        binding.noItems.setOnClickListener {

            Observable.just(repository)
                .subscribeOn(Schedulers.io())
                .map {
                    val id = random.nextLong()
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
                    val data = it

                    data.observe(this, androidx.lifecycle.Observer {
                        Toast.makeText(context, it.size.toString(), Toast.LENGTH_LONG).show()
                    })
                }
                .subscribe()
        }

        return binding.root
    }
}


