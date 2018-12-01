package asura.com.rssclient.dagger

import asura.com.rssclient.viewmodels.RssCreateViewModel
import asura.com.rssclient.viewmodels.RssDetailViewModel
import asura.com.rssclient.viewmodels.RssEditViewModel
import asura.com.rssclient.viewmodels.RssListViewModel
import dagger.Component
import javax.inject.Singleton

@Singleton
@Component(modules = [ApplicationModule::class, RepositoryModule::class])
interface ApplicationComponent {

    fun inject(rssListViewModel: RssListViewModel)
    fun inject(rssCreateViewModel: RssCreateViewModel)
    fun inject(rssEditViewModel: RssEditViewModel)
    fun inject(rssDetailViewModel: RssDetailViewModel)
}