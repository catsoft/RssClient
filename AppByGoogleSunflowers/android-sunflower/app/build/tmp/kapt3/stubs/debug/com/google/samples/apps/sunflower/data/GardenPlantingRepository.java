package com.google.samples.apps.sunflower.data;

import java.lang.System;

@kotlin.Metadata(mv = {1, 1, 13}, bv = {1, 0, 3}, k = 1, d1 = {"\u00006\n\u0002\u0018\u0002\n\u0002\u0010\u0000\n\u0000\n\u0002\u0018\u0002\n\u0002\b\u0002\n\u0002\u0010\u0002\n\u0000\n\u0002\u0010\u000e\n\u0000\n\u0002\u0018\u0002\n\u0002\u0018\u0002\n\u0000\n\u0002\u0010 \n\u0000\n\u0002\u0018\u0002\n\u0002\b\u0004\u0018\u0000 \u00122\u00020\u0001:\u0001\u0012B\u000f\b\u0002\u0012\u0006\u0010\u0002\u001a\u00020\u0003\u00a2\u0006\u0002\u0010\u0004J\u000e\u0010\u0005\u001a\u00020\u00062\u0006\u0010\u0007\u001a\u00020\bJ\u0014\u0010\t\u001a\b\u0012\u0004\u0012\u00020\u000b0\n2\u0006\u0010\u0007\u001a\u00020\bJ\u0012\u0010\f\u001a\u000e\u0012\n\u0012\b\u0012\u0004\u0012\u00020\u000b0\r0\nJ\u0012\u0010\u000e\u001a\u000e\u0012\n\u0012\b\u0012\u0004\u0012\u00020\u000f0\r0\nJ\u000e\u0010\u0010\u001a\u00020\u00062\u0006\u0010\u0011\u001a\u00020\u000bR\u000e\u0010\u0002\u001a\u00020\u0003X\u0082\u0004\u00a2\u0006\u0002\n\u0000\u00a8\u0006\u0013"}, d2 = {"Lcom/google/samples/apps/sunflower/data/GardenPlantingRepository;", "", "gardenPlantingDao", "Lcom/google/samples/apps/sunflower/data/GardenPlantingDao;", "(Lcom/google/samples/apps/sunflower/data/GardenPlantingDao;)V", "createGardenPlanting", "", "plantId", "", "getGardenPlantingForPlant", "Landroidx/lifecycle/LiveData;", "Lcom/google/samples/apps/sunflower/data/GardenPlanting;", "getGardenPlantings", "", "getPlantAndGardenPlantings", "Lcom/google/samples/apps/sunflower/data/PlantAndGardenPlantings;", "removeGardenPlanting", "gardenPlanting", "Companion", "app_debug"})
public final class GardenPlantingRepository {
    private final com.google.samples.apps.sunflower.data.GardenPlantingDao gardenPlantingDao = null;
    private static volatile com.google.samples.apps.sunflower.data.GardenPlantingRepository instance;
    public static final com.google.samples.apps.sunflower.data.GardenPlantingRepository.Companion Companion = null;
    
    public final void createGardenPlanting(@org.jetbrains.annotations.NotNull()
    java.lang.String plantId) {
    }
    
    public final void removeGardenPlanting(@org.jetbrains.annotations.NotNull()
    com.google.samples.apps.sunflower.data.GardenPlanting gardenPlanting) {
    }
    
    @org.jetbrains.annotations.NotNull()
    public final androidx.lifecycle.LiveData<com.google.samples.apps.sunflower.data.GardenPlanting> getGardenPlantingForPlant(@org.jetbrains.annotations.NotNull()
    java.lang.String plantId) {
        return null;
    }
    
    @org.jetbrains.annotations.NotNull()
    public final androidx.lifecycle.LiveData<java.util.List<com.google.samples.apps.sunflower.data.GardenPlanting>> getGardenPlantings() {
        return null;
    }
    
    @org.jetbrains.annotations.NotNull()
    public final androidx.lifecycle.LiveData<java.util.List<com.google.samples.apps.sunflower.data.PlantAndGardenPlantings>> getPlantAndGardenPlantings() {
        return null;
    }
    
    private GardenPlantingRepository(com.google.samples.apps.sunflower.data.GardenPlantingDao gardenPlantingDao) {
        super();
    }
    
    @kotlin.Metadata(mv = {1, 1, 13}, bv = {1, 0, 3}, k = 1, d1 = {"\u0000\u001a\n\u0002\u0018\u0002\n\u0002\u0010\u0000\n\u0002\b\u0002\n\u0002\u0018\u0002\n\u0002\b\u0002\n\u0002\u0018\u0002\n\u0000\b\u0086\u0003\u0018\u00002\u00020\u0001B\u0007\b\u0002\u00a2\u0006\u0002\u0010\u0002J\u000e\u0010\u0005\u001a\u00020\u00042\u0006\u0010\u0006\u001a\u00020\u0007R\u0010\u0010\u0003\u001a\u0004\u0018\u00010\u0004X\u0082\u000e\u00a2\u0006\u0002\n\u0000\u00a8\u0006\b"}, d2 = {"Lcom/google/samples/apps/sunflower/data/GardenPlantingRepository$Companion;", "", "()V", "instance", "Lcom/google/samples/apps/sunflower/data/GardenPlantingRepository;", "getInstance", "gardenPlantingDao", "Lcom/google/samples/apps/sunflower/data/GardenPlantingDao;", "app_debug"})
    public static final class Companion {
        
        @org.jetbrains.annotations.NotNull()
        public final com.google.samples.apps.sunflower.data.GardenPlantingRepository getInstance(@org.jetbrains.annotations.NotNull()
        com.google.samples.apps.sunflower.data.GardenPlantingDao gardenPlantingDao) {
            return null;
        }
        
        private Companion() {
            super();
        }
    }
}