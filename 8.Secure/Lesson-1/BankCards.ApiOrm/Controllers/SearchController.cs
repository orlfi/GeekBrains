using BankCards.ApiOrm.DTO.Search;
using BankCards.Domain.Mongo;
using BankCards.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankCards.ApiOrm.Controllers;

public class SearchController : Controller
{
    private readonly IElasticRepository _db;

    public SearchController(IElasticRepository db)
    {
        _db = db;
    }

    //GET: SearchController/SearchText
   [HttpGet]
    public ActionResult Index(string text)
    {
        var result =  _db.Search(text);

        return View(result);
    }
}
