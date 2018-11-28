package com.google.samples.apps.sunflower.viewmodels;

import java.lang.System;

/**
 * * The ViewModel for [PlantListFragment].
 */
@kotlin.Metadata(mv = {1, 1, 13}, bv = {1, 0, 3}, k = 1, d1 = {"\u0000:\n\u0002\u0018\u0002\n\u0002\u0018\u0002\n\u0000\n\u0002\u0018\u0002\n\u0002\b\u0002\n\u0002\u0018\u0002\n\u0002\u0010\b\n\u0000\n\u0002\u0018\u0002\n\u0002\u0010 \n\u0002\u0018\u0002\n\u0000\n\u0002\u0010\u0002\n\u0002\b\u0002\n\u0002\u0010\u000b\n\u0002\b\u0004\u0018\u0000 \u00132\u00020\u0001:\u0001\u0013B\u000f\b\u0000\u0012\u0006\u0010\u0002\u001a\u00020\u0003\u00a2\u0006\u0002\u0010\u0004J\u0006\u0010\f\u001a\u00020\rJ\u0012\u0010\u000e\u001a\u000e\u0012\n\u0012\b\u0012\u0004\u0012\u00020\u000b0\n0\tJ\u0006\u0010\u000f\u001a\u00020\u0010J\u000e\u0010\u0011\u001a\u00020\r2\u0006\u0010\u0012\u001a\u00020\u0007R\u0014\u0010\u0005\u001a\b\u0012\u0004\u0012\u00020\u00070\u0006X\u0082\u0004\u00a2\u0006\u0002\n\u0000R\u001a\u0010\b\u001a\u000e\u0012\n\u0012\b\u0012\u0004\u0012\u00020\u000b0\n0\tX\u0082\u0004\u00a2\u0006\u0002\n\u0000R\u000e\u0010\u0002\u001a\u00020\u0003X\u0082\u0004\u00a2\u0006\u0002\n\u0000\u00a8\u0006\u0014"}, d2 = {"Lcom/google/samples/apps/sunflower/viewmodels/PlantListViewModel;", "Landroidx/lifecycle/ViewModel;", "plantRepository", "Lcom/google/samples/apps/sunflower/data/PlantRepository;", "(Lcom/google/samples/apps/sunflower/data/PlantRepository;)V", "growZoneNumber", "Landroidx/lifecycle/MutableLiveData;", "", "plantList", "Landroidx/lifecycle/MediatorLiveData;", "", "Lcom/google/samples/apps/sunflower/data/Plant;", "clearGrowZoneNumber", "", "getPlants", "isFiltered", "", "setGrowZoneNumber", "num", "Companion", "app_debug"})
public final class PlantListViewModel extends androidx.lifecycle.ViewModel {
    private final androidx.lifecycle.MutableLiveData<java.lang.Integer> growZoneNumber = null;
    private final androidx.lifecycle.MediatorLiveData<java.util.List<com.google.samples.apps.sunflower.data.Plant>> plantList = null;
    private final com.google.samples.apps.sunflower.data.PlantRepository plantRepository = null;
    private static final int NO_GROW_ZONE = -1;
    public static final com.google.samples.apps.sunflower.viewmodels.PlantListViewModel.Companion Companion = null;
    
    @org.jetbrains.annotations.NotNull()
    public final androidx.lifecycle.MediatorLiveData<java.util.List<com.google.samples.apps.sunflower.data.Plant>> getPlants() {
        return null;
    }
    
    public final void setGrowZoneNumber(int num) {
    }
    
    public final void clearGrowZoneNumber() {
    }
    
    public final boolean isFiltered() {
        return false;
    }
    
    public PlantListViewModel(@org.jetbrains.annotations.NotNull()
    com.google.samples.apps.sunflower.data.PlantRepository plantRepository) {
        super();
    }
    
    @kotlin.Metadata(mv = {1, 1, 13}, bv = {1, 0, 3}, k = 1, d1 = {"\u0000\u0012\n\u0002\u0018\u0002\n\u0002\u0010\u0000\n\u0002\b\u0002\n\u0002\u0010\b\n\u0000\b\u0086\u0003\u0018\u00002\u00020\u0001B\u0007\b\u0002\u00a2\u0006\u0002\u0010\u0002R\u000e\u0010\u0003\u001a\u00020\u0004X\u0082T\u00a2\u0006\u0002\n\u0000\u00a8\u0006\u0005"}, d2 = {"Lcom/google/samples/apps/sunflower/viewmodels/PlantListViewModel$Companion;", "", "()V", "NO_GROW_ZONE", "", "app_debug"})
    public static final class Companion {
        
        private Companion() {
            super();
        }
    }
}