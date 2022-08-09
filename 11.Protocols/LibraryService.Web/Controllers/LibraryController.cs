using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryService.Web.Models;
using LibraryService.Web.DAL.Interfaces;
using LibraryService.Web.ViewModels;
using LibraryService.Web.Mappings;
using System.Runtime.CompilerServices;

namespace LibraryService.Web.Controllers;

public class LibraryController : Controller
{
    private readonly ILogger<LibraryController> _logger;
    private readonly ILibraryRepository _repository;

    private void LogError(Exception ex, [CallerMemberName] string methodName = null!) => _logger.LogError(ex, "Ошибка выполнения {0}", methodName);

    public LibraryController(ILogger<LibraryController> logger, ILibraryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public IActionResult Index(SearchType searchType, string searchString)
    {
        var viewModel = new BookCategoryViewModel
        {
            Books = Array.Empty<BookViewModel>()
        };

        if (!string.IsNullOrEmpty(searchString) && searchString.Length >= 3)
        {
            switch (searchType)
            {
                case SearchType.Title:
                    viewModel.Books = _repository.GetByTitle(searchString).ToView();
                    break;
                case SearchType.Author:
                    viewModel.Books = _repository.GetByAuthor(searchString).ToView();
                    break;
                case SearchType.Category:
                    viewModel.Books = _repository.GetByCategory(searchString).ToView();
                    break;
            }
        }
        else
        {
            viewModel.Books = _repository.GetAll().ToView();

        }
        return View(viewModel);
    }

    public IActionResult Create() => View("Edit", new BookViewModel());

    public IActionResult Edit(string id)
    {
        try
        {
            var book = _repository.GetById(id);

            if (book is null)
                return NotFound();

            var bookViewModel = book.ToView();

            return View(bookViewModel);
        }
        catch (Exception ex)
        {
            LogError(ex);
            throw;
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(BookViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var book = model.ToModel()!;

            if (string.IsNullOrEmpty(book.Id))
                _repository.Add(book);
            else
                _repository.Update(book);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            LogError(ex);
            throw;
        }
    }

    // GET: HarwareController/Delete/5
    public IActionResult Delete(string id)
    {
        try
        {
            var book = _repository.GetById(id);

            if (book is null)
                return NotFound();


            var bookViewModel = book.ToView();

            return View(bookViewModel);
        }
        catch (Exception ex)
        {
            LogError(ex);
            throw;
        }

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(string id)
    {
        try
        {
            var book = _repository.GetById(id);

            if (book is null)
                return NotFound();

            _repository.Delete(book);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            LogError(ex);
            throw;
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
