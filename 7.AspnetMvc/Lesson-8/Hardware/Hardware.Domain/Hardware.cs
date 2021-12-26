using Hardwares.Domain.Base;

namespace Hardwares.Domain;

/// <summary>
/// Аппаратное обеспечение
/// </summary>
public class Hardware : NamedEntity
{
    public string Description { get; set; } = null!;

    public decimal  Cost { get; set; }

    public DateTime InstallationDate { get; set; }
}