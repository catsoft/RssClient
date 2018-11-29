package asura.com.rssclient.dagger

import asura.com.rssclient.ui.DaggerFragment
import asura.com.rssclient.ui.RssListFragment
import dagger.Component
import javax.inject.Singleton

@Singleton
@Component(modules = arrayOf(ApplicationModule::class, RepositotyModule::class))
interface ApplicationComponent {

    fun inject(daggerFragment: DaggerFragment)
    fun inject(fragment: RssListFragment)
}