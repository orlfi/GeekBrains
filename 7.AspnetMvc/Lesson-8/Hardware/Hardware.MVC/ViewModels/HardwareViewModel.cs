using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Hardwares.MVC.ViewModels
{
    public class HardwareViewModel : IValidatableObject
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Обязательно")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Длина от 2 до 100 символов")]
        public string Name { get; set; } = null!;

        [Display(Name = "Характеристики")]
        [Required(ErrorMessage = "Обязательно")]
        public string Description { get; set; } = null!;

        [Display(Name = "Дата установки")]
        [Required(ErrorMessage = "Обязательно")]
        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime InstallationDate { get; set; }

        [Display(Name = "Стоимость")]
        [Required(ErrorMessage = "Обязательно")]
        [Range(0, 999999999.99)]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal Cost { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return ValidationResult.Success!;

        }
    }
}
