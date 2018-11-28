package com.google.samples.apps.sunflower.data;

import android.database.Cursor;
import androidx.annotation.NonNull;
import androidx.lifecycle.ComputableLiveData;
import androidx.lifecycle.LiveData;
import androidx.room.EntityInsertionAdapter;
import androidx.room.InvalidationTracker.Observer;
import androidx.room.RoomDatabase;
import androidx.room.RoomSQLiteQuery;
import androidx.room.util.DBUtil;
import androidx.sqlite.db.SupportSQLiteStatement;
import java.lang.Override;
import java.lang.String;
import java.lang.SuppressWarnings;
import java.util.ArrayList;
import java.util.List;
import java.util.Set;

@SuppressWarnings("unchecked")
public final class PlantDao_Impl implements PlantDao {
  private final RoomDatabase __db;

  private final EntityInsertionAdapter __insertionAdapterOfPlant;

  public PlantDao_Impl(RoomDatabase __db) {
    this.__db = __db;
    this.__insertionAdapterOfPlant = new EntityInsertionAdapter<Plant>(__db) {
      @Override
      public String createQuery() {
        return "INSERT OR REPLACE INTO `plants`(`id`,`name`,`description`,`growZoneNumber`,`wateringInterval`,`imageUrl`) VALUES (?,?,?,?,?,?)";
      }

      @Override
      public void bind(SupportSQLiteStatement stmt, Plant value) {
        if (value.getPlantId() == null) {
          stmt.bindNull(1);
        } else {
          stmt.bindString(1, value.getPlantId());
        }
        if (value.getName() == null) {
          stmt.bindNull(2);
        } else {
          stmt.bindString(2, value.getName());
        }
        if (value.getDescription() == null) {
          stmt.bindNull(3);
        } else {
          stmt.bindString(3, value.getDescription());
        }
        stmt.bindLong(4, value.getGrowZoneNumber());
        stmt.bindLong(5, value.getWateringInterval());
        if (value.getImageUrl() == null) {
          stmt.bindNull(6);
        } else {
          stmt.bindString(6, value.getImageUrl());
        }
      }
    };
  }

  @Override
  public void insertAll(List<Plant> plants) {
    __db.beginTransaction();
    try {
      __insertionAdapterOfPlant.insert(plants);
      __db.setTransactionSuccessful();
    } finally {
      __db.endTransaction();
    }
  }

  @Override
  public LiveData<List<Plant>> getPlants() {
    final String _sql = "SELECT * FROM plants ORDER BY name";
    final RoomSQLiteQuery _statement = RoomSQLiteQuery.acquire(_sql, 0);
    return new ComputableLiveData<List<Plant>>(__db.getQueryExecutor()) {
      private Observer _observer;

      @Override
      protected List<Plant> compute() {
        if (_observer == null) {
          _observer = new Observer("plants") {
            @Override
            public void onInvalidated(@NonNull Set<String> tables) {
              invalidate();
            }
          };
          __db.getInvalidationTracker().addWeakObserver(_observer);
        }
        final Cursor _cursor = DBUtil.query(__db, _statement, false);
        try {
          final int _cursorIndexOfPlantId = _cursor.getColumnIndexOrThrow("id");
          final int _cursorIndexOfName = _cursor.getColumnIndexOrThrow("name");
          final int _cursorIndexOfDescription = _cursor.getColumnIndexOrThrow("description");
          final int _cursorIndexOfGrowZoneNumber = _cursor.getColumnIndexOrThrow("growZoneNumber");
          final int _cursorIndexOfWateringInterval = _cursor.getColumnIndexOrThrow("wateringInterval");
          final int _cursorIndexOfImageUrl = _cursor.getColumnIndexOrThrow("imageUrl");
          final List<Plant> _result = new ArrayList<Plant>(_cursor.getCount());
          while(_cursor.moveToNext()) {
            final Plant _item;
            final String _tmpPlantId;
            _tmpPlantId = _cursor.getString(_cursorIndexOfPlantId);
            final String _tmpName;
            _tmpName = _cursor.getString(_cursorIndexOfName);
            final String _tmpDescription;
            _tmpDescription = _cursor.getString(_cursorIndexOfDescription);
            final int _tmpGrowZoneNumber;
            _tmpGrowZoneNumber = _cursor.getInt(_cursorIndexOfGrowZoneNumber);
            final int _tmpWateringInterval;
            _tmpWateringInterval = _cursor.getInt(_cursorIndexOfWateringInterval);
            final String _tmpImageUrl;
            _tmpImageUrl = _cursor.getString(_cursorIndexOfImageUrl);
            _item = new Plant(_tmpPlantId,_tmpName,_tmpDescription,_tmpGrowZoneNumber,_tmpWateringInterval,_tmpImageUrl);
            _result.add(_item);
          }
          return _result;
        } finally {
          _cursor.close();
        }
      }

      @Override
      protected void finalize() {
        _statement.release();
      }
    }.getLiveData();
  }

  @Override
  public LiveData<List<Plant>> getPlantsWithGrowZoneNumber(int growZoneNumber) {
    final String _sql = "SELECT * FROM plants WHERE growZoneNumber = ? ORDER BY name";
    final RoomSQLiteQuery _statement = RoomSQLiteQuery.acquire(_sql, 1);
    int _argIndex = 1;
    _statement.bindLong(_argIndex, growZoneNumber);
    return new ComputableLiveData<List<Plant>>(__db.getQueryExecutor()) {
      private Observer _observer;

      @Override
      protected List<Plant> compute() {
        if (_observer == null) {
          _observer = new Observer("plants") {
            @Override
            public void onInvalidated(@NonNull Set<String> tables) {
              invalidate();
            }
          };
          __db.getInvalidationTracker().addWeakObserver(_observer);
        }
        final Cursor _cursor = DBUtil.query(__db, _statement, false);
        try {
          final int _cursorIndexOfPlantId = _cursor.getColumnIndexOrThrow("id");
          final int _cursorIndexOfName = _cursor.getColumnIndexOrThrow("name");
          final int _cursorIndexOfDescription = _cursor.getColumnIndexOrThrow("description");
          final int _cursorIndexOfGrowZoneNumber = _cursor.getColumnIndexOrThrow("growZoneNumber");
          final int _cursorIndexOfWateringInterval = _cursor.getColumnIndexOrThrow("wateringInterval");
          final int _cursorIndexOfImageUrl = _cursor.getColumnIndexOrThrow("imageUrl");
          final List<Plant> _result = new ArrayList<Plant>(_cursor.getCount());
          while(_cursor.moveToNext()) {
            final Plant _item;
            final String _tmpPlantId;
            _tmpPlantId = _cursor.getString(_cursorIndexOfPlantId);
            final String _tmpName;
            _tmpName = _cursor.getString(_cursorIndexOfName);
            final String _tmpDescription;
            _tmpDescription = _cursor.getString(_cursorIndexOfDescription);
            final int _tmpGrowZoneNumber;
            _tmpGrowZoneNumber = _cursor.getInt(_cursorIndexOfGrowZoneNumber);
            final int _tmpWateringInterval;
            _tmpWateringInterval = _cursor.getInt(_cursorIndexOfWateringInterval);
            final String _tmpImageUrl;
            _tmpImageUrl = _cursor.getString(_cursorIndexOfImageUrl);
            _item = new Plant(_tmpPlantId,_tmpName,_tmpDescription,_tmpGrowZoneNumber,_tmpWateringInterval,_tmpImageUrl);
            _result.add(_item);
          }
          return _result;
        } finally {
          _cursor.close();
        }
      }

      @Override
      protected void finalize() {
        _statement.release();
      }
    }.getLiveData();
  }

  @Override
  public LiveData<Plant> getPlant(String plantId) {
    final String _sql = "SELECT * FROM plants WHERE id = ?";
    final RoomSQLiteQuery _statement = RoomSQLiteQuery.acquire(_sql, 1);
    int _argIndex = 1;
    if (plantId == null) {
      _statement.bindNull(_argIndex);
    } else {
      _statement.bindString(_argIndex, plantId);
    }
    return new ComputableLiveData<Plant>(__db.getQueryExecutor()) {
      private Observer _observer;

      @Override
      protected Plant compute() {
        if (_observer == null) {
          _observer = new Observer("plants") {
            @Override
            public void onInvalidated(@NonNull Set<String> tables) {
              invalidate();
            }
          };
          __db.getInvalidationTracker().addWeakObserver(_observer);
        }
        final Cursor _cursor = DBUtil.query(__db, _statement, false);
        try {
          final int _cursorIndexOfPlantId = _cursor.getColumnIndexOrThrow("id");
          final int _cursorIndexOfName = _cursor.getColumnIndexOrThrow("name");
          final int _cursorIndexOfDescription = _cursor.getColumnIndexOrThrow("description");
          final int _cursorIndexOfGrowZoneNumber = _cursor.getColumnIndexOrThrow("growZoneNumber");
          final int _cursorIndexOfWateringInterval = _cursor.getColumnIndexOrThrow("wateringInterval");
          final int _cursorIndexOfImageUrl = _cursor.getColumnIndexOrThrow("imageUrl");
          final Plant _result;
          if(_cursor.moveToFirst()) {
            final String _tmpPlantId;
            _tmpPlantId = _cursor.getString(_cursorIndexOfPlantId);
            final String _tmpName;
            _tmpName = _cursor.getString(_cursorIndexOfName);
            final String _tmpDescription;
            _tmpDescription = _cursor.getString(_cursorIndexOfDescription);
            final int _tmpGrowZoneNumber;
            _tmpGrowZoneNumber = _cursor.getInt(_cursorIndexOfGrowZoneNumber);
            final int _tmpWateringInterval;
            _tmpWateringInterval = _cursor.getInt(_cursorIndexOfWateringInterval);
            final String _tmpImageUrl;
            _tmpImageUrl = _cursor.getString(_cursorIndexOfImageUrl);
            _result = new Plant(_tmpPlantId,_tmpName,_tmpDescription,_tmpGrowZoneNumber,_tmpWateringInterval,_tmpImageUrl);
          } else {
            _result = null;
          }
          return _result;
        } finally {
          _cursor.close();
        }
      }

      @Override
      protected void finalize() {
        _statement.release();
      }
    }.getLiveData();
  }
}
