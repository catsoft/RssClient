package asura.com.rssclient.api

import me.toptas.rssconverter.RssConverterFactory
import me.toptas.rssconverter.RssFeed
import retrofit2.Call
import retrofit2.Retrofit
import retrofit2.adapter.rxjava2.RxJava2CallAdapterFactory
import retrofit2.http.GET
import retrofit2.http.Url

interface RssApiService {

    @GET
    fun getQuery(@Url url: String) : Call<RssFeed>

    companion object {
        fun create() : RssApiService{
            val retrofit = Retrofit.Builder()
                .addCallAdapterFactory(RxJava2CallAdapterFactory.create())
                .baseUrl("http://baseurl")
                .addConverterFactory(RssConverterFactory.create())
                .build()

            return retrofit.create(RssApiService::class.java)
        }
    }
}