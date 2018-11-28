package com.google.samples.apps.sunflower.databinding;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.databinding.Bindable;
import androidx.databinding.DataBindingComponent;
import androidx.databinding.DataBindingUtil;
import androidx.databinding.ViewDataBinding;
import androidx.recyclerview.widget.RecyclerView;

public abstract class FragmentGardenBinding extends ViewDataBinding {
  @NonNull
  public final TextView emptyGarden;

  @NonNull
  public final RecyclerView gardenList;

  @Bindable
  protected Boolean mHasPlantings;

  protected FragmentGardenBinding(DataBindingComponent _bindingComponent, View _root,
      int _localFieldCount, TextView emptyGarden, RecyclerView gardenList) {
    super(_bindingComponent, _root, _localFieldCount);
    this.emptyGarden = emptyGarden;
    this.gardenList = gardenList;
  }

  public abstract void setHasPlantings(@Nullable Boolean hasPlantings);

  @Nullable
  public Boolean getHasPlantings() {
    return mHasPlantings;
  }

  @NonNull
  public static FragmentGardenBinding inflate(@NonNull LayoutInflater inflater,
      @Nullable ViewGroup root, boolean attachToRoot) {
    return inflate(inflater, root, attachToRoot, DataBindingUtil.getDefaultComponent());
  }

  @NonNull
  public static FragmentGardenBinding inflate(@NonNull LayoutInflater inflater,
      @Nullable ViewGroup root, boolean attachToRoot, @Nullable DataBindingComponent component) {
    return DataBindingUtil.<FragmentGardenBinding>inflate(inflater, com.google.samples.apps.sunflower.R.layout.fragment_garden, root, attachToRoot, component);
  }

  @NonNull
  public static FragmentGardenBinding inflate(@NonNull LayoutInflater inflater) {
    return inflate(inflater, DataBindingUtil.getDefaultComponent());
  }

  @NonNull
  public static FragmentGardenBinding inflate(@NonNull LayoutInflater inflater,
      @Nullable DataBindingComponent component) {
    return DataBindingUtil.<FragmentGardenBinding>inflate(inflater, com.google.samples.apps.sunflower.R.layout.fragment_garden, null, false, component);
  }

  public static FragmentGardenBinding bind(@NonNull View view) {
    return bind(view, DataBindingUtil.getDefaultComponent());
  }

  public static FragmentGardenBinding bind(@NonNull View view,
      @Nullable DataBindingComponent component) {
    return (FragmentGardenBinding)bind(component, view, com.google.samples.apps.sunflower.R.layout.fragment_garden);
  }
}
