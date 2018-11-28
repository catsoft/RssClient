package com.google.samples.apps.sunflower.data;

import java.lang.System;

/**
 * * Type converters to allow Room to reference complex data types.
 */
@kotlin.Metadata(mv = {1, 1, 13}, bv = {1, 0, 3}, k = 1, d1 = {"\u0000\u001a\n\u0002\u0018\u0002\n\u0002\u0010\u0000\n\u0002\b\u0002\n\u0002\u0010\t\n\u0000\n\u0002\u0018\u0002\n\u0002\b\u0003\u0018\u00002\u00020\u0001B\u0005\u00a2\u0006\u0002\u0010\u0002J\u0010\u0010\u0003\u001a\u00020\u00042\u0006\u0010\u0005\u001a\u00020\u0006H\u0007J\u0010\u0010\u0007\u001a\u00020\u00062\u0006\u0010\b\u001a\u00020\u0004H\u0007\u00a8\u0006\t"}, d2 = {"Lcom/google/samples/apps/sunflower/data/Converters;", "", "()V", "calendarToDatestamp", "", "calendar", "Ljava/util/Calendar;", "datestampToCalendar", "value", "app_debug"})
public final class Converters {
    
    @androidx.room.TypeConverter()
    public final long calendarToDatestamp(@org.jetbrains.annotations.NotNull()
    java.util.Calendar calendar) {
        return 0L;
    }
    
    @org.jetbrains.annotations.NotNull()
    @androidx.room.TypeConverter()
    public final java.util.Calendar datestampToCalendar(long value) {
        return null;
    }
    
    public Converters() {
        super();
    }
}