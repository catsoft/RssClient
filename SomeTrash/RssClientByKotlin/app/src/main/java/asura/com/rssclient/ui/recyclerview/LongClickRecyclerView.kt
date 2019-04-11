package asura.com.rssclient.ui.recyclerview

import android.content.Context
import android.util.AttributeSet
import android.view.ContextMenu
import android.view.View
import androidx.recyclerview.widget.RecyclerView

/**
 * RecyclerView with context menu by long click
 */
class LongClickRecyclerView @JvmOverloads constructor(
    context: Context, attrs: AttributeSet? = null, defStyleAttr: Int = 0
) : RecyclerView(context, attrs, defStyleAttr) {

    private var menuInfo: RecyclerContextMenuInfo? = null

    override fun getContextMenuInfo(): ContextMenu.ContextMenuInfo {
        return menuInfo ?: RecyclerContextMenuInfo(109, 109)
    }

    override fun showContextMenuForChild(originalView: View?): Boolean {
        originalView?.let {
            val view = it.parent ?: it
            val longPressPosition = getChildAdapterPosition(view as View)
            if (longPressPosition >= 0) {
                val longPressId = adapter!!.getItemId(longPressPosition)
                menuInfo = RecyclerContextMenuInfo(longPressPosition, longPressId)
                return super.showContextMenuForChild(originalView)
            }
        }

        return false
    }

    open class RecyclerContextMenuInfo(val position: Int, val id: Long) : ContextMenu.ContextMenuInfo
}