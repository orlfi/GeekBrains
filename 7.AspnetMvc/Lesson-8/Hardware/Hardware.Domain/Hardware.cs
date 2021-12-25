using Hardware.Domain.Base;

namespace Hardware.Domain;

public class Hardware : NamedEntity
{
    public string Description { get; set; }

    public decimal  Cost { get; set; }

    public DateTime InstallationDate { get; set; }

    public HardwareType HardwareType { get; set; }

    public Office Office { get; set; }
}