package com.google.samples.apps.sunflower.adapters;

import java.lang.System;

/**
 * * Adapter for the [RecyclerView] in [PlantListFragment].
 */
@kotlin.Metadata(mv = {1, 1, 13}, bv = {1, 0, 3}, k = 1, d1 = {"\u00008\n\u0002\u0018\u0002\n\u0002\u0018\u0002\n\u0002\u0018\u0002\n\u0002\u0018\u0002\n\u0002\b\u0002\n\u0002\u0018\u0002\n\u0000\n\u0002\u0010\u000e\n\u0000\n\u0002\u0010\u0002\n\u0002\b\u0002\n\u0002\u0010\b\n\u0002\b\u0002\n\u0002\u0018\u0002\n\u0002\b\u0003\u0018\u00002\u000e\u0012\u0004\u0012\u00020\u0002\u0012\u0004\u0012\u00020\u00030\u0001:\u0001\u0012B\u0005\u00a2\u0006\u0002\u0010\u0004J\u0010\u0010\u0005\u001a\u00020\u00062\u0006\u0010\u0007\u001a\u00020\bH\u0002J\u0018\u0010\t\u001a\u00020\n2\u0006\u0010\u000b\u001a\u00020\u00032\u0006\u0010\f\u001a\u00020\rH\u0016J\u0018\u0010\u000e\u001a\u00020\u00032\u0006\u0010\u000f\u001a\u00020\u00102\u0006\u0010\u0011\u001a\u00020\rH\u0016\u00a8\u0006\u0013"}, d2 = {"Lcom/google/samples/apps/sunflower/adapters/PlantAdapter;", "Landroidx/recyclerview/widget/ListAdapter;", "Lcom/google/samples/apps/sunflower/data/Plant;", "Lcom/google/samples/apps/sunflower/adapters/PlantAdapter$ViewHolder;", "()V", "createOnClickListener", "Landroid/view/View$OnClickListener;", "plantId", "", "onBindViewHolder", "", "holder", "position", "", "onCreateViewHolder", "parent", "Landroid/view/ViewGroup;", "viewType", "ViewHolder", "app_debug"})
public final class PlantAdapter extends androidx.recyclerview.widget.ListAdapter<com.google.samples.apps.sunflower.data.Plant, com.google.samples.apps.sunflower.adapters.PlantAdapter.ViewHolder> {
    
    @java.lang.Override()
    public void onBindViewHolder(@org.jetbrains.annotations.NotNull()
    com.google.samples.apps.sunflower.adapters.PlantAdapter.ViewHolder holder, int position) {
    }
    
    @org.jetbrains.annotations.NotNull()
    @java.lang.Override()
    public com.google.samples.apps.sunflower.adapters.PlantAdapter.ViewHolder onCreateViewHolder(@org.jetbrains.annotations.NotNull()
    android.view.ViewGroup parent, int viewType) {
        return null;
    }
    
    private final android.view.View.OnClickListener createOnClickListener(java.lang.String plantId) {
        return null;
    }
    
    public PlantAdapter() {
        super(null);
    }
    
    @kotlin.Metadata(mv = {1, 1, 13}, bv = {1, 0, 3}, k = 1, d1 = {"\u0000$\n\u0002\u0018\u0002\n\u0002\u0018\u0002\n\u0000\n\u0002\u0018\u0002\n\u0002\b\u0002\n\u0002\u0010\u0002\n\u0000\n\u0002\u0018\u0002\n\u0000\n\u0002\u0018\u0002\n\u0000\u0018\u00002\u00020\u0001B\r\u0012\u0006\u0010\u0002\u001a\u00020\u0003\u00a2\u0006\u0002\u0010\u0004J\u0016\u0010\u0005\u001a\u00020\u00062\u0006\u0010\u0007\u001a\u00020\b2\u0006\u0010\t\u001a\u00020\nR\u000e\u0010\u0002\u001a\u00020\u0003X\u0082\u0004\u00a2\u0006\u0002\n\u0000\u00a8\u0006\u000b"}, d2 = {"Lcom/google/samples/apps/sunflower/adapters/PlantAdapter$ViewHolder;", "Landroidx/recyclerview/widget/RecyclerView$ViewHolder;", "binding", "Lcom/google/samples/apps/sunflower/databinding/ListItemPlantBinding;", "(Lcom/google/samples/apps/sunflower/databinding/ListItemPlantBinding;)V", "bind", "", "listener", "Landroid/view/View$OnClickListener;", "item", "Lcom/google/samples/apps/sunflower/data/Plant;", "app_debug"})
    public static final class ViewHolder extends androidx.recyclerview.widget.RecyclerView.ViewHolder {
        private final com.google.samples.apps.sunflower.databinding.ListItemPlantBinding binding = null;
        
        public final void bind(@org.jetbrains.annotations.NotNull()
        android.view.View.OnClickListener listener, @org.jetbrains.annotations.NotNull()
        com.google.samples.apps.sunflower.data.Plant item) {
        }
        
        public ViewHolder(@org.jetbrains.annotations.NotNull()
        com.google.samples.apps.sunflower.databinding.ListItemPlantBinding binding) {
            super(null);
        }
    }
}