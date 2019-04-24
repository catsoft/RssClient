using System;

namespace Core.Database
{
    public interface IHaveId
    {
        Guid Id { get; set; }
    }
}
