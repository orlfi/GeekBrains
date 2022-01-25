﻿using BankCards.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using BankCards.ApiOrm.Mappers;
using BankCards.ApiOrm.DTO.Cards;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankCards.ApiOrm.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
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

    /// <summary>
    /// Возвращает список банковских карт
    /// GET: api/<CardsController> 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CardResponse>>> Get()
    {
        _logger.LogInformation("Запрос списка всех карт");

        var cards = await _db.GetAllAsync();
        var result = cards.ToResponse();
        return Ok(result);
    }

    // GET api/<CardsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CardResponse>> Get(int id)
    {
        _logger.LogInformation("Запрос информации по карте с id {0}", id);

        var card = await _db.GetByIdAsync(id);
        if (card is null)
            return NotFound("Карта не найдена");

        var result = card.ToResponse();
        return Ok(result);
    }


    // GET api/<CardsController>/%234234%
    [HttpGet("GetByNumber/{pattern}")]
    public async Task<ActionResult<CardResponse>> GetByNumber(string pattern)
    {
        var card = await _db.GetByNumberAsync(pattern);
        if (card is null)
            return NotFound("Карта не найдена");

        var result = card.ToResponse();
        return Ok(result);
    }

    // POST api/<CardsController>
    [HttpPost]
    public async Task<ActionResult<CardResponse>> Post([FromBody] CardCreateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var model = request.ToCard();
        await _db.CreateAsync(model);
        _logger.LogInformation("Карта успешно создана");

        var result = model.ToResponse();
        return Ok(result);
    }

    // PUT api/<CardsController>
    [HttpPut]
    public async Task<ActionResult<CardResponse>> Put([FromBody] CardUpdateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var model = request.ToCard();
        await _db.UpdateAsync(model);
        _logger.LogInformation("Карта {0} успешно изменена", request.Id);

        var result = model.ToResponse();
        return Ok(result);
    }

    // DELETE api/<CardsController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _db.DeleteAsync(id);
        _logger.LogInformation("Карта {0} успешно удалена", id);

        return Ok();
    }
}
