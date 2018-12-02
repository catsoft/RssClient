package asura.com.rssclient.stated

import androidx.core.view.isGone
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import kotlinx.android.synthetic.main.stated_layout.*

abstract class StatedFragment : Fragment() {

    fun subscribeOnState(viewModel: StateViewModel) {
        viewModel.getNormalData().observe(viewLifecycleOwner, Observer {
            normal_state_block.isGone = it == null
        })

        viewModel.getLoadData().observe(viewLifecycleOwner, Observer {
            load_state_block.isGone = it == null
        })

        viewModel.getInvalidData().observe(viewLifecycleOwner, Observer {
            invalid_state_block.isGone = it == null
        })
    }
}