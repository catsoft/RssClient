package com.google.samples.apps.sunflower;

import android.util.SparseArray;
import android.util.SparseIntArray;
import android.view.View;
import androidx.databinding.DataBinderMapper;
import androidx.databinding.DataBindingComponent;
import androidx.databinding.ViewDataBinding;
import com.google.samples.apps.sunflower.databinding.ActivityGardenBindingImpl;
import com.google.samples.apps.sunflower.databinding.FragmentGardenBindingImpl;
import com.google.samples.apps.sunflower.databinding.FragmentPlantDetailBindingImpl;
import com.google.samples.apps.sunflower.databinding.FragmentPlantListBindingImpl;
import com.google.samples.apps.sunflower.databinding.ListItemGardenPlantingBindingImpl;
import com.google.samples.apps.sunflower.databinding.ListItemPlantBindingImpl;
import java.lang.IllegalArgumentException;
import java.lang.Integer;
import java.lang.Object;
import java.lang.Override;
import java.lang.RuntimeException;
import java.lang.String;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class DataBinderMapperImpl extends DataBinderMapper {
  private static final int LAYOUT_ACTIVITYGARDEN = 1;

  private static final int LAYOUT_FRAGMENTGARDEN = 2;

  private static final int LAYOUT_FRAGMENTPLANTDETAIL = 3;

  private static final int LAYOUT_FRAGMENTPLANTLIST = 4;

  private static final int LAYOUT_LISTITEMGARDENPLANTING = 5;

  private static final int LAYOUT_LISTITEMPLANT = 6;

  private static final SparseIntArray INTERNAL_LAYOUT_ID_LOOKUP = new SparseIntArray(6);

  static {
    INTERNAL_LAYOUT_ID_LOOKUP.put(com.google.samples.apps.sunflower.R.layout.activity_garden, LAYOUT_ACTIVITYGARDEN);
    INTERNAL_LAYOUT_ID_LOOKUP.put(com.google.samples.apps.sunflower.R.layout.fragment_garden, LAYOUT_FRAGMENTGARDEN);
    INTERNAL_LAYOUT_ID_LOOKUP.put(com.google.samples.apps.sunflower.R.layout.fragment_plant_detail, LAYOUT_FRAGMENTPLANTDETAIL);
    INTERNAL_LAYOUT_ID_LOOKUP.put(com.google.samples.apps.sunflower.R.layout.fragment_plant_list, LAYOUT_FRAGMENTPLANTLIST);
    INTERNAL_LAYOUT_ID_LOOKUP.put(com.google.samples.apps.sunflower.R.layout.list_item_garden_planting, LAYOUT_LISTITEMGARDENPLANTING);
    INTERNAL_LAYOUT_ID_LOOKUP.put(com.google.samples.apps.sunflower.R.layout.list_item_plant, LAYOUT_LISTITEMPLANT);
  }

  @Override
  public ViewDataBinding getDataBinder(DataBindingComponent component, View view, int layoutId) {
    int localizedLayoutId = INTERNAL_LAYOUT_ID_LOOKUP.get(layoutId);
    if(localizedLayoutId > 0) {
      final Object tag = view.getTag();
      if(tag == null) {
        throw new RuntimeException("view must have a tag");
      }
      switch(localizedLayoutId) {
        case  LAYOUT_ACTIVITYGARDEN: {
          if ("layout/activity_garden_0".equals(tag)) {
            return new ActivityGardenBindingImpl(component, view);
          }
          throw new IllegalArgumentException("The tag for activity_garden is invalid. Received: " + tag);
        }
        case  LAYOUT_FRAGMENTGARDEN: {
          if ("layout/fragment_garden_0".equals(tag)) {
            return new FragmentGardenBindingImpl(component, view);
          }
          throw new IllegalArgumentException("The tag for fragment_garden is invalid. Received: " + tag);
        }
        case  LAYOUT_FRAGMENTPLANTDETAIL: {
          if ("layout/fragment_plant_detail_0".equals(tag)) {
            return new FragmentPlantDetailBindingImpl(component, view);
          }
          throw new IllegalArgumentException("The tag for fragment_plant_detail is invalid. Received: " + tag);
        }
        case  LAYOUT_FRAGMENTPLANTLIST: {
          if ("layout/fragment_plant_list_0".equals(tag)) {
            return new FragmentPlantListBindingImpl(component, view);
          }
          throw new IllegalArgumentException("The tag for fragment_plant_list is invalid. Received: " + tag);
        }
        case  LAYOUT_LISTITEMGARDENPLANTING: {
          if ("layout/list_item_garden_planting_0".equals(tag)) {
            return new ListItemGardenPlantingBindingImpl(component, view);
          }
          throw new IllegalArgumentException("The tag for list_item_garden_planting is invalid. Received: " + tag);
        }
        case  LAYOUT_LISTITEMPLANT: {
          if ("layout/list_item_plant_0".equals(tag)) {
            return new ListItemPlantBindingImpl(component, view);
          }
          throw new IllegalArgumentException("The tag for list_item_plant is invalid. Received: " + tag);
        }
      }
    }
    return null;
  }

  @Override
  public ViewDataBinding getDataBinder(DataBindingComponent component, View[] views, int layoutId) {
    if(views == null || views.length == 0) {
      return null;
    }
    int localizedLayoutId = INTERNAL_LAYOUT_ID_LOOKUP.get(layoutId);
    if(localizedLayoutId > 0) {
      final Object tag = views[0].getTag();
      if(tag == null) {
        throw new RuntimeException("view must have a tag");
      }
      switch(localizedLayoutId) {
      }
    }
    return null;
  }

  @Override
  public int getLayoutId(String tag) {
    if (tag == null) {
      return 0;
    }
    Integer tmpVal = InnerLayoutIdLookup.sKeys.get(tag);
    return tmpVal == null ? 0 : tmpVal;
  }

  @Override
  public String convertBrIdToString(int localId) {
    String tmpVal = InnerBrLookup.sKeys.get(localId);
    return tmpVal;
  }

  @Override
  public List<DataBinderMapper> collectDependencies() {
    ArrayList<DataBinderMapper> result = new ArrayList<DataBinderMapper>(1);
    result.add(new androidx.databinding.library.baseAdapters.DataBinderMapperImpl());
    return result;
  }

  private static class InnerBrLookup {
    static final SparseArray<String> sKeys = new SparseArray<String>(6);

    static {
      sKeys.put(0, "_all");
      sKeys.put(1, "hasPlantings");
      sKeys.put(2, "plant");
      sKeys.put(3, "viewModel");
      sKeys.put(4, "clickListener");
    }
  }

  private static class InnerLayoutIdLookup {
    static final HashMap<String, Integer> sKeys = new HashMap<String, Integer>(6);

    static {
      sKeys.put("layout/activity_garden_0", com.google.samples.apps.sunflower.R.layout.activity_garden);
      sKeys.put("layout/fragment_garden_0", com.google.samples.apps.sunflower.R.layout.fragment_garden);
      sKeys.put("layout/fragment_plant_detail_0", com.google.samples.apps.sunflower.R.layout.fragment_plant_detail);
      sKeys.put("layout/fragment_plant_list_0", com.google.samples.apps.sunflower.R.layout.fragment_plant_list);
      sKeys.put("layout/list_item_garden_planting_0", com.google.samples.apps.sunflower.R.layout.list_item_garden_planting);
      sKeys.put("layout/list_item_plant_0", com.google.samples.apps.sunflower.R.layout.list_item_plant);
    }
  }
}
