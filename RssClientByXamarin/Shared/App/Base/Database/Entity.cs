using Newtonsoft.Json;

namespace Shared.App.Base.Database
{
    public class Entity : IEntity
    {
        [JsonProperty("LocalId")]
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
    }
}
