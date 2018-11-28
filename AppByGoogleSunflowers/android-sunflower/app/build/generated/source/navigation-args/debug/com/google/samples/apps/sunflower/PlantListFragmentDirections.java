package com.google.samples.apps.sunflower;

import android.os.Bundle;
import androidx.annotation.NonNull;
import androidx.navigation.NavDirections;
import java.lang.IllegalArgumentException;
import java.lang.Object;
import java.lang.Override;
import java.lang.String;

public class PlantListFragmentDirections {
  @NonNull
  public static ActionPlantListFragmentToPlantDetailFragment actionPlantListFragmentToPlantDetailFragment(@NonNull String plantId) {
    return new ActionPlantListFragmentToPlantDetailFragment(plantId);
  }

  public static class ActionPlantListFragmentToPlantDetailFragment implements NavDirections {
    @NonNull
    private String plantId;

    public ActionPlantListFragmentToPlantDetailFragment(@NonNull String plantId) {
      this.plantId = plantId;
      if (this.plantId == null) {
        throw new IllegalArgumentException("Argument \"plantId\" is marked as non-null but was passed a null value.");
      }
    }

    @NonNull
    public ActionPlantListFragmentToPlantDetailFragment setPlantId(@NonNull String plantId) {
      if (plantId == null) {
        throw new IllegalArgumentException("Argument \"plantId\" is marked as non-null but was passed a null value.");
      }
      this.plantId = plantId;
      return this;
    }

    @NonNull
    public Bundle getArguments() {
      Bundle __outBundle = new Bundle();
      __outBundle.putString("plantId", this.plantId);
      return __outBundle;
    }

    public int getActionId() {
      return com.google.samples.apps.sunflower.R.id.action_plant_list_fragment_to_plant_detail_fragment;
    }

    @Override
    public boolean equals(Object object) {
      if (this == object) {
          return true;
      }
      if (object == null || getClass() != object.getClass()) {
          return false;
      }
      ActionPlantListFragmentToPlantDetailFragment that = (ActionPlantListFragmentToPlantDetailFragment) object;
      if (plantId != null ? !plantId.equals(that.plantId) : that.plantId != null) {
        return false;
      }
      if (getActionId() != that.getActionId()) {
        return false;
      }
      return true;
    }

    @Override
    public int hashCode() {
      int result = super.hashCode();
      result = 31 * result + (plantId != null ? plantId.hashCode() : 0);
      result = 31 * result + getActionId();
      return result;
    }

    @Override
    public String toString() {
      return "ActionPlantListFragmentToPlantDetailFragment(actionId=" + getActionId() + "){"
          + "plantId=" + plantId
          + "}";
    }
  }
}
