package com.google.samples.apps.sunflower.viewmodels;

import java.lang.System;

/**
 * * Factory for creating a [PlantDetailViewModel] with a constructor that takes a [PlantRepository]
 * * and an ID for the current [Plant].
 */
@kotlin.Metadata(mv = {1, 1, 13}, bv = {1, 0, 3}, k = 1, d1 = {"\u0000,\n\u0002\u0018\u0002\n\u0002\u0018\u0002\n\u0000\n\u0002\u0018\u0002\n\u0000\n\u0002\u0018\u0002\n\u0000\n\u0002\u0010\u000e\n\u0002\b\u0003\n\u0002\u0018\u0002\n\u0000\n\u0002\u0018\u0002\n\u0002\b\u0002\u0018\u00002\u00020\u0001B\u001d\u0012\u0006\u0010\u0002\u001a\u00020\u0003\u0012\u0006\u0010\u0004\u001a\u00020\u0005\u0012\u0006\u0010\u0006\u001a\u00020\u0007\u00a2\u0006\u0002\u0010\bJ%\u0010\t\u001a\u0002H\n\"\b\b\u0000\u0010\n*\u00020\u000b2\f\u0010\f\u001a\b\u0012\u0004\u0012\u0002H\n0\rH\u0016\u00a2\u0006\u0002\u0010\u000eR\u000e\u0010\u0004\u001a\u00020\u0005X\u0082\u0004\u00a2\u0006\u0002\n\u0000R\u000e\u0010\u0006\u001a\u00020\u0007X\u0082\u0004\u00a2\u0006\u0002\n\u0000R\u000e\u0010\u0002\u001a\u00020\u0003X\u0082\u0004\u00a2\u0006\u0002\n\u0000\u00a8\u0006\u000f"}, d2 = {"Lcom/google/samples/apps/sunflower/viewmodels/PlantDetailViewModelFactory;", "Landroidx/lifecycle/ViewModelProvider$NewInstanceFactory;", "plantRepository", "Lcom/google/samples/apps/sunflower/data/PlantRepository;", "gardenPlantingRepository", "Lcom/google/samples/apps/sunflower/data/GardenPlantingRepository;", "plantId", "", "(Lcom/google/samples/apps/sunflower/data/PlantRepository;Lcom/google/samples/apps/sunflower/data/GardenPlantingRepository;Ljava/lang/String;)V", "create", "T", "Landroidx/lifecycle/ViewModel;", "modelClass", "Ljava/lang/Class;", "(Ljava/lang/Class;)Landroidx/lifecycle/ViewModel;", "app_debug"})
public final class PlantDetailViewModelFactory extends androidx.lifecycle.ViewModelProvider.NewInstanceFactory {
    private final com.google.samples.apps.sunflower.data.PlantRepository plantRepository = null;
    private final com.google.samples.apps.sunflower.data.GardenPlantingRepository gardenPlantingRepository = null;
    private final java.lang.String plantId = null;
    
    @org.jetbrains.annotations.NotNull()
    @kotlin.Suppress(names = {"UNCHECKED_CAST"})
    @java.lang.Override()
    public <T extends androidx.lifecycle.ViewModel>T create(@org.jetbrains.annotations.NotNull()
    java.lang.Class<T> modelClass) {
        return null;
    }
    
    public PlantDetailViewModelFactory(@org.jetbrains.annotations.NotNull()
    com.google.samples.apps.sunflower.data.PlantRepository plantRepository, @org.jetbrains.annotations.NotNull()
    com.google.samples.apps.sunflower.data.GardenPlantingRepository gardenPlantingRepository, @org.jetbrains.annotations.NotNull()
    java.lang.String plantId) {
        super();
    }
}