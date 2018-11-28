package com.google.samples.apps.sunflower.data;

import java.lang.System;

/**
 * * This class captures the relationship between a [Plant] and a user's [GardenPlanting], which is
 * * used by Room to fetch the related entities.
 */
@kotlin.Metadata(mv = {1, 1, 13}, bv = {1, 0, 3}, k = 1, d1 = {"\u0000 \n\u0002\u0018\u0002\n\u0002\u0010\u0000\n\u0002\b\u0002\n\u0002\u0010 \n\u0002\u0018\u0002\n\u0002\b\u0005\n\u0002\u0018\u0002\n\u0002\b\u0005\u0018\u00002\u00020\u0001B\u0005\u00a2\u0006\u0002\u0010\u0002R$\u0010\u0003\u001a\b\u0012\u0004\u0012\u00020\u00050\u00048\u0006@\u0006X\u0087\u000e\u00a2\u0006\u000e\n\u0000\u001a\u0004\b\u0006\u0010\u0007\"\u0004\b\b\u0010\tR \u0010\n\u001a\u0004\u0018\u00010\u000b8\u0006@\u0006X\u0087\u000e\u00a2\u0006\u000e\n\u0000\u001a\u0004\b\f\u0010\r\"\u0004\b\u000e\u0010\u000f\u00a8\u0006\u0010"}, d2 = {"Lcom/google/samples/apps/sunflower/data/PlantAndGardenPlantings;", "", "()V", "gardenPlantings", "", "Lcom/google/samples/apps/sunflower/data/GardenPlanting;", "getGardenPlantings", "()Ljava/util/List;", "setGardenPlantings", "(Ljava/util/List;)V", "plant", "Lcom/google/samples/apps/sunflower/data/Plant;", "getPlant", "()Lcom/google/samples/apps/sunflower/data/Plant;", "setPlant", "(Lcom/google/samples/apps/sunflower/data/Plant;)V", "app_debug"})
public final class PlantAndGardenPlantings {
    @org.jetbrains.annotations.Nullable()
    @androidx.room.Embedded()
    private com.google.samples.apps.sunflower.data.Plant plant;
    @org.jetbrains.annotations.NotNull()
    @androidx.room.Relation(parentColumn = "id", entityColumn = "plant_id")
    private java.util.List<com.google.samples.apps.sunflower.data.GardenPlanting> gardenPlantings;
    
    @org.jetbrains.annotations.Nullable()
    public final com.google.samples.apps.sunflower.data.Plant getPlant() {
        return null;
    }
    
    public final void setPlant(@org.jetbrains.annotations.Nullable()
    com.google.samples.apps.sunflower.data.Plant p0) {
    }
    
    @org.jetbrains.annotations.NotNull()
    public final java.util.List<com.google.samples.apps.sunflower.data.GardenPlanting> getGardenPlantings() {
        return null;
    }
    
    public final void setGardenPlantings(@org.jetbrains.annotations.NotNull()
    java.util.List<com.google.samples.apps.sunflower.data.GardenPlanting> p0) {
    }
    
    public PlantAndGardenPlantings() {
        super();
    }
}