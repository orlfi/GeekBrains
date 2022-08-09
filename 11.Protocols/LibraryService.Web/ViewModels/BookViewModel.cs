using System;
using System.Collections.Generic;
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

        public string Title { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Lang { get; set; } = string.Empty;

        public int Pages { get; set; }

        public int AgeLimit { get; set; }

        public string Authors { get; init; } = string.Empty;

        public string PublicationDate { get; set; } = string.Empty;
    }
}