using Hardwares.Domain;
using Hardwares.MVC.ViewModels;

namespace Hardwares.MVC.Infrastructure.Mapping
{
    public static class HardwaresMapping
    {
        public static HardwareViewModel? ToView(this Hardware? hardware) => hardware is null
            ? null
            : new HardwareViewModel
            {
                Id = hardware.Id,
                Name = hardware.Name,
                Description = hardware.Description,
                InstallationDate = hardware.InstallationDate,
                Cost = hardware.Cost,
            };

        public static Hardware? FromView(this HardwareViewModel? hardware) => hardware is null
            ? null
            : new Hardware
            {
                Id = hardware.Id,
                Name = hardware.Name,
                Description = hardware.Description,
                InstallationDate = hardware.InstallationDate,
                Cost = hardware.Cost,
            };

        public static IEnumerable<HardwareViewModel?> ToView(this IEnumerable<Hardware?> hardwares) => hardwares.Select(x => x.ToView());

        public static IEnumerable<Hardware?> FromView(this IEnumerable<HardwareViewModel?> hardwares) => hardwares.Select(x => x.FromView());
    }
}
