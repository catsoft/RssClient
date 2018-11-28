package com.google.samples.apps.sunflower.utilities;

import java.lang.System;

/**
 * * Static methods used to inject classes needed for various Activities and Fragments.
 */
@kotlin.Metadata(mv = {1, 1, 13}, bv = {1, 0, 3}, k = 1, d1 = {"\u00006\n\u0002\u0018\u0002\n\u0002\u0010\u0000\n\u0002\b\u0002\n\u0002\u0018\u0002\n\u0000\n\u0002\u0018\u0002\n\u0000\n\u0002\u0018\u0002\n\u0000\n\u0002\u0018\u0002\n\u0000\n\u0002\u0018\u0002\n\u0000\n\u0002\u0010\u000e\n\u0000\n\u0002\u0018\u0002\n\u0000\b\u00c6\u0002\u0018\u00002\u00020\u0001B\u0007\b\u0002\u00a2\u0006\u0002\u0010\u0002J\u0010\u0010\u0003\u001a\u00020\u00042\u0006\u0010\u0005\u001a\u00020\u0006H\u0002J\u0010\u0010\u0007\u001a\u00020\b2\u0006\u0010\u0005\u001a\u00020\u0006H\u0002J\u000e\u0010\t\u001a\u00020\n2\u0006\u0010\u0005\u001a\u00020\u0006J\u0016\u0010\u000b\u001a\u00020\f2\u0006\u0010\u0005\u001a\u00020\u00062\u0006\u0010\r\u001a\u00020\u000eJ\u000e\u0010\u000f\u001a\u00020\u00102\u0006\u0010\u0005\u001a\u00020\u0006\u00a8\u0006\u0011"}, d2 = {"Lcom/google/samples/apps/sunflower/utilities/InjectorUtils;", "", "()V", "getGardenPlantingRepository", "Lcom/google/samples/apps/sunflower/data/GardenPlantingRepository;", "context", "Landroid/content/Context;", "getPlantRepository", "Lcom/google/samples/apps/sunflower/data/PlantRepository;", "provideGardenPlantingListViewModelFactory", "Lcom/google/samples/apps/sunflower/viewmodels/GardenPlantingListViewModelFactory;", "providePlantDetailViewModelFactory", "Lcom/google/samples/apps/sunflower/viewmodels/PlantDetailViewModelFactory;", "plantId", "", "providePlantListViewModelFactory", "Lcom/google/samples/apps/sunflower/viewmodels/PlantListViewModelFactory;", "app_debug"})
public final class InjectorUtils {
    public static final com.google.samples.apps.sunflower.utilities.InjectorUtils INSTANCE = null;
    
    private final com.google.samples.apps.sunflower.data.PlantRepository getPlantRepository(android.content.Context context) {
        return null;
    }
    
    private final com.google.samples.apps.sunflower.data.GardenPlantingRepository getGardenPlantingRepository(android.content.Context context) {
        return null;
    }
    
    @org.jetbrains.annotations.NotNull()
    public final com.google.samples.apps.sunflower.viewmodels.GardenPlantingListViewModelFactory provideGardenPlantingListViewModelFactory(@org.jetbrains.annotations.NotNull()
    android.content.Context context) {
        return null;
    }
    
    @org.jetbrains.annotations.NotNull()
    public final com.google.samples.apps.sunflower.viewmodels.PlantListViewModelFactory providePlantListViewModelFactory(@org.jetbrains.annotations.NotNull()
    android.content.Context context) {
        return null;
    }
    
    @org.jetbrains.annotations.NotNull()
    public final com.google.samples.apps.sunflower.viewmodels.PlantDetailViewModelFactory providePlantDetailViewModelFactory(@org.jetbrains.annotations.NotNull()
    android.content.Context context, @org.jetbrains.annotations.NotNull()
    java.lang.String plantId) {
        return null;
    }
    
    private InjectorUtils() {
        super();
    }
}