package com.google.samples.apps.sunflower.databinding;
import com.google.samples.apps.sunflower.R;
import com.google.samples.apps.sunflower.BR;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import android.view.View;
@SuppressWarnings("unchecked")
public class FragmentGardenBindingImpl extends FragmentGardenBinding  {

    @Nullable
    private static final androidx.databinding.ViewDataBinding.IncludedLayouts sIncludes;
    @Nullable
    private static final android.util.SparseIntArray sViewsWithIds;
    static {
        sIncludes = null;
        sViewsWithIds = null;
    }
    // views
    @NonNull
    private final android.widget.FrameLayout mboundView0;
    // variables
    // values
    // listeners
    // Inverse Binding Event Handlers

    public FragmentGardenBindingImpl(@Nullable androidx.databinding.DataBindingComponent bindingComponent, @NonNull View root) {
        this(bindingComponent, root, mapBindings(bindingComponent, root, 3, sIncludes, sViewsWithIds));
    }
    private FragmentGardenBindingImpl(androidx.databinding.DataBindingComponent bindingComponent, View root, Object[] bindings) {
        super(bindingComponent, root, 0
            , (android.widget.TextView) bindings[2]
            , (androidx.recyclerview.widget.RecyclerView) bindings[1]
            );
        this.emptyGarden.setTag(null);
        this.gardenList.setTag(null);
        this.mboundView0 = (android.widget.FrameLayout) bindings[0];
        this.mboundView0.setTag(null);
        setRootTag(root);
        // listeners
        invalidateAll();
    }

    @Override
    public void invalidateAll() {
        synchronized(this) {
                mDirtyFlags = 0x2L;
        }
        requestRebind();
    }

    @Override
    public boolean hasPendingBindings() {
        synchronized(this) {
            if (mDirtyFlags != 0) {
                return true;
            }
        }
        return false;
    }

    @Override
    public boolean setVariable(int variableId, @Nullable Object variable)  {
        boolean variableSet = true;
        if (BR.hasPlantings == variableId) {
            setHasPlantings((java.lang.Boolean) variable);
        }
        else {
            variableSet = false;
        }
            return variableSet;
    }

    public void setHasPlantings(@Nullable java.lang.Boolean HasPlantings) {
        this.mHasPlantings = HasPlantings;
        synchronized(this) {
            mDirtyFlags |= 0x1L;
        }
        notifyPropertyChanged(BR.hasPlantings);
        super.requestRebind();
    }

    @Override
    protected boolean onFieldChange(int localFieldId, Object object, int fieldId) {
        switch (localFieldId) {
        }
        return false;
    }

    @Override
    protected void executeBindings() {
        long dirtyFlags = 0;
        synchronized(this) {
            dirtyFlags = mDirtyFlags;
            mDirtyFlags = 0;
        }
        boolean androidxDatabindingViewDataBindingSafeUnboxHasPlantings = false;
        java.lang.Boolean hasPlantings = mHasPlantings;
        boolean HasPlantings1 = false;
        boolean AndroidxDatabindingViewDataBindingSafeUnboxHasPlantings1 = false;

        if ((dirtyFlags & 0x3L) != 0) {



                // read androidx.databinding.ViewDataBinding.safeUnbox(hasPlantings)
                AndroidxDatabindingViewDataBindingSafeUnboxHasPlantings1 = androidx.databinding.ViewDataBinding.safeUnbox(hasPlantings);


                // read !androidx.databinding.ViewDataBinding.safeUnbox(hasPlantings)
                HasPlantings1 = !AndroidxDatabindingViewDataBindingSafeUnboxHasPlantings1;


                // read androidx.databinding.ViewDataBinding.safeUnbox(!androidx.databinding.ViewDataBinding.safeUnbox(hasPlantings))
                androidxDatabindingViewDataBindingSafeUnboxHasPlantings = androidx.databinding.ViewDataBinding.safeUnbox(HasPlantings1);
        }
        // batch finished
        if ((dirtyFlags & 0x3L) != 0) {
            // api target 1

            com.google.samples.apps.sunflower.adapters.BindingAdaptersKt.bindIsGone(this.emptyGarden, AndroidxDatabindingViewDataBindingSafeUnboxHasPlantings1);
            com.google.samples.apps.sunflower.adapters.BindingAdaptersKt.bindIsGone(this.gardenList, androidxDatabindingViewDataBindingSafeUnboxHasPlantings);
        }
    }
    // Listener Stub Implementations
    // callback impls
    // dirty flag
    private  long mDirtyFlags = 0xffffffffffffffffL;
    /* flag mapping
        flag 0 (0x1L): hasPlantings
        flag 1 (0x2L): null
    flag mapping end*/
    //end
}