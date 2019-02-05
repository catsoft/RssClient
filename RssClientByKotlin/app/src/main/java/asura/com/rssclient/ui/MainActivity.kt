package asura.com.rssclient.ui

import android.content.Context
import android.content.Intent
import android.os.Bundle
import android.view.MenuItem
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
import com.google.android.material.navigation.NavigationView
import kotlinx.android.synthetic.main.activity_rss.*

class MainActivity : AppCompatActivity(), NavigationView.OnNavigationItemSelectedListener {

    private lateinit var drawerLayout : DrawerLayout
    private lateinit var navController : NavController
    private lateinit var appbarConfiguration : AppBarConfiguration

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        val binding: ActivityRssBinding = DataBindingUtil.setContentView(this, R.layout.activity_rss)

        drawerLayout = binding.drawerLayout

        navController = Navigation.findNavController(this, R.id.rss_list_nav_fragment)
        appbarConfiguration = AppBarConfiguration(navController.graph, drawerLayout)

        setSupportActionBar(binding.toolbar)
        setupActionBarWithNavController(navController, appbarConfiguration)

        binding.navigationView.setupWithNavController(navController)

        navigation_view.setNavigationItemSelectedListener(this)
        navigation_view.setCheckedItem(R.id.menu_navigation_home)
    }

    override fun onNavigationItemSelected(p0: MenuItem): Boolean {

        when (p0.itemId) {
            R.id.menu_navigation_home -> {

            }
            R.id.menu_navigation_recommendation -> {

            }
            R.id.menu_navigation_settings -> {

            }
            R.id.menu_navigation_contacts -> {

            }
            R.id.menu_navigation_about -> {

            }
        }

        navigation_view.setCheckedItem(p0.itemId)

        return true
    }

    override fun onBackPressed() {
        if(drawerLayout.isDrawerOpen(drawerLayoutGravity))
            drawerLayout.closeDrawer(drawerLayoutGravity)
        else
            onSupportNavigateUp()
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