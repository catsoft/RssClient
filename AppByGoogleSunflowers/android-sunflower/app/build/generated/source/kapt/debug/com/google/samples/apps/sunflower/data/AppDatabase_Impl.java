package com.google.samples.apps.sunflower.data;

import androidx.room.DatabaseConfiguration;
import androidx.room.InvalidationTracker;
import androidx.room.RoomOpenHelper;
import androidx.room.RoomOpenHelper.Delegate;
import androidx.room.util.DBUtil;
import androidx.room.util.TableInfo;
import androidx.room.util.TableInfo.Column;
import androidx.room.util.TableInfo.ForeignKey;
import androidx.room.util.TableInfo.Index;
import androidx.sqlite.db.SupportSQLiteDatabase;
import androidx.sqlite.db.SupportSQLiteOpenHelper;
import androidx.sqlite.db.SupportSQLiteOpenHelper.Callback;
import androidx.sqlite.db.SupportSQLiteOpenHelper.Configuration;
import java.lang.IllegalStateException;
import java.lang.Override;
import java.lang.String;
import java.lang.SuppressWarnings;
import java.util.Arrays;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Set;

@SuppressWarnings("unchecked")
public final class AppDatabase_Impl extends AppDatabase {
  private volatile GardenPlantingDao _gardenPlantingDao;

  private volatile PlantDao _plantDao;

  @Override
  protected SupportSQLiteOpenHelper createOpenHelper(DatabaseConfiguration configuration) {
    final SupportSQLiteOpenHelper.Callback _openCallback = new RoomOpenHelper(configuration, new RoomOpenHelper.Delegate(1) {
      @Override
      public void createAllTables(SupportSQLiteDatabase _db) {
        _db.execSQL("CREATE TABLE IF NOT EXISTS `garden_plantings` (`id` INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, `plant_id` TEXT NOT NULL, `plant_date` INTEGER NOT NULL, `last_watering_date` INTEGER NOT NULL, FOREIGN KEY(`plant_id`) REFERENCES `plants`(`id`) ON UPDATE NO ACTION ON DELETE NO ACTION )");
        _db.execSQL("CREATE  INDEX `index_garden_plantings_plant_id` ON `garden_plantings` (`plant_id`)");
        _db.execSQL("CREATE TABLE IF NOT EXISTS `plants` (`id` TEXT NOT NULL, `name` TEXT NOT NULL, `description` TEXT NOT NULL, `growZoneNumber` INTEGER NOT NULL, `wateringInterval` INTEGER NOT NULL, `imageUrl` TEXT NOT NULL, PRIMARY KEY(`id`))");
        _db.execSQL("CREATE TABLE IF NOT EXISTS room_master_table (id INTEGER PRIMARY KEY,identity_hash TEXT)");
        _db.execSQL("INSERT OR REPLACE INTO room_master_table (id,identity_hash) VALUES(42, \"af891a346ba35e9eb2713324457ca482\")");
      }

      @Override
      public void dropAllTables(SupportSQLiteDatabase _db) {
        _db.execSQL("DROP TABLE IF EXISTS `garden_plantings`");
        _db.execSQL("DROP TABLE IF EXISTS `plants`");
      }

      @Override
      protected void onCreate(SupportSQLiteDatabase _db) {
        if (mCallbacks != null) {
          for (int _i = 0, _size = mCallbacks.size(); _i < _size; _i++) {
            mCallbacks.get(_i).onCreate(_db);
          }
        }
      }

      @Override
      public void onOpen(SupportSQLiteDatabase _db) {
        mDatabase = _db;
        _db.execSQL("PRAGMA foreign_keys = ON");
        internalInitInvalidationTracker(_db);
        if (mCallbacks != null) {
          for (int _i = 0, _size = mCallbacks.size(); _i < _size; _i++) {
            mCallbacks.get(_i).onOpen(_db);
          }
        }
      }

      @Override
      public void onPreMigrate(SupportSQLiteDatabase _db) {
        DBUtil.dropFtsSyncTriggers(_db);
      }

      @Override
      public void onPostMigrate(SupportSQLiteDatabase _db) {
      }

      @Override
      protected void validateMigration(SupportSQLiteDatabase _db) {
        final HashMap<String, TableInfo.Column> _columnsGardenPlantings = new HashMap<String, TableInfo.Column>(4);
        _columnsGardenPlantings.put("id", new TableInfo.Column("id", "INTEGER", true, 1));
        _columnsGardenPlantings.put("plant_id", new TableInfo.Column("plant_id", "TEXT", true, 0));
        _columnsGardenPlantings.put("plant_date", new TableInfo.Column("plant_date", "INTEGER", true, 0));
        _columnsGardenPlantings.put("last_watering_date", new TableInfo.Column("last_watering_date", "INTEGER", true, 0));
        final HashSet<TableInfo.ForeignKey> _foreignKeysGardenPlantings = new HashSet<TableInfo.ForeignKey>(1);
        _foreignKeysGardenPlantings.add(new TableInfo.ForeignKey("plants", "NO ACTION", "NO ACTION",Arrays.asList("plant_id"), Arrays.asList("id")));
        final HashSet<TableInfo.Index> _indicesGardenPlantings = new HashSet<TableInfo.Index>(1);
        _indicesGardenPlantings.add(new TableInfo.Index("index_garden_plantings_plant_id", false, Arrays.asList("plant_id")));
        final TableInfo _infoGardenPlantings = new TableInfo("garden_plantings", _columnsGardenPlantings, _foreignKeysGardenPlantings, _indicesGardenPlantings);
        final TableInfo _existingGardenPlantings = TableInfo.read(_db, "garden_plantings");
        if (! _infoGardenPlantings.equals(_existingGardenPlantings)) {
          throw new IllegalStateException("Migration didn't properly handle garden_plantings(com.google.samples.apps.sunflower.data.GardenPlanting).\n"
                  + " Expected:\n" + _infoGardenPlantings + "\n"
                  + " Found:\n" + _existingGardenPlantings);
        }
        final HashMap<String, TableInfo.Column> _columnsPlants = new HashMap<String, TableInfo.Column>(6);
        _columnsPlants.put("id", new TableInfo.Column("id", "TEXT", true, 1));
        _columnsPlants.put("name", new TableInfo.Column("name", "TEXT", true, 0));
        _columnsPlants.put("description", new TableInfo.Column("description", "TEXT", true, 0));
        _columnsPlants.put("growZoneNumber", new TableInfo.Column("growZoneNumber", "INTEGER", true, 0));
        _columnsPlants.put("wateringInterval", new TableInfo.Column("wateringInterval", "INTEGER", true, 0));
        _columnsPlants.put("imageUrl", new TableInfo.Column("imageUrl", "TEXT", true, 0));
        final HashSet<TableInfo.ForeignKey> _foreignKeysPlants = new HashSet<TableInfo.ForeignKey>(0);
        final HashSet<TableInfo.Index> _indicesPlants = new HashSet<TableInfo.Index>(0);
        final TableInfo _infoPlants = new TableInfo("plants", _columnsPlants, _foreignKeysPlants, _indicesPlants);
        final TableInfo _existingPlants = TableInfo.read(_db, "plants");
        if (! _infoPlants.equals(_existingPlants)) {
          throw new IllegalStateException("Migration didn't properly handle plants(com.google.samples.apps.sunflower.data.Plant).\n"
                  + " Expected:\n" + _infoPlants + "\n"
                  + " Found:\n" + _existingPlants);
        }
      }
    }, "af891a346ba35e9eb2713324457ca482", "4cd91bcacf974ceb7b5a8b3b51ea4fa2");
    final SupportSQLiteOpenHelper.Configuration _sqliteConfig = SupportSQLiteOpenHelper.Configuration.builder(configuration.context)
        .name(configuration.name)
        .callback(_openCallback)
        .build();
    final SupportSQLiteOpenHelper _helper = configuration.sqliteOpenHelperFactory.create(_sqliteConfig);
    return _helper;
  }

  @Override
  protected InvalidationTracker createInvalidationTracker() {
    final HashMap<String, String> _shadowTablesMap = new HashMap<String, String>(0);
    HashMap<String, Set<String>> _viewTables = new HashMap<String, Set<String>>(0);
    return new InvalidationTracker(this, _shadowTablesMap, _viewTables, "garden_plantings","plants");
  }

  @Override
  public void clearAllTables() {
    super.assertNotMainThread();
    final SupportSQLiteDatabase _db = super.getOpenHelper().getWritableDatabase();
    boolean _supportsDeferForeignKeys = android.os.Build.VERSION.SDK_INT >= android.os.Build.VERSION_CODES.LOLLIPOP;
    try {
      if (!_supportsDeferForeignKeys) {
        _db.execSQL("PRAGMA foreign_keys = FALSE");
      }
      super.beginTransaction();
      if (_supportsDeferForeignKeys) {
        _db.execSQL("PRAGMA defer_foreign_keys = TRUE");
      }
      _db.execSQL("DELETE FROM `garden_plantings`");
      _db.execSQL("DELETE FROM `plants`");
      super.setTransactionSuccessful();
    } finally {
      super.endTransaction();
      if (!_supportsDeferForeignKeys) {
        _db.execSQL("PRAGMA foreign_keys = TRUE");
      }
      _db.query("PRAGMA wal_checkpoint(FULL)").close();
      if (!_db.inTransaction()) {
        _db.execSQL("VACUUM");
      }
    }
  }

  @Override
  public GardenPlantingDao gardenPlantingDao() {
    if (_gardenPlantingDao != null) {
      return _gardenPlantingDao;
    } else {
      synchronized(this) {
        if(_gardenPlantingDao == null) {
          _gardenPlantingDao = new GardenPlantingDao_Impl(this);
        }
        return _gardenPlantingDao;
      }
    }
  }

  @Override
  public PlantDao plantDao() {
    if (_plantDao != null) {
      return _plantDao;
    } else {
      synchronized(this) {
        if(_plantDao == null) {
          _plantDao = new PlantDao_Impl(this);
        }
        return _plantDao;
      }
    }
  }
}
