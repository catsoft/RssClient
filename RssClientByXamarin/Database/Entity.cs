using System;

namespace Database
{
    public class Entity : IEntity
    {
        [SQLite.PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
