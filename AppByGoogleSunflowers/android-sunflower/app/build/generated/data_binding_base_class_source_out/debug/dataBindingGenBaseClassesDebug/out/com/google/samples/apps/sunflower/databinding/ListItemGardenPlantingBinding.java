package com.google.samples.apps.sunflower.databinding;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.databinding.Bindable;
import androidx.databinding.DataBindingComponent;
import androidx.databinding.DataBindingUtil;
import androidx.databinding.ViewDataBinding;
import com.google.samples.apps.sunflower.viewmodels.PlantAndGardenPlantingsViewModel;

public abstract class ListItemGardenPlantingBinding extends ViewDataBinding {
  @NonNull
  public final ImageView imageView;

  @NonNull
  public final TextView plantDate;

  @NonNull
  public final TextView waterDate;

  @Bindable
  protected PlantAndGardenPlantingsViewModel mViewModel;

  protected ListItemGardenPlantingBinding(DataBindingComponent _bindingComponent, View _root,
      int _localFieldCount, ImageView imageView, TextView plantDate, TextView waterDate) {
    super(_bindingComponent, _root, _localFieldCount);
    this.imageView = imageView;
    this.plantDate = plantDate;
    this.waterDate = waterDate;
  }

  public abstract void setViewModel(@Nullable PlantAndGardenPlantingsViewModel viewModel);

  @Nullable
  public PlantAndGardenPlantingsViewModel getViewModel() {
    return mViewModel;
  }

  @NonNull
  public static ListItemGardenPlantingBinding inflate(@NonNull LayoutInflater inflater,
      @Nullable ViewGroup root, boolean attachToRoot) {
    return inflate(inflater, root, attachToRoot, DataBindingUtil.getDefaultComponent());
  }

  @NonNull
  public static ListItemGardenPlantingBinding inflate(@NonNull LayoutInflater inflater,
      @Nullable ViewGroup root, boolean attachToRoot, @Nullable DataBindingComponent component) {
    return DataBindingUtil.<ListItemGardenPlantingBinding>inflate(inflater, com.google.samples.apps.sunflower.R.layout.list_item_garden_planting, root, attachToRoot, component);
  }

  @NonNull
  public static ListItemGardenPlantingBinding inflate(@NonNull LayoutInflater inflater) {
    return inflate(inflater, DataBindingUtil.getDefaultComponent());
  }

  @NonNull
  public static ListItemGardenPlantingBinding inflate(@NonNull LayoutInflater inflater,
      @Nullable DataBindingComponent component) {
    return DataBindingUtil.<ListItemGardenPlantingBinding>inflate(inflater, com.google.samples.apps.sunflower.R.layout.list_item_garden_planting, null, false, component);
  }

  public static ListItemGardenPlantingBinding bind(@NonNull View view) {
    return bind(view, DataBindingUtil.getDefaultComponent());
  }

  public static ListItemGardenPlantingBinding bind(@NonNull View view,
      @Nullable DataBindingComponent component) {
    return (ListItemGardenPlantingBinding)bind(component, view, com.google.samples.apps.sunflower.R.layout.list_item_garden_planting);
  }
}
