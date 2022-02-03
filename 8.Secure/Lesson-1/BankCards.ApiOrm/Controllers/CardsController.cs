using BankCards.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using BankCards.ApiOrm.DTO.Cards;
using AutoMapper;
using BankCards.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankCards.ApiOrm.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class CardsController : ControllerBase
{
    private readonly ICardRepository _db;
    private readonly IMapper _mapper;
    private readonly ILogger<CardsController> _logger;

    public CardsController(ICardRepository db, IMapper mapper, ILogger<CardsController> logger)
    {
        _db = db;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Возвращает список банковских карт
    /// GET: api/Cards
    /// </summary>
    /// <returns>IEnumerable of CardResponse</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CardResponse>>> Get()
    {
        _logger.LogInformation("Запрос списка всех карт");

        var cards = await _db.GetAllAsync();
        var result = _mapper.Map<IEnumerable<CardResponse>>(cards);
        return Ok(result);
    }




    /// <summary>
    /// Возвращает карту по ID
    /// GET api/Cards/5
    /// </summary>
    /// <param name="id"></param>
    /// <returns>CardResponse</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<CardResponse>> Get(int id)
    {
        _logger.LogInformation("Запрос информации по карте с id {0}", id);

        var card = await _db.GetByIdAsync(id);
        if (card is null)
            return NotFound("Карта не найдена");

        var result = _mapper.Map<CardResponse>(card);
        return Ok(result);
    }


    /// <summary>
    /// Возвращает карту по номеру
    /// GET api/Cards/GetByNumber/%234234%
    /// </summary>
    /// <param name="pattern">шаблон поиска может содержать подстановочные знаки %</param>
    /// <returns>CardResponse</returns>
    [HttpGet("GetByNumber/{pattern}")]
    public async Task<ActionResult<CardResponse>> GetByNumber(string pattern)
    {
        var card = await _db.GetByNumberAsync(pattern);
        if (card is null)
            return NotFound("Карта не найдена");

        var result = _mapper.Map<CardResponse>(card);
        return Ok(result);
    }

    /// <summary>
    /// Создает новую банковскую карту
    /// POST api/Cards
    /// </summary>
    /// <param name="request">создаваемый объект</param>
    /// <returns>CardResponse</returns>
    [HttpPost]
    public async Task<ActionResult<CardResponse>> Post([FromBody] CardCreateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var model = _mapper.Map<Card>(request);
        await _db.CreateAsync(model);
        _logger.LogInformation("Карта успешно создана");

        var result = _mapper.Map<CardResponse>(model);
        return Ok(result);
    }

    // PUT api/<CardsController>
    /// <summary>
    /// Изменяет банковскую карту
    /// PUT api/Cards
    /// </summary>
    /// <param name="request">изменяемый объект</param>
    /// <returns>CardResponse</returns>
    [HttpPut]
    public async Task<ActionResult<CardResponse>> Put([FromBody] CardUpdateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var model = _mapper.Map<Card>(request);
        await _db.UpdateAsync(model);
        _logger.LogInformation("Карта {0} успешно изменена", request.Id);

        var result = _mapper.Map<CardResponse>(model);
        return Ok(result);
    }

    /// <summary>
    /// Удаляет банковскую карту
    /// DELETE api/Cards/5
    /// </summary>
    /// <param name="id">ID карты</param>
    /// <returns>CardResponse</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _db.DeleteAsync(id);
        _logger.LogInformation("Карта {0} успешно удалена", id);

        return Ok();
    }
}
