using BankCards.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using BankCards.ApiOrm.DTO.Cards;
using AutoMapper;
using BankCards.Domain;
using BankCards.Domain.Mongo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankCards.ApiOrm.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _db;
    private readonly IMapper _mapper;
    private readonly ILogger<BooksController> _logger;

    public BooksController(IBookRepository db, IMapper mapper, ILogger<BooksController> logger)
    {
        _db = db;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Возвращает список книг
    /// GET: api/Books
    /// </summary>
    /// <returns>IEnumerable of BookResponse</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookResponse>>> Get()
    {
        _logger.LogInformation("Запрос списка всех книг");

        var cards = await _db.GetAllAsync();
        var result = _mapper.Map<IEnumerable<BookResponse>>(cards);
        return Ok(result);
    }

    /// <summary>
    /// Возвращает книгу по ID
    /// GET api/Books/5
    /// </summary>
    /// <param name="id"></param>
    /// <returns>BookResponse</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<BookResponse>> Get(string id)
    {
        _logger.LogInformation("Запрос информации по книге с id {0}", id);

        var card = await _db.GetByIdAsync(id);
        if (card is null)
            return NotFound("Книга не найдена");

        var result = _mapper.Map<BookResponse>(card);
        return Ok(result);
    }


    /// <summary>
    /// Создает новую книгу
    /// POST api/Books
    /// </summary>
    /// <param name="request">создаваемый объект</param>
    /// <returns>BookResponse</returns>
    [HttpPost]
    public async Task<ActionResult<BookResponse>> Post([FromBody] BookCreateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var model = _mapper.Map<Book>(request);
        await _db.CreateAsync(model);
        _logger.LogInformation("Книга успешно создана");

        var result = _mapper.Map<BookResponse>(model);
        return Ok(result);
    }

    /// <summary>
    /// Изменяет книгу
    /// PUT api/Books
    /// </summary>
    /// <param name="request">изменяемый объект</param>
    /// <returns>BookResponse</returns>
    [HttpPut]
    public async Task<ActionResult<BookResponse>> Put([FromBody] BookUpdateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var model = _mapper.Map<Book>(request);
        await _db.UpdateAsync(model);
        _logger.LogInformation("Книга {0} успешно изменена", request.Id);

        var result = _mapper.Map<BookResponse>(model);
        return Ok(result);
    }

    /// <summary>
    /// Удаляет книгу
    /// DELETE api/Books/5
    /// </summary>
    /// <param name="id">ID книги</param>
    /// <returns>BookResponse</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        await _db.DeleteAsync(id);
        _logger.LogInformation("Книга {0} успешно удалена", id);

        return Ok();
    }
}
