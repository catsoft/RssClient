package com.google.samples.apps.sunflower.data;

import java.lang.System;

/**
 * * Repository module for handling data operations.
 */
@kotlin.Metadata(mv = {1, 1, 13}, bv = {1, 0, 3}, k = 1, d1 = {"\u00002\n\u0002\u0018\u0002\n\u0002\u0010\u0000\n\u0000\n\u0002\u0018\u0002\n\u0002\b\u0002\n\u0002\u0018\u0002\n\u0002\u0018\u0002\n\u0000\n\u0002\u0010\u000e\n\u0000\n\u0002\u0010 \n\u0002\b\u0002\n\u0002\u0010\b\n\u0002\b\u0002\u0018\u0000 \u000f2\u00020\u0001:\u0001\u000fB\u000f\b\u0002\u0012\u0006\u0010\u0002\u001a\u00020\u0003\u00a2\u0006\u0002\u0010\u0004J\u0014\u0010\u0005\u001a\b\u0012\u0004\u0012\u00020\u00070\u00062\u0006\u0010\b\u001a\u00020\tJ\u0012\u0010\n\u001a\u000e\u0012\n\u0012\b\u0012\u0004\u0012\u00020\u00070\u000b0\u0006J\u001a\u0010\f\u001a\u000e\u0012\n\u0012\b\u0012\u0004\u0012\u00020\u00070\u000b0\u00062\u0006\u0010\r\u001a\u00020\u000eR\u000e\u0010\u0002\u001a\u00020\u0003X\u0082\u0004\u00a2\u0006\u0002\n\u0000\u00a8\u0006\u0010"}, d2 = {"Lcom/google/samples/apps/sunflower/data/PlantRepository;", "", "plantDao", "Lcom/google/samples/apps/sunflower/data/PlantDao;", "(Lcom/google/samples/apps/sunflower/data/PlantDao;)V", "getPlant", "Landroidx/lifecycle/LiveData;", "Lcom/google/samples/apps/sunflower/data/Plant;", "plantId", "", "getPlants", "", "getPlantsWithGrowZoneNumber", "growZoneNumber", "", "Companion", "app_debug"})
public final class PlantRepository {
    private final com.google.samples.apps.sunflower.data.PlantDao plantDao = null;
    private static volatile com.google.samples.apps.sunflower.data.PlantRepository instance;
    public static final com.google.samples.apps.sunflower.data.PlantRepository.Companion Companion = null;
    
    @org.jetbrains.annotations.NotNull()
    public final androidx.lifecycle.LiveData<java.util.List<com.google.samples.apps.sunflower.data.Plant>> getPlants() {
        return null;
    }
    
    @org.jetbrains.annotations.NotNull()
    public final androidx.lifecycle.LiveData<com.google.samples.apps.sunflower.data.Plant> getPlant(@org.jetbrains.annotations.NotNull()
    java.lang.String plantId) {
        return null;
    }
    
    @org.jetbrains.annotations.NotNull()
    public final androidx.lifecycle.LiveData<java.util.List<com.google.samples.apps.sunflower.data.Plant>> getPlantsWithGrowZoneNumber(int growZoneNumber) {
        return null;
    }
    
    private PlantRepository(com.google.samples.apps.sunflower.data.PlantDao plantDao) {
        super();
    }
    
    @kotlin.Metadata(mv = {1, 1, 13}, bv = {1, 0, 3}, k = 1, d1 = {"\u0000\u001a\n\u0002\u0018\u0002\n\u0002\u0010\u0000\n\u0002\b\u0002\n\u0002\u0018\u0002\n\u0002\b\u0002\n\u0002\u0018\u0002\n\u0000\b\u0086\u0003\u0018\u00002\u00020\u0001B\u0007\b\u0002\u00a2\u0006\u0002\u0010\u0002J\u000e\u0010\u0005\u001a\u00020\u00042\u0006\u0010\u0006\u001a\u00020\u0007R\u0010\u0010\u0003\u001a\u0004\u0018\u00010\u0004X\u0082\u000e\u00a2\u0006\u0002\n\u0000\u00a8\u0006\b"}, d2 = {"Lcom/google/samples/apps/sunflower/data/PlantRepository$Companion;", "", "()V", "instance", "Lcom/google/samples/apps/sunflower/data/PlantRepository;", "getInstance", "plantDao", "Lcom/google/samples/apps/sunflower/data/PlantDao;", "app_debug"})
    public static final class Companion {
        
        @org.jetbrains.annotations.NotNull()
        public final com.google.samples.apps.sunflower.data.PlantRepository getInstance(@org.jetbrains.annotations.NotNull()
        com.google.samples.apps.sunflower.data.PlantDao plantDao) {
            return null;
        }
        
        private Companion() {
            super();
        }
    }
}