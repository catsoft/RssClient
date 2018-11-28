package com.google.samples.apps.sunflower.databinding;
import com.google.samples.apps.sunflower.R;
import com.google.samples.apps.sunflower.BR;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import android.view.View;
@SuppressWarnings("unchecked")
public class ListItemGardenPlantingBindingImpl extends ListItemGardenPlantingBinding  {

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
    private final androidx.constraintlayout.widget.ConstraintLayout mboundView0;
    // variables
    // values
    // listeners
    // Inverse Binding Event Handlers

    public ListItemGardenPlantingBindingImpl(@Nullable androidx.databinding.DataBindingComponent bindingComponent, @NonNull View root) {
        this(bindingComponent, root, mapBindings(bindingComponent, root, 4, sIncludes, sViewsWithIds));
    }
    private ListItemGardenPlantingBindingImpl(androidx.databinding.DataBindingComponent bindingComponent, View root, Object[] bindings) {
        super(bindingComponent, root, 3
            , (android.widget.ImageView) bindings[1]
            , (android.widget.TextView) bindings[2]
            , (android.widget.TextView) bindings[3]
            );
        this.imageView.setTag(null);
        this.mboundView0 = (androidx.constraintlayout.widget.ConstraintLayout) bindings[0];
        this.mboundView0.setTag(null);
        this.plantDate.setTag(null);
        this.waterDate.setTag(null);
        setRootTag(root);
        // listeners
        invalidateAll();
    }

    @Override
    public void invalidateAll() {
        synchronized(this) {
                mDirtyFlags = 0x10L;
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
        if (BR.viewModel == variableId) {
            setViewModel((com.google.samples.apps.sunflower.viewmodels.PlantAndGardenPlantingsViewModel) variable);
        }
        else {
            variableSet = false;
        }
            return variableSet;
    }

    public void setViewModel(@Nullable com.google.samples.apps.sunflower.viewmodels.PlantAndGardenPlantingsViewModel ViewModel) {
        this.mViewModel = ViewModel;
        synchronized(this) {
            mDirtyFlags |= 0x8L;
        }
        notifyPropertyChanged(BR.viewModel);
        super.requestRebind();
    }

    @Override
    protected boolean onFieldChange(int localFieldId, Object object, int fieldId) {
        switch (localFieldId) {
            case 0 :
                return onChangeViewModelWaterDate((androidx.databinding.ObservableField<java.lang.String>) object, fieldId);
            case 1 :
                return onChangeViewModelImageUrl((androidx.databinding.ObservableField<java.lang.String>) object, fieldId);
            case 2 :
                return onChangeViewModelPlantDate((androidx.databinding.ObservableField<java.lang.String>) object, fieldId);
        }
        return false;
    }
    private boolean onChangeViewModelWaterDate(androidx.databinding.ObservableField<java.lang.String> ViewModelWaterDate, int fieldId) {
        if (fieldId == BR._all) {
            synchronized(this) {
                    mDirtyFlags |= 0x1L;
            }
            return true;
        }
        return false;
    }
    private boolean onChangeViewModelImageUrl(androidx.databinding.ObservableField<java.lang.String> ViewModelImageUrl, int fieldId) {
        if (fieldId == BR._all) {
            synchronized(this) {
                    mDirtyFlags |= 0x2L;
            }
            return true;
        }
        return false;
    }
    private boolean onChangeViewModelPlantDate(androidx.databinding.ObservableField<java.lang.String> ViewModelPlantDate, int fieldId) {
        if (fieldId == BR._all) {
            synchronized(this) {
                    mDirtyFlags |= 0x4L;
            }
            return true;
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
        androidx.databinding.ObservableField<java.lang.String> viewModelWaterDate = null;
        java.lang.String viewModelPlantDateGet = null;
        java.lang.String viewModelWaterDateGet = null;
        java.lang.String viewModelImageUrlGet = null;
        androidx.databinding.ObservableField<java.lang.String> viewModelImageUrl = null;
        androidx.databinding.ObservableField<java.lang.String> viewModelPlantDate = null;
        com.google.samples.apps.sunflower.viewmodels.PlantAndGardenPlantingsViewModel viewModel = mViewModel;

        if ((dirtyFlags & 0x1fL) != 0) {


            if ((dirtyFlags & 0x19L) != 0) {

                    if (viewModel != null) {
                        // read viewModel.waterDate
                        viewModelWaterDate = viewModel.getWaterDate();
                    }
                    updateRegistration(0, viewModelWaterDate);


                    if (viewModelWaterDate != null) {
                        // read viewModel.waterDate.get()
                        viewModelWaterDateGet = viewModelWaterDate.get();
                    }
            }
            if ((dirtyFlags & 0x1aL) != 0) {

                    if (viewModel != null) {
                        // read viewModel.imageUrl
                        viewModelImageUrl = viewModel.getImageUrl();
                    }
                    updateRegistration(1, viewModelImageUrl);


                    if (viewModelImageUrl != null) {
                        // read viewModel.imageUrl.get()
                        viewModelImageUrlGet = viewModelImageUrl.get();
                    }
            }
            if ((dirtyFlags & 0x1cL) != 0) {

                    if (viewModel != null) {
                        // read viewModel.plantDate
                        viewModelPlantDate = viewModel.getPlantDate();
                    }
                    updateRegistration(2, viewModelPlantDate);


                    if (viewModelPlantDate != null) {
                        // read viewModel.plantDate.get()
                        viewModelPlantDateGet = viewModelPlantDate.get();
                    }
            }
        }
        // batch finished
        if ((dirtyFlags & 0x1aL) != 0) {
            // api target 1

            com.google.samples.apps.sunflower.adapters.PlantDetailBindingAdaptersKt.bindImageFromUrl(this.imageView, viewModelImageUrlGet);
        }
        if ((dirtyFlags & 0x1cL) != 0) {
            // api target 1

            androidx.databinding.adapters.TextViewBindingAdapter.setText(this.plantDate, viewModelPlantDateGet);
        }
        if ((dirtyFlags & 0x19L) != 0) {
            // api target 1

            androidx.databinding.adapters.TextViewBindingAdapter.setText(this.waterDate, viewModelWaterDateGet);
        }
    }
    // Listener Stub Implementations
    // callback impls
    // dirty flag
    private  long mDirtyFlags = 0xffffffffffffffffL;
    /* flag mapping
        flag 0 (0x1L): viewModel.waterDate
        flag 1 (0x2L): viewModel.imageUrl
        flag 2 (0x3L): viewModel.plantDate
        flag 3 (0x4L): viewModel
        flag 4 (0x5L): null
    flag mapping end*/
    //end
}