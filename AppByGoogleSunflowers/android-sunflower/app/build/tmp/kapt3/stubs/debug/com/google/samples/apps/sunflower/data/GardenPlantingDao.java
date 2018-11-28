package com.google.samples.apps.sunflower.data;

import java.lang.System;

/**
 * * The Data Access Object for the [GardenPlanting] class.
 */
@androidx.room.Dao()
@kotlin.Metadata(mv = {1, 1, 13}, bv = {1, 0, 3}, k = 1, d1 = {"\u00008\n\u0002\u0018\u0002\n\u0002\u0010\u0000\n\u0000\n\u0002\u0010\u0002\n\u0000\n\u0002\u0018\u0002\n\u0000\n\u0002\u0018\u0002\n\u0000\n\u0002\u0010\t\n\u0002\b\u0002\n\u0002\u0010\u000e\n\u0000\n\u0002\u0010 \n\u0000\n\u0002\u0018\u0002\n\u0002\b\u0002\bg\u0018\u00002\u00020\u0001J\u0010\u0010\u0002\u001a\u00020\u00032\u0006\u0010\u0004\u001a\u00020\u0005H\'J\u0016\u0010\u0006\u001a\b\u0012\u0004\u0012\u00020\u00050\u00072\u0006\u0010\b\u001a\u00020\tH\'J\u0016\u0010\n\u001a\b\u0012\u0004\u0012\u00020\u00050\u00072\u0006\u0010\u000b\u001a\u00020\fH\'J\u0014\u0010\r\u001a\u000e\u0012\n\u0012\b\u0012\u0004\u0012\u00020\u00050\u000e0\u0007H\'J\u0014\u0010\u000f\u001a\u000e\u0012\n\u0012\b\u0012\u0004\u0012\u00020\u00100\u000e0\u0007H\'J\u0010\u0010\u0011\u001a\u00020\t2\u0006\u0010\u0004\u001a\u00020\u0005H\'\u00a8\u0006\u0012"}, d2 = {"Lcom/google/samples/apps/sunflower/data/GardenPlantingDao;", "", "deleteGardenPlanting", "", "gardenPlanting", "Lcom/google/samples/apps/sunflower/data/GardenPlanting;", "getGardenPlanting", "Landroidx/lifecycle/LiveData;", "gardenPlantingId", "", "getGardenPlantingForPlant", "plantId", "", "getGardenPlantings", "", "getPlantAndGardenPlantings", "Lcom/google/samples/apps/sunflower/data/PlantAndGardenPlantings;", "insertGardenPlanting", "app_debug"})
public abstract interface GardenPlantingDao {
    
    @org.jetbrains.annotations.NotNull()
    @androidx.room.Query(value = "SELECT * FROM garden_plantings")
    public abstract androidx.lifecycle.LiveData<java.util.List<com.google.samples.apps.sunflower.data.GardenPlanting>> getGardenPlantings();
    
    @org.jetbrains.annotations.NotNull()
    @androidx.room.Query(value = "SELECT * FROM garden_plantings WHERE id = :gardenPlantingId")
    public abstract androidx.lifecycle.LiveData<com.google.samples.apps.sunflower.data.GardenPlanting> getGardenPlanting(long gardenPlantingId);
    
    @org.jetbrains.annotations.NotNull()
    @androidx.room.Query(value = "SELECT * FROM garden_plantings WHERE plant_id = :plantId")
    public abstract androidx.lifecycle.LiveData<com.google.samples.apps.sunflower.data.GardenPlanting> getGardenPlantingForPlant(@org.jetbrains.annotations.NotNull()
    java.lang.String plantId);
    
    /**
     * * This query will tell Room to query both the [Plant] and [GardenPlanting] tables and handle
     *     * the object mapping.
     */
    @org.jetbrains.annotations.NotNull()
    @androidx.room.Query(value = "SELECT * FROM plants")
    @androidx.room.Transaction()
    public abstract androidx.lifecycle.LiveData<java.util.List<com.google.samples.apps.sunflower.data.PlantAndGardenPlantings>> getPlantAndGardenPlantings();
    
    @androidx.room.Insert()
    public abstract long insertGardenPlanting(@org.jetbrains.annotations.NotNull()
    com.google.samples.apps.sunflower.data.GardenPlanting gardenPlanting);
    
    @androidx.room.Delete()
    public abstract void deleteGardenPlanting(@org.jetbrains.annotations.NotNull()
    com.google.samples.apps.sunflower.data.GardenPlanting gardenPlanting);
}