package com.google.samples.apps.sunflower.utilities;

import java.lang.System;

@kotlin.Metadata(mv = {1, 1, 13}, bv = {1, 0, 3}, k = 2, d1 = {"\u0000\u000e\n\u0000\n\u0002\u0010\b\n\u0000\n\u0002\u0010\u0006\n\u0000\u001a\u000e\u0010\u0000\u001a\u00020\u00012\u0006\u0010\u0002\u001a\u00020\u0003\u00a8\u0006\u0004"}, d2 = {"getZoneForLatitude", "", "latitude", "", "app_debug"})
public final class GrowZoneUtilKt {
    
    /**
     * * A helper function to determine a Plant's growing zone for a given latitude.
     * *
     * * The numbers listed here are roughly based on the United States Department of Agriculture's
     * * Plant Hardiness Zone Map (http://planthardiness.ars.usda.gov/), which helps determine which
     * * plants are most likely to thrive at a location.
     * *
     * * If a given latitude falls on the border between two zone ranges, the larger zone range is chosen
     * * (e.g. latitude 14.0 => zone 12).
     * *
     * * Negative latitude values are converted to positive with [Math.abs].
     * *
     * * For latitude values greater than max (90.0), zone 1 is returned.
     */
    public static final int getZoneForLatitude(double latitude) {
        return 0;
    }
}