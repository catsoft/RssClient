namespace Database
{
    public class Entity : IEntity
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
    }
}
