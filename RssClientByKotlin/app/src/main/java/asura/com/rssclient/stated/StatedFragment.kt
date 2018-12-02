package asura.com.rssclient.stated

import androidx.core.view.isGone
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import kotlinx.android.synthetic.main.stated_error.*
import kotlinx.android.synthetic.main.stated_layout.*
import kotlinx.android.synthetic.main.stated_load.*

abstract class StatedFragment : Fragment() {

    fun subscribeOnState(viewModel: StatedViewModel) {
        viewModel.getNormalData().observe(viewLifecycleOwner, Observer {
            normal_state_block.isGone = it == null
        })

        viewModel.getLoadData().observe(viewLifecycleOwner, Observer {
            load_state_block.isGone = it == null
            it?.let {
                stated_load_text.text = it.text ?: "Loading data, please wait"
            }
        })

        viewModel.getInvalidData().observe(viewLifecycleOwner, Observer {
            invalid_state_block.isGone = it == null
            it?.let {
                state_error_text_view.text = it.data
            }
        })
    }
}