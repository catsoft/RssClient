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
import com.google.samples.apps.sunflower.data.Plant;

public abstract class ListItemPlantBinding extends ViewDataBinding {
  @NonNull
  public final ImageView plantItemImage;

  @NonNull
  public final TextView plantItemTitle;

  @Bindable
  protected View.OnClickListener mClickListener;

  @Bindable
  protected Plant mPlant;

  protected ListItemPlantBinding(DataBindingComponent _bindingComponent, View _root,
      int _localFieldCount, ImageView plantItemImage, TextView plantItemTitle) {
    super(_bindingComponent, _root, _localFieldCount);
    this.plantItemImage = plantItemImage;
    this.plantItemTitle = plantItemTitle;
  }

  public abstract void setClickListener(@Nullable View.OnClickListener clickListener);

  @Nullable
  public View.OnClickListener getClickListener() {
    return mClickListener;
  }

  public abstract void setPlant(@Nullable Plant plant);

  @Nullable
  public Plant getPlant() {
    return mPlant;
  }

  @NonNull
  public static ListItemPlantBinding inflate(@NonNull LayoutInflater inflater,
      @Nullable ViewGroup root, boolean attachToRoot) {
    return inflate(inflater, root, attachToRoot, DataBindingUtil.getDefaultComponent());
  }

  @NonNull
  public static ListItemPlantBinding inflate(@NonNull LayoutInflater inflater,
      @Nullable ViewGroup root, boolean attachToRoot, @Nullable DataBindingComponent component) {
    return DataBindingUtil.<ListItemPlantBinding>inflate(inflater, com.google.samples.apps.sunflower.R.layout.list_item_plant, root, attachToRoot, component);
  }

  @NonNull
  public static ListItemPlantBinding inflate(@NonNull LayoutInflater inflater) {
    return inflate(inflater, DataBindingUtil.getDefaultComponent());
  }

  @NonNull
  public static ListItemPlantBinding inflate(@NonNull LayoutInflater inflater,
      @Nullable DataBindingComponent component) {
    return DataBindingUtil.<ListItemPlantBinding>inflate(inflater, com.google.samples.apps.sunflower.R.layout.list_item_plant, null, false, component);
  }

  public static ListItemPlantBinding bind(@NonNull View view) {
    return bind(view, DataBindingUtil.getDefaultComponent());
  }

  public static ListItemPlantBinding bind(@NonNull View view,
      @Nullable DataBindingComponent component) {
    return (ListItemPlantBinding)bind(component, view, com.google.samples.apps.sunflower.R.layout.list_item_plant);
  }
}
