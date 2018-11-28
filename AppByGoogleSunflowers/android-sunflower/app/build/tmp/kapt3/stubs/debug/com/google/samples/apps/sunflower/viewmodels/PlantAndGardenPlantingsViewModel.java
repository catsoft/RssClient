package com.google.samples.apps.sunflower.viewmodels;

import java.lang.System;

@kotlin.Metadata(mv = {1, 1, 13}, bv = {1, 0, 3}, k = 1, d1 = {"\u0000:\n\u0002\u0018\u0002\n\u0002\u0018\u0002\n\u0000\n\u0002\u0018\u0002\n\u0000\n\u0002\u0018\u0002\n\u0002\b\u0002\n\u0002\u0018\u0002\n\u0002\b\u0005\n\u0002\u0018\u0002\n\u0000\n\u0002\u0018\u0002\n\u0002\u0010\u000e\n\u0002\b\u0003\n\u0002\u0018\u0002\n\u0002\b\u0013\u0018\u00002\u00020\u0001B\u0015\u0012\u0006\u0010\u0002\u001a\u00020\u0003\u0012\u0006\u0010\u0004\u001a\u00020\u0005\u00a2\u0006\u0002\u0010\u0006R\u001b\u0010\u0007\u001a\u00020\b8BX\u0082\u0084\u0002\u00a2\u0006\f\n\u0004\b\u000b\u0010\f\u001a\u0004\b\t\u0010\nR\u000e\u0010\r\u001a\u00020\u000eX\u0082\u0004\u00a2\u0006\u0002\n\u0000R\u0017\u0010\u000f\u001a\b\u0012\u0004\u0012\u00020\u00110\u0010\u00a2\u0006\b\n\u0000\u001a\u0004\b\u0012\u0010\u0013R\u000e\u0010\u0014\u001a\u00020\u0015X\u0082\u0004\u00a2\u0006\u0002\n\u0000R\u0017\u0010\u0016\u001a\b\u0012\u0004\u0012\u00020\u00110\u0010\u00a2\u0006\b\n\u0000\u001a\u0004\b\u0017\u0010\u0013R#\u0010\u0018\u001a\n \u0019*\u0004\u0018\u00010\u00110\u00118BX\u0082\u0084\u0002\u00a2\u0006\f\n\u0004\b\u001c\u0010\f\u001a\u0004\b\u001a\u0010\u001bR\u0017\u0010\u001d\u001a\b\u0012\u0004\u0012\u00020\u00110\u0010\u00a2\u0006\b\n\u0000\u001a\u0004\b\u001e\u0010\u0013R#\u0010\u001f\u001a\n \u0019*\u0004\u0018\u00010\u00110\u00118BX\u0082\u0084\u0002\u00a2\u0006\f\n\u0004\b!\u0010\f\u001a\u0004\b \u0010\u001bR#\u0010\"\u001a\n \u0019*\u0004\u0018\u00010\u00110\u00118BX\u0082\u0084\u0002\u00a2\u0006\f\n\u0004\b$\u0010\f\u001a\u0004\b#\u0010\u001bR#\u0010%\u001a\n \u0019*\u0004\u0018\u00010\u00110\u00118BX\u0082\u0084\u0002\u00a2\u0006\f\n\u0004\b\'\u0010\f\u001a\u0004\b&\u0010\u001b\u00a8\u0006("}, d2 = {"Lcom/google/samples/apps/sunflower/viewmodels/PlantAndGardenPlantingsViewModel;", "Landroidx/lifecycle/ViewModel;", "context", "Landroid/content/Context;", "plantings", "Lcom/google/samples/apps/sunflower/data/PlantAndGardenPlantings;", "(Landroid/content/Context;Lcom/google/samples/apps/sunflower/data/PlantAndGardenPlantings;)V", "dateFormat", "Ljava/text/SimpleDateFormat;", "getDateFormat", "()Ljava/text/SimpleDateFormat;", "dateFormat$delegate", "Lkotlin/Lazy;", "gardenPlanting", "Lcom/google/samples/apps/sunflower/data/GardenPlanting;", "imageUrl", "Landroidx/databinding/ObservableField;", "", "getImageUrl", "()Landroidx/databinding/ObservableField;", "plant", "Lcom/google/samples/apps/sunflower/data/Plant;", "plantDate", "getPlantDate", "plantDateString", "kotlin.jvm.PlatformType", "getPlantDateString", "()Ljava/lang/String;", "plantDateString$delegate", "waterDate", "getWaterDate", "waterDateString", "getWaterDateString", "waterDateString$delegate", "wateringPrefix", "getWateringPrefix", "wateringPrefix$delegate", "wateringSuffix", "getWateringSuffix", "wateringSuffix$delegate", "app_debug"})
public final class PlantAndGardenPlantingsViewModel extends androidx.lifecycle.ViewModel {
    private final com.google.samples.apps.sunflower.data.Plant plant = null;
    private final com.google.samples.apps.sunflower.data.GardenPlanting gardenPlanting = null;
    private final kotlin.Lazy dateFormat$delegate = null;
    private final kotlin.Lazy plantDateString$delegate = null;
    private final kotlin.Lazy waterDateString$delegate = null;
    private final kotlin.Lazy wateringPrefix$delegate = null;
    private final kotlin.Lazy wateringSuffix$delegate = null;
    @org.jetbrains.annotations.NotNull()
    private final androidx.databinding.ObservableField<java.lang.String> imageUrl = null;
    @org.jetbrains.annotations.NotNull()
    private final androidx.databinding.ObservableField<java.lang.String> plantDate = null;
    @org.jetbrains.annotations.NotNull()
    private final androidx.databinding.ObservableField<java.lang.String> waterDate = null;
    
    private final java.text.SimpleDateFormat getDateFormat() {
        return null;
    }
    
    private final java.lang.String getPlantDateString() {
        return null;
    }
    
    private final java.lang.String getWaterDateString() {
        return null;
    }
    
    private final java.lang.String getWateringPrefix() {
        return null;
    }
    
    private final java.lang.String getWateringSuffix() {
        return null;
    }
    
    @org.jetbrains.annotations.NotNull()
    public final androidx.databinding.ObservableField<java.lang.String> getImageUrl() {
        return null;
    }
    
    @org.jetbrains.annotations.NotNull()
    public final androidx.databinding.ObservableField<java.lang.String> getPlantDate() {
        return null;
    }
    
    @org.jetbrains.annotations.NotNull()
    public final androidx.databinding.ObservableField<java.lang.String> getWaterDate() {
        return null;
    }
    
    public PlantAndGardenPlantingsViewModel(@org.jetbrains.annotations.NotNull()
    android.content.Context context, @org.jetbrains.annotations.NotNull()
    com.google.samples.apps.sunflower.data.PlantAndGardenPlantings plantings) {
        super();
    }
}