package asura.com.rssclient.dagger

import android.app.Application
import asura.com.rssclient.data.AppDatabase
import asura.com.rssclient.data.RssItemRepository
import dagger.Module
import dagger.Provides
import javax.inject.Singleton

@Module
class RepositoryModule(private val baseApp: Application) {

    @Singleton
    @Provides
    fun provideRssItemRepository(): RssItemRepository {
        return RssItemRepository.getInstance(AppDatabase.getInstance(baseApp.applicationContext).rssItemDao())
    }
}