using System;
using Realms;

namespace Database
{
    public class Entity : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
