namespace Hardwares.Interfaces.Common;

public interface INamedEntity : IEntity
{
    string Name { get; set; }
}
