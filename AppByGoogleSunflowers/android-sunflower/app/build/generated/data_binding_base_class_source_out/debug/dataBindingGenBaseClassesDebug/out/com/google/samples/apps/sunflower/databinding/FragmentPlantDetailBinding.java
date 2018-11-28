package com.google.samples.apps.sunflower.databinding;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.widget.Toolbar;
import androidx.core.widget.NestedScrollView;
import androidx.databinding.Bindable;
import androidx.databinding.DataBindingComponent;
import androidx.databinding.DataBindingUtil;
import androidx.databinding.ViewDataBinding;
import com.google.android.material.appbar.AppBarLayout;
import com.google.android.material.appbar.CollapsingToolbarLayout;
import com.google.android.material.floatingactionbutton.FloatingActionButton;
import com.google.samples.apps.sunflower.viewmodels.PlantDetailViewModel;

public abstract class FragmentPlantDetailBinding extends ViewDataBinding {
  @NonNull
  public final AppBarLayout appbar;

  @NonNull
  public final ImageView detailImage;

  @NonNull
  public final Toolbar detailToolbar;

  @NonNull
  public final FloatingActionButton fab;

  @NonNull
  public final TextView plantDetail;

  @NonNull
  public final NestedScrollView plantDetailScrollview;

  @NonNull
  public final TextView plantWatering;

  @NonNull
  public final CollapsingToolbarLayout toolbarLayout;

  @Bindable
  protected PlantDetailViewModel mViewModel;

  protected FragmentPlantDetailBinding(DataBindingComponent _bindingComponent, View _root,
      int _localFieldCount, AppBarLayout appbar, ImageView detailImage, Toolbar detailToolbar,
      FloatingActionButton fab, TextView plantDetail, NestedScrollView plantDetailScrollview,
      TextView plantWatering, CollapsingToolbarLayout toolbarLayout) {
    super(_bindingComponent, _root, _localFieldCount);
    this.appbar = appbar;
    this.detailImage = detailImage;
    this.detailToolbar = detailToolbar;
    this.fab = fab;
    this.plantDetail = plantDetail;
    this.plantDetailScrollview = plantDetailScrollview;
    this.plantWatering = plantWatering;
    this.toolbarLayout = toolbarLayout;
  }

  public abstract void setViewModel(@Nullable PlantDetailViewModel viewModel);

  @Nullable
  public PlantDetailViewModel getViewModel() {
    return mViewModel;
  }

  @NonNull
  public static FragmentPlantDetailBinding inflate(@NonNull LayoutInflater inflater,
      @Nullable ViewGroup root, boolean attachToRoot) {
    return inflate(inflater, root, attachToRoot, DataBindingUtil.getDefaultComponent());
  }

  @NonNull
  public static FragmentPlantDetailBinding inflate(@NonNull LayoutInflater inflater,
      @Nullable ViewGroup root, boolean attachToRoot, @Nullable DataBindingComponent component) {
    return DataBindingUtil.<FragmentPlantDetailBinding>inflate(inflater, com.google.samples.apps.sunflower.R.layout.fragment_plant_detail, root, attachToRoot, component);
  }

  @NonNull
  public static FragmentPlantDetailBinding inflate(@NonNull LayoutInflater inflater) {
    return inflate(inflater, DataBindingUtil.getDefaultComponent());
  }

  @NonNull
  public static FragmentPlantDetailBinding inflate(@NonNull LayoutInflater inflater,
      @Nullable DataBindingComponent component) {
    return DataBindingUtil.<FragmentPlantDetailBinding>inflate(inflater, com.google.samples.apps.sunflower.R.layout.fragment_plant_detail, null, false, component);
  }

  public static FragmentPlantDetailBinding bind(@NonNull View view) {
    return bind(view, DataBindingUtil.getDefaultComponent());
  }

  public static FragmentPlantDetailBinding bind(@NonNull View view,
      @Nullable DataBindingComponent component) {
    return (FragmentPlantDetailBinding)bind(component, view, com.google.samples.apps.sunflower.R.layout.fragment_plant_detail);
  }
}
