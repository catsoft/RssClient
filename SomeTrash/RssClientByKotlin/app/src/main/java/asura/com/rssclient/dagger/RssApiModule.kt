package asura.com.rssclient.dagger

import asura.com.rssclient.api.RssApiService
import dagger.Module
import dagger.Provides
import javax.inject.Singleton

@Module
class RssApiModule {

    @Singleton
    @Provides
    fun provideRssApi(): RssApiService{
        return RssApiService.create()
    }
}