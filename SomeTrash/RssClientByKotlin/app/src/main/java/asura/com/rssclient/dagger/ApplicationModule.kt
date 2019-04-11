package asura.com.rssclient.dagger

import android.app.Application
import android.content.Context
import asura.com.rssclient.ui.App
import dagger.Module
import dagger.Provides
import javax.inject.Singleton

@Module
class ApplicationModule(private val rssApplication: App) {

    @Provides
    @Singleton
    fun provideApplication(): Application {
        return rssApplication
    }

    @Singleton
    @Provides
    fun provideContext() : Context {
        return rssApplication.applicationContext
    }
}