package androidx.databinding;

public class DataBinderMapperImpl extends MergedDataBinderMapper {
  DataBinderMapperImpl() {
    addMapper(new com.google.samples.apps.sunflower.DataBinderMapperImpl());
  }
}
