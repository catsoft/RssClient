package asura.com.rssclient.dagger

import asura.com.rssclient.viewmodels.RssListViewModel
import dagger.Component
import javax.inject.Singleton

@Singleton
@Component(modules = arrayOf(ApplicationModule::class, RepositotyModule::class))
interface ApplicationComponent {

    fun inject(rssListViewModel: RssListViewModel)

}