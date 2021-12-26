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
        public DateTime InstallationDate { get; set; }

        [Display(Name = "Стоимость")]
        [Required(ErrorMessage = "Обязательно")]
        [Range(0, 9999999.99)]
        [DisplayFormat(DataFormatString = "{0:C1}")]
        //[RegularExpression(@"^\d+(\.\d{1,2})?$")]
        //[Range(0, 9999999999999999.99)]
        public decimal Cost { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return ValidationResult.Success!;

        }
    }
}
