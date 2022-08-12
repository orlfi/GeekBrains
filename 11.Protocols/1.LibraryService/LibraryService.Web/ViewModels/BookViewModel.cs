using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Web.ViewModels
{
    public class BookViewModel
    {
        // [HiddenInput(DisplayValue = false)]
        public string? Id { get; set; }

        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Обязательно")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Обязательно")]
        public string Category { get; set; } = string.Empty;

        [Display(Name = "Язык")]
        public string Lang { get; set; } = string.Empty;

        [Display(Name = "Стоимость")]
        [Required(ErrorMessage = "Обязательно")]
        [Range(0, 999999999.99)]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public int Pages { get; set; }

        [Display(Name = "Возрастной ценз")]
        [Required(ErrorMessage = "Обязательно")]
        [Range(0, 21)]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public int AgeLimit { get; set; }

        [Display(Name = "Авторы")]
        public string Authors { get; init; } = string.Empty;

        [Display(Name = "Дата публикации")]
        [Required(ErrorMessage = "Обязательно")]
        public string PublicationDate { get; set; } = string.Empty;
    }
}