package asura.com.rssclient.stated

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import asura.com.rssclient.utilites.runOnIoThread

abstract class StatedViewModel : ViewModel() {

    private var loadData: MutableLiveData<LoadStateData> = MutableLiveData()
    private var invalidData: MutableLiveData<InvalidStateData> = MutableLiveData()
    private var normalData: MutableLiveData<NormalStateData> = MutableLiveData()

    fun getLoadData(): LiveData<LoadStateData> = loadData

    fun getInvalidData(): LiveData<InvalidStateData> = invalidData

    fun getNormalData(): LiveData<NormalStateData> = normalData

    fun setNormalState(normalStateData: NormalStateData) {
        normalData.value = normalStateData
        loadData.value = null
        invalidData.value = null
    }

    fun setLoadState(loadStateData: LoadStateData) {
        normalData.value = null
        loadData.value = loadStateData
        invalidData.value = null
    }

    fun setInvalidState(errorStateData: InvalidStateData) {
        normalData.value = null
        loadData.value = null
        invalidData.value = errorStateData
    }
}