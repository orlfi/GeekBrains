using Hardwares.Interfaces.Common;

namespace Hardwares.Domain.Base;

public class NamedEntity: Entity, INamedEntity
{
    public string Name { get; set; } = null!;
}
