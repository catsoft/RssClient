package asura.com.rssclient.ui

import android.app.Application
import asura.com.rssclient.dagger.ApplicationComponent
import asura.com.rssclient.dagger.ApplicationModule
import asura.com.rssclient.dagger.DaggerApplicationComponent
import asura.com.rssclient.dagger.RepositotyModule

class RssApplication : Application() {

    override fun onCreate() {
        super.onCreate()

        appComponent = DaggerApplicationComponent.builder()
            .applicationModule(ApplicationModule(this))
            .repositotyModule(RepositotyModule(this))
            .build()
    }

    companion object {
        lateinit var appComponent : ApplicationComponent
    }
}