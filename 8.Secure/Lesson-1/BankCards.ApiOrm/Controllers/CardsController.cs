using BankCards.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using BankCards.ApiOrm.Mappers;
using BankCards.ApiOrm.DTO.Cards;
using System.Runtime.CompilerServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankCards.ApiOrm.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CardsController : ControllerBase
{
    private readonly ICardRepository _db;

    private readonly ILogger<CardsController> _logger;

    private void LogError(Exception ex, [CallerMemberName] string methodName = default) =>
        _logger.LogError(ex, "Ошибка выполнения {0}", methodName);

    public CardsController(ICardRepository db, ILogger<CardsController> logger)
    {
        this._db = db;
        this._logger = logger;
    }

    // GET: api/<CardsController>
    [HttpGet]
    public async Task<ActionResult<CardsResponse>> Get()
    {
        try
        {
            var cards = await _db.GetAllAsync();
            var result = cards.ToResponse();
            return Ok(result);
        }
        catch (Exception ex)
        {
            LogError(ex);
            return BadRequest("Ошибка получения списка карт");
        }
    }

    // GET api/<CardsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CardResponse>> Get(int id)
    {
        try
        {
            var card = await _db.GetAsync(id);
            if (card is null)
                return NotFound("Карта не найдена");

            var result = card.ToResponse();
            return Ok(result);
        }
        catch (Exception ex)
        {
            LogError(ex);
            return BadRequest($"Ошибка получения карты {id}");
        }
    }

    // POST api/<CardsController>
    [HttpPost]
    public async Task<ActionResult<CardResponse>> Post([FromBody] CardCreateRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = request.ToCard();
            await _db.CreateAsync(model);
            _logger.LogInformation("Карта успешно создана");

            var result = model.ToResponse();
            return Ok(result);

        }
        catch (Exception ex)
        {
            LogError(ex);
            return BadRequest($"Ошибка создания новой карты");
        }
    }

    // PUT api/<CardsController>
    [HttpPut]
    public async Task<ActionResult<CardResponse>> Put([FromBody] CardUpdateRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = request.ToCard();
            await _db.UpdateAsync(model);
            _logger.LogInformation("Карта {0} успешно изменена", request.Id);

            var result = model.ToResponse();
            return Ok(result);

        }
        catch (Exception ex)
        {
            LogError(ex);
            return BadRequest($"Ошибка изменения карты {request.Id}");
        }
    }

    // DELETE api/<CardsController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _db.DeleteAsync(id);
            _logger.LogInformation("Карта {0} успешно удалена", id);

            return Ok();
        }
        catch (Exception ex)
        {
            LogError(ex);
            return BadRequest($"Ошибка удаления карты {id}");
        }
    }
}
