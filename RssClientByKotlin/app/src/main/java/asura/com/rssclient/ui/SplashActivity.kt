package asura.com.rssclient.ui

import android.app.Activity
import android.os.Bundle

class SplashActivity : Activity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        startActivity(RssActivity.newIntent(this))
    }
}