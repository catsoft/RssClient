package com.google.samples.apps.sunflower.data;

import java.lang.System;

/**
 * * [GardenPlanting] represents when a user adds a [Plant] to their garden, with useful metadata.
 * * Properties such as [lastWateringDate] are used for notifications (such as when to water the
 * * plant).
 * *
 * * Declaring the column info allows for the renaming of variables without implementing a
 * * database migration, as the column name would not change.
 */
@androidx.room.Entity(tableName = "garden_plantings", foreignKeys = {@androidx.room.ForeignKey(entity = com.google.samples.apps.sunflower.data.Plant.class, childColumns = {"plant_id"}, parentColumns = {"id"})}, indices = {@androidx.room.Index(value = {"plant_id"})})
@kotlin.Metadata(mv = {1, 1, 13}, bv = {1, 0, 3}, k = 1, d1 = {"\u00000\n\u0002\u0018\u0002\n\u0002\u0010\u0000\n\u0000\n\u0002\u0010\u000e\n\u0000\n\u0002\u0018\u0002\n\u0002\b\u0003\n\u0002\u0010\t\n\u0002\b\u000e\n\u0002\u0010\u000b\n\u0002\b\u0002\n\u0002\u0010\b\n\u0002\b\u0002\b\u0087\b\u0018\u00002\u00020\u0001B!\u0012\u0006\u0010\u0002\u001a\u00020\u0003\u0012\b\b\u0002\u0010\u0004\u001a\u00020\u0005\u0012\b\b\u0002\u0010\u0006\u001a\u00020\u0005\u00a2\u0006\u0002\u0010\u0007J\t\u0010\u0013\u001a\u00020\u0003H\u00c6\u0003J\t\u0010\u0014\u001a\u00020\u0005H\u00c6\u0003J\t\u0010\u0015\u001a\u00020\u0005H\u00c6\u0003J\'\u0010\u0016\u001a\u00020\u00002\b\b\u0002\u0010\u0002\u001a\u00020\u00032\b\b\u0002\u0010\u0004\u001a\u00020\u00052\b\b\u0002\u0010\u0006\u001a\u00020\u0005H\u00c6\u0001J\u0013\u0010\u0017\u001a\u00020\u00182\b\u0010\u0019\u001a\u0004\u0018\u00010\u0001H\u00d6\u0003J\t\u0010\u001a\u001a\u00020\u001bH\u00d6\u0001J\t\u0010\u001c\u001a\u00020\u0003H\u00d6\u0001R\u001e\u0010\b\u001a\u00020\t8\u0006@\u0006X\u0087\u000e\u00a2\u0006\u000e\n\u0000\u001a\u0004\b\n\u0010\u000b\"\u0004\b\f\u0010\rR\u0016\u0010\u0006\u001a\u00020\u00058\u0006X\u0087\u0004\u00a2\u0006\b\n\u0000\u001a\u0004\b\u000e\u0010\u000fR\u0016\u0010\u0004\u001a\u00020\u00058\u0006X\u0087\u0004\u00a2\u0006\b\n\u0000\u001a\u0004\b\u0010\u0010\u000fR\u0016\u0010\u0002\u001a\u00020\u00038\u0006X\u0087\u0004\u00a2\u0006\b\n\u0000\u001a\u0004\b\u0011\u0010\u0012\u00a8\u0006\u001d"}, d2 = {"Lcom/google/samples/apps/sunflower/data/GardenPlanting;", "", "plantId", "", "plantDate", "Ljava/util/Calendar;", "lastWateringDate", "(Ljava/lang/String;Ljava/util/Calendar;Ljava/util/Calendar;)V", "gardenPlantingId", "", "getGardenPlantingId", "()J", "setGardenPlantingId", "(J)V", "getLastWateringDate", "()Ljava/util/Calendar;", "getPlantDate", "getPlantId", "()Ljava/lang/String;", "component1", "component2", "component3", "copy", "equals", "", "other", "hashCode", "", "toString", "app_debug"})
public final class GardenPlanting {
    @androidx.room.ColumnInfo(name = "id")
    @androidx.room.PrimaryKey(autoGenerate = true)
    private long gardenPlantingId;
    @org.jetbrains.annotations.NotNull()
    @androidx.room.ColumnInfo(name = "plant_id")
    private final java.lang.String plantId = null;
    
    /**
     * * Indicates when the [Plant] was planted. Used for showing notification when it's time
     *     * to harvest the plant.
     */
    @org.jetbrains.annotations.NotNull()
    @androidx.room.ColumnInfo(name = "plant_date")
    private final java.util.Calendar plantDate = null;
    
    /**
     * * Indicates when the [Plant] was last watered. Used for showing notification when it's
     *     * time to water the plant.
     */
    @org.jetbrains.annotations.NotNull()
    @androidx.room.ColumnInfo(name = "last_watering_date")
    private final java.util.Calendar lastWateringDate = null;
    
    public final long getGardenPlantingId() {
        return 0L;
    }
    
    public final void setGardenPlantingId(long p0) {
    }
    
    @org.jetbrains.annotations.NotNull()
    public final java.lang.String getPlantId() {
        return null;
    }
    
    /**
     * * Indicates when the [Plant] was planted. Used for showing notification when it's time
     *     * to harvest the plant.
     */
    @org.jetbrains.annotations.NotNull()
    public final java.util.Calendar getPlantDate() {
        return null;
    }
    
    /**
     * * Indicates when the [Plant] was last watered. Used for showing notification when it's
     *     * time to water the plant.
     */
    @org.jetbrains.annotations.NotNull()
    public final java.util.Calendar getLastWateringDate() {
        return null;
    }
    
    public GardenPlanting(@org.jetbrains.annotations.NotNull()
    java.lang.String plantId, @org.jetbrains.annotations.NotNull()
    java.util.Calendar plantDate, @org.jetbrains.annotations.NotNull()
    java.util.Calendar lastWateringDate) {
        super();
    }
    
    @org.jetbrains.annotations.NotNull()
    public final java.lang.String component1() {
        return null;
    }
    
    /**
     * * Indicates when the [Plant] was planted. Used for showing notification when it's time
     *     * to harvest the plant.
     */
    @org.jetbrains.annotations.NotNull()
    public final java.util.Calendar component2() {
        return null;
    }
    
    /**
     * * Indicates when the [Plant] was last watered. Used for showing notification when it's
     *     * time to water the plant.
     */
    @org.jetbrains.annotations.NotNull()
    public final java.util.Calendar component3() {
        return null;
    }
    
    /**
     * * [GardenPlanting] represents when a user adds a [Plant] to their garden, with useful metadata.
     * * Properties such as [lastWateringDate] are used for notifications (such as when to water the
     * * plant).
     * *
     * * Declaring the column info allows for the renaming of variables without implementing a
     * * database migration, as the column name would not change.
     */
    @org.jetbrains.annotations.NotNull()
    public final com.google.samples.apps.sunflower.data.GardenPlanting copy(@org.jetbrains.annotations.NotNull()
    java.lang.String plantId, @org.jetbrains.annotations.NotNull()
    java.util.Calendar plantDate, @org.jetbrains.annotations.NotNull()
    java.util.Calendar lastWateringDate) {
        return null;
    }
    
    @org.jetbrains.annotations.NotNull()
    @java.lang.Override()
    public java.lang.String toString() {
        return null;
    }
    
    @java.lang.Override()
    public int hashCode() {
        return 0;
    }
    
    @java.lang.Override()
    public boolean equals(@org.jetbrains.annotations.Nullable()
    java.lang.Object p0) {
        return false;
    }
}