package asura.com.rssclient.ui

import android.content.Context
import android.content.Intent
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.GravityCompat
import androidx.databinding.DataBindingUtil
import androidx.drawerlayout.widget.DrawerLayout
import androidx.navigation.NavController
import androidx.navigation.Navigation
import androidx.navigation.ui.AppBarConfiguration
import androidx.navigation.ui.navigateUp
import androidx.navigation.ui.setupActionBarWithNavController
import androidx.navigation.ui.setupWithNavController
import asura.com.rssclient.R
import asura.com.rssclient.databinding.ActivityRssBinding
import java.util.*

class MainActivity : AppCompatActivity() {

    private lateinit var drawerLayout : DrawerLayout
    private lateinit var navController : NavController
    private lateinit var appbarConfiguration : AppBarConfiguration

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        val binding: ActivityRssBinding = DataBindingUtil.setContentView(this, R.layout.activity_rss)

        drawerLayout = binding.drawerLayout

        navController = Navigation.findNavController(this, R.id.rss_list_nav_fragment)

        val topLevelDestination = HashSet<Int>()
        topLevelDestination.add(R.id.all_messages_fragment)
        topLevelDestination.add(R.id.recommendation_categories_list_fragment)
        topLevelDestination.add(R.id.settings_fragment)
        topLevelDestination.add(R.id.contacts_fragment)
        topLevelDestination.add(R.id.about_fragment)

        appbarConfiguration = AppBarConfiguration(topLevelDestination, drawerLayout)

        setSupportActionBar(binding.toolbar)
        setupActionBarWithNavController(navController, appbarConfiguration)

        binding.navigationView.setupWithNavController(navController)
    }

    override fun onBackPressed() {
        if(drawerLayout.isDrawerOpen(drawerLayoutGravity))
            drawerLayout.closeDrawer(drawerLayoutGravity)
        else
            super.onBackPressed()
    }

    override fun onSupportNavigateUp(): Boolean {
        return navController.navigateUp(appbarConfiguration) || super.onSupportNavigateUp()
    }


    companion object {
        fun newIntent(context: Context) : Intent {
            return Intent(context, MainActivity::class.java)
        }

        private const val drawerLayoutGravity : Int = GravityCompat.START
    }
}