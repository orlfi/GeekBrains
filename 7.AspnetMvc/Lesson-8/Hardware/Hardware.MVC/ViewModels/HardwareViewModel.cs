namespace Hardwares.MVC.ViewModels
{
    public class HardwareViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime InstallationDate { get; set; }

        public decimal Cost { get; set; }
    }
}
