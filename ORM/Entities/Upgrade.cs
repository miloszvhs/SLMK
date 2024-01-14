using ORM.Contract.Enums;

namespace ORM.Entities;

public class Upgrade
{
    public virtual int Id { get; set; }
    public virtual int Number { get; set; }
    public virtual DatabaseConnectionKeys ConnectionKey { get; set; }
}