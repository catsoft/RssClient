package com.google.samples.apps.sunflower.databinding;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.widget.Toolbar;
import androidx.databinding.DataBindingComponent;
import androidx.databinding.DataBindingUtil;
import androidx.databinding.ViewDataBinding;
import androidx.drawerlayout.widget.DrawerLayout;
import com.google.android.material.appbar.AppBarLayout;
import com.google.android.material.navigation.NavigationView;

public abstract class ActivityGardenBinding extends ViewDataBinding {
  @NonNull
  public final AppBarLayout appbar;

  @NonNull
  public final DrawerLayout drawerLayout;

  @NonNull
  public final NavigationView navigationView;

  @NonNull
  public final Toolbar toolbar;

  protected ActivityGardenBinding(DataBindingComponent _bindingComponent, View _root,
      int _localFieldCount, AppBarLayout appbar, DrawerLayout drawerLayout,
      NavigationView navigationView, Toolbar toolbar) {
    super(_bindingComponent, _root, _localFieldCount);
    this.appbar = appbar;
    this.drawerLayout = drawerLayout;
    this.navigationView = navigationView;
    this.toolbar = toolbar;
  }

  @NonNull
  public static ActivityGardenBinding inflate(@NonNull LayoutInflater inflater,
      @Nullable ViewGroup root, boolean attachToRoot) {
    return inflate(inflater, root, attachToRoot, DataBindingUtil.getDefaultComponent());
  }

  @NonNull
  public static ActivityGardenBinding inflate(@NonNull LayoutInflater inflater,
      @Nullable ViewGroup root, boolean attachToRoot, @Nullable DataBindingComponent component) {
    return DataBindingUtil.<ActivityGardenBinding>inflate(inflater, com.google.samples.apps.sunflower.R.layout.activity_garden, root, attachToRoot, component);
  }

  @NonNull
  public static ActivityGardenBinding inflate(@NonNull LayoutInflater inflater) {
    return inflate(inflater, DataBindingUtil.getDefaultComponent());
  }

  @NonNull
  public static ActivityGardenBinding inflate(@NonNull LayoutInflater inflater,
      @Nullable DataBindingComponent component) {
    return DataBindingUtil.<ActivityGardenBinding>inflate(inflater, com.google.samples.apps.sunflower.R.layout.activity_garden, null, false, component);
  }

  public static ActivityGardenBinding bind(@NonNull View view) {
    return bind(view, DataBindingUtil.getDefaultComponent());
  }

  public static ActivityGardenBinding bind(@NonNull View view,
      @Nullable DataBindingComponent component) {
    return (ActivityGardenBinding)bind(component, view, com.google.samples.apps.sunflower.R.layout.activity_garden);
  }
}
