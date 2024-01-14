using System.Reflection;

namespace ORM.Contract.Interfaces;

public interface IMappingFromAssembly
{
    Assembly Assembly { get; }
}