package com.google.samples.apps.sunflower;

import android.os.Bundle;
import androidx.annotation.NonNull;
import java.lang.IllegalArgumentException;
import java.lang.Object;
import java.lang.Override;
import java.lang.String;

public class PlantDetailFragmentArgs {
  @NonNull
  private String plantId;

  private PlantDetailFragmentArgs() {
  }

  @NonNull
  public static PlantDetailFragmentArgs fromBundle(Bundle bundle) {
    PlantDetailFragmentArgs result = new PlantDetailFragmentArgs();
    bundle.setClassLoader(PlantDetailFragmentArgs.class.getClassLoader());
    if (bundle.containsKey("plantId")) {
      result.plantId = bundle.getString("plantId");
      if (result.plantId == null) {
        throw new IllegalArgumentException("Argument \"plantId\" is marked as non-null but was passed a null value.");
      }
    } else {
      throw new IllegalArgumentException("Required argument \"plantId\" is missing and does not have an android:defaultValue");
    }
    return result;
  }

  @NonNull
  public String getPlantId() {
    return plantId;
  }

  @NonNull
  public Bundle toBundle() {
    Bundle __outBundle = new Bundle();
    __outBundle.putString("plantId", this.plantId);
    return __outBundle;
  }

  @Override
  public boolean equals(Object object) {
    if (this == object) {
        return true;
    }
    if (object == null || getClass() != object.getClass()) {
        return false;
    }
    PlantDetailFragmentArgs that = (PlantDetailFragmentArgs) object;
    if (plantId != null ? !plantId.equals(that.plantId) : that.plantId != null) {
      return false;
    }
    return true;
  }

  @Override
  public int hashCode() {
    int result = super.hashCode();
    result = 31 * result + (plantId != null ? plantId.hashCode() : 0);
    return result;
  }

  @Override
  public String toString() {
    return "PlantDetailFragmentArgs{"
        + "plantId=" + plantId
        + "}";
  }

  public static class Builder {
    @NonNull
    private String plantId;

    public Builder(PlantDetailFragmentArgs original) {
      this.plantId = original.plantId;
    }

    public Builder(@NonNull String plantId) {
      this.plantId = plantId;
      if (this.plantId == null) {
        throw new IllegalArgumentException("Argument \"plantId\" is marked as non-null but was passed a null value.");
      }
    }

    @NonNull
    public PlantDetailFragmentArgs build() {
      PlantDetailFragmentArgs result = new PlantDetailFragmentArgs();
      result.plantId = this.plantId;
      return result;
    }

    @NonNull
    public Builder setPlantId(@NonNull String plantId) {
      if (plantId == null) {
        throw new IllegalArgumentException("Argument \"plantId\" is marked as non-null but was passed a null value.");
      }
      this.plantId = plantId;
      return this;
    }

    @NonNull
    public String getPlantId() {
      return plantId;
    }
  }
}
