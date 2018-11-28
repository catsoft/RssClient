package com.google.samples.apps.sunflower.databinding;
import com.google.samples.apps.sunflower.R;
import com.google.samples.apps.sunflower.BR;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import android.view.View;
@SuppressWarnings("unchecked")
public class FragmentPlantDetailBindingImpl extends FragmentPlantDetailBinding  {

    @Nullable
    private static final androidx.databinding.ViewDataBinding.IncludedLayouts sIncludes;
    @Nullable
    private static final android.util.SparseIntArray sViewsWithIds;
    static {
        sIncludes = null;
        sViewsWithIds = new android.util.SparseIntArray();
        sViewsWithIds.put(R.id.appbar, 6);
        sViewsWithIds.put(R.id.detail_toolbar, 7);
        sViewsWithIds.put(R.id.plant_detail_scrollview, 8);
    }
    // views
    @NonNull
    private final androidx.coordinatorlayout.widget.CoordinatorLayout mboundView0;
    // variables
    // values
    // listeners
    // Inverse Binding Event Handlers

    public FragmentPlantDetailBindingImpl(@Nullable androidx.databinding.DataBindingComponent bindingComponent, @NonNull View root) {
        this(bindingComponent, root, mapBindings(bindingComponent, root, 9, sIncludes, sViewsWithIds));
    }
    private FragmentPlantDetailBindingImpl(androidx.databinding.DataBindingComponent bindingComponent, View root, Object[] bindings) {
        super(bindingComponent, root, 2
            , (com.google.android.material.appbar.AppBarLayout) bindings[6]
            , (android.widget.ImageView) bindings[2]
            , (androidx.appcompat.widget.Toolbar) bindings[7]
            , (com.google.android.material.floatingactionbutton.FloatingActionButton) bindings[5]
            , (android.widget.TextView) bindings[4]
            , (androidx.core.widget.NestedScrollView) bindings[8]
            , (android.widget.TextView) bindings[3]
            , (com.google.android.material.appbar.CollapsingToolbarLayout) bindings[1]
            );
        this.detailImage.setTag(null);
        this.fab.setTag(null);
        this.mboundView0 = (androidx.coordinatorlayout.widget.CoordinatorLayout) bindings[0];
        this.mboundView0.setTag(null);
        this.plantDetail.setTag(null);
        this.plantWatering.setTag(null);
        this.toolbarLayout.setTag(null);
        setRootTag(root);
        // listeners
        invalidateAll();
    }

    @Override
    public void invalidateAll() {
        synchronized(this) {
                mDirtyFlags = 0x8L;
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
            setViewModel((com.google.samples.apps.sunflower.viewmodels.PlantDetailViewModel) variable);
        }
        else {
            variableSet = false;
        }
            return variableSet;
    }

    public void setViewModel(@Nullable com.google.samples.apps.sunflower.viewmodels.PlantDetailViewModel ViewModel) {
        this.mViewModel = ViewModel;
        synchronized(this) {
            mDirtyFlags |= 0x4L;
        }
        notifyPropertyChanged(BR.viewModel);
        super.requestRebind();
    }

    @Override
    protected boolean onFieldChange(int localFieldId, Object object, int fieldId) {
        switch (localFieldId) {
            case 0 :
                return onChangeViewModelPlant((androidx.lifecycle.LiveData<com.google.samples.apps.sunflower.data.Plant>) object, fieldId);
            case 1 :
                return onChangeViewModelIsPlanted((androidx.lifecycle.LiveData<java.lang.Boolean>) object, fieldId);
        }
        return false;
    }
    private boolean onChangeViewModelPlant(androidx.lifecycle.LiveData<com.google.samples.apps.sunflower.data.Plant> ViewModelPlant, int fieldId) {
        if (fieldId == BR._all) {
            synchronized(this) {
                    mDirtyFlags |= 0x1L;
            }
            return true;
        }
        return false;
    }
    private boolean onChangeViewModelIsPlanted(androidx.lifecycle.LiveData<java.lang.Boolean> ViewModelIsPlanted, int fieldId) {
        if (fieldId == BR._all) {
            synchronized(this) {
                    mDirtyFlags |= 0x2L;
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
        androidx.lifecycle.LiveData<com.google.samples.apps.sunflower.data.Plant> viewModelPlant = null;
        androidx.lifecycle.LiveData<java.lang.Boolean> viewModelIsPlanted = null;
        java.lang.String viewModelPlantName = null;
        java.lang.String viewModelPlantImageUrl = null;
        java.lang.Boolean viewModelIsPlantedGetValue = null;
        com.google.samples.apps.sunflower.data.Plant viewModelPlantGetValue = null;
        int viewModelPlantWateringInterval = 0;
        java.lang.String viewModelPlantDescription = null;
        com.google.samples.apps.sunflower.viewmodels.PlantDetailViewModel viewModel = mViewModel;

        if ((dirtyFlags & 0xfL) != 0) {


            if ((dirtyFlags & 0xdL) != 0) {

                    if (viewModel != null) {
                        // read viewModel.plant
                        viewModelPlant = viewModel.getPlant();
                    }
                    updateLiveDataRegistration(0, viewModelPlant);


                    if (viewModelPlant != null) {
                        // read viewModel.plant.getValue()
                        viewModelPlantGetValue = viewModelPlant.getValue();
                    }


                    if (viewModelPlantGetValue != null) {
                        // read viewModel.plant.getValue().name
                        viewModelPlantName = viewModelPlantGetValue.getName();
                        // read viewModel.plant.getValue().imageUrl
                        viewModelPlantImageUrl = viewModelPlantGetValue.getImageUrl();
                        // read viewModel.plant.getValue().wateringInterval
                        viewModelPlantWateringInterval = viewModelPlantGetValue.getWateringInterval();
                        // read viewModel.plant.getValue().description
                        viewModelPlantDescription = viewModelPlantGetValue.getDescription();
                    }
            }
            if ((dirtyFlags & 0xeL) != 0) {

                    if (viewModel != null) {
                        // read viewModel.isPlanted
                        viewModelIsPlanted = viewModel.isPlanted();
                    }
                    updateLiveDataRegistration(1, viewModelIsPlanted);


                    if (viewModelIsPlanted != null) {
                        // read viewModel.isPlanted.getValue()
                        viewModelIsPlantedGetValue = viewModelIsPlanted.getValue();
                    }
            }
        }
        // batch finished
        if ((dirtyFlags & 0xdL) != 0) {
            // api target 1

            com.google.samples.apps.sunflower.adapters.PlantDetailBindingAdaptersKt.bindImageFromUrl(this.detailImage, viewModelPlantImageUrl);
            androidx.databinding.adapters.TextViewBindingAdapter.setText(this.plantDetail, viewModelPlantDescription);
            com.google.samples.apps.sunflower.adapters.PlantDetailBindingAdaptersKt.bindWateringText(this.plantWatering, viewModelPlantWateringInterval);
            this.toolbarLayout.setTitle(viewModelPlantName);
        }
        if ((dirtyFlags & 0xeL) != 0) {
            // api target 1

            com.google.samples.apps.sunflower.adapters.PlantDetailBindingAdaptersKt.bindIsGone(this.fab, viewModelIsPlantedGetValue);
        }
    }
    // Listener Stub Implementations
    // callback impls
    // dirty flag
    private  long mDirtyFlags = 0xffffffffffffffffL;
    /* flag mapping
        flag 0 (0x1L): viewModel.plant
        flag 1 (0x2L): viewModel.isPlanted
        flag 2 (0x3L): viewModel
        flag 3 (0x4L): null
    flag mapping end*/
    //end
}