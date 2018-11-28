package com.google.samples.apps.sunflower.databinding;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.databinding.DataBindingComponent;
import androidx.databinding.DataBindingUtil;
import androidx.databinding.ViewDataBinding;
import androidx.recyclerview.widget.RecyclerView;

public abstract class FragmentPlantListBinding extends ViewDataBinding {
  @NonNull
  public final RecyclerView plantList;

  protected FragmentPlantListBinding(DataBindingComponent _bindingComponent, View _root,
      int _localFieldCount, RecyclerView plantList) {
    super(_bindingComponent, _root, _localFieldCount);
    this.plantList = plantList;
  }

  @NonNull
  public static FragmentPlantListBinding inflate(@NonNull LayoutInflater inflater,
      @Nullable ViewGroup root, boolean attachToRoot) {
    return inflate(inflater, root, attachToRoot, DataBindingUtil.getDefaultComponent());
  }

  @NonNull
  public static FragmentPlantListBinding inflate(@NonNull LayoutInflater inflater,
      @Nullable ViewGroup root, boolean attachToRoot, @Nullable DataBindingComponent component) {
    return DataBindingUtil.<FragmentPlantListBinding>inflate(inflater, com.google.samples.apps.sunflower.R.layout.fragment_plant_list, root, attachToRoot, component);
  }

  @NonNull
  public static FragmentPlantListBinding inflate(@NonNull LayoutInflater inflater) {
    return inflate(inflater, DataBindingUtil.getDefaultComponent());
  }

  @NonNull
  public static FragmentPlantListBinding inflate(@NonNull LayoutInflater inflater,
      @Nullable DataBindingComponent component) {
    return DataBindingUtil.<FragmentPlantListBinding>inflate(inflater, com.google.samples.apps.sunflower.R.layout.fragment_plant_list, null, false, component);
  }

  public static FragmentPlantListBinding bind(@NonNull View view) {
    return bind(view, DataBindingUtil.getDefaultComponent());
  }

  public static FragmentPlantListBinding bind(@NonNull View view,
      @Nullable DataBindingComponent component) {
    return (FragmentPlantListBinding)bind(component, view, com.google.samples.apps.sunflower.R.layout.fragment_plant_list);
  }
}
