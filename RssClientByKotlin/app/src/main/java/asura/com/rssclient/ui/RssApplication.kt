package asura.com.rssclient.ui

import android.app.Application
import asura.com.rssclient.dagger.*

class RssApplication : Application() {

    override fun onCreate() {
        super.onCreate()

        appComponent = DaggerApplicationComponent.builder()
            .applicationModule(ApplicationModule(this))
            .repositoryModule(RepositoryModule(this))
            .rssApiModule(RssApiModule())
            .build()
    }

    companion object {
        lateinit var appComponent : ApplicationComponent
    }
}