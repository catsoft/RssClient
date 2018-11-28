package com.google.samples.apps.sunflower.viewmodels;

import java.lang.System;

/**
 * * The ViewModel used in [PlantDetailFragment].
 */
@kotlin.Metadata(mv = {1, 1, 13}, bv = {1, 0, 3}, k = 1, d1 = {"\u00008\n\u0002\u0018\u0002\n\u0002\u0018\u0002\n\u0000\n\u0002\u0018\u0002\n\u0000\n\u0002\u0018\u0002\n\u0000\n\u0002\u0010\u000e\n\u0002\b\u0002\n\u0002\u0018\u0002\n\u0002\u0010\u000b\n\u0002\b\u0002\n\u0002\u0018\u0002\n\u0002\b\u0002\n\u0002\u0010\u0002\n\u0000\u0018\u00002\u00020\u0001B\u001d\u0012\u0006\u0010\u0002\u001a\u00020\u0003\u0012\u0006\u0010\u0004\u001a\u00020\u0005\u0012\u0006\u0010\u0006\u001a\u00020\u0007\u00a2\u0006\u0002\u0010\bJ\u0006\u0010\u0010\u001a\u00020\u0011R\u000e\u0010\u0004\u001a\u00020\u0005X\u0082\u0004\u00a2\u0006\u0002\n\u0000R\u0017\u0010\t\u001a\b\u0012\u0004\u0012\u00020\u000b0\n\u00a2\u0006\b\n\u0000\u001a\u0004\b\t\u0010\fR\u0017\u0010\r\u001a\b\u0012\u0004\u0012\u00020\u000e0\n\u00a2\u0006\b\n\u0000\u001a\u0004\b\u000f\u0010\fR\u000e\u0010\u0006\u001a\u00020\u0007X\u0082\u0004\u00a2\u0006\u0002\n\u0000\u00a8\u0006\u0012"}, d2 = {"Lcom/google/samples/apps/sunflower/viewmodels/PlantDetailViewModel;", "Landroidx/lifecycle/ViewModel;", "plantRepository", "Lcom/google/samples/apps/sunflower/data/PlantRepository;", "gardenPlantingRepository", "Lcom/google/samples/apps/sunflower/data/GardenPlantingRepository;", "plantId", "", "(Lcom/google/samples/apps/sunflower/data/PlantRepository;Lcom/google/samples/apps/sunflower/data/GardenPlantingRepository;Ljava/lang/String;)V", "isPlanted", "Landroidx/lifecycle/LiveData;", "", "()Landroidx/lifecycle/LiveData;", "plant", "Lcom/google/samples/apps/sunflower/data/Plant;", "getPlant", "addPlantToGarden", "", "app_debug"})
public final class PlantDetailViewModel extends androidx.lifecycle.ViewModel {
    @org.jetbrains.annotations.NotNull()
    private final androidx.lifecycle.LiveData<java.lang.Boolean> isPlanted = null;
    @org.jetbrains.annotations.NotNull()
    private final androidx.lifecycle.LiveData<com.google.samples.apps.sunflower.data.Plant> plant = null;
    private final com.google.samples.apps.sunflower.data.GardenPlantingRepository gardenPlantingRepository = null;
    private final java.lang.String plantId = null;
    
    @org.jetbrains.annotations.NotNull()
    public final androidx.lifecycle.LiveData<java.lang.Boolean> isPlanted() {
        return null;
    }
    
    @org.jetbrains.annotations.NotNull()
    public final androidx.lifecycle.LiveData<com.google.samples.apps.sunflower.data.Plant> getPlant() {
        return null;
    }
    
    public final void addPlantToGarden() {
    }
    
    public PlantDetailViewModel(@org.jetbrains.annotations.NotNull()
    com.google.samples.apps.sunflower.data.PlantRepository plantRepository, @org.jetbrains.annotations.NotNull()
    com.google.samples.apps.sunflower.data.GardenPlantingRepository gardenPlantingRepository, @org.jetbrains.annotations.NotNull()
    java.lang.String plantId) {
        super();
    }
}