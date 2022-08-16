using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryService.Web.Models;
using LibraryService.Web.ViewModels;
using LibraryService.Web.Mappings;
using System.Runtime.CompilerServices;
using LibraryServiceReference;

namespace LibraryService.Web.Controllers;

public class LibraryController : Controller
{
    private readonly ILogger<LibraryController> _logger;
    private readonly LibraryWebServiceSoapClient _client;

    private void LogError(Exception ex, [CallerMemberName] string methodName = null!) => _logger.LogError(ex, "Ошибка выполнения {0}", methodName);

    public LibraryController(ILogger<LibraryController> logger)
    {
        _logger = logger;
        _client = new LibraryWebServiceSoapClient(LibraryWebServiceSoapClient.EndpointConfiguration.LibraryWebServiceSoap12);

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
                    viewModel.Books = _client.GetByTitle(searchString).ToView();
                    break;
                case SearchType.Author:
                    viewModel.Books = _client.GetByAuthor(searchString).ToView();
                    break;
                case SearchType.Category:
                    viewModel.Books = _client.GetByCategory(searchString).ToView();
                    break;
            }
        }
        else
        {
            viewModel.Books = _client.GetAll().ToView();

        }
        
        return View(viewModel);
    }

    public IActionResult Create() => View("Edit", new BookViewModel());

    public IActionResult Edit(string id)
    {
        try
        {
            var book = _client.GetById(id);

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
                _client.Add(book);
            else
                _client.Update(book);

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
            var book = _client.GetById(id);

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
            var book = _client.GetById(id);

            if (book is null)
                return NotFound();

            _client.Delete(book);

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
