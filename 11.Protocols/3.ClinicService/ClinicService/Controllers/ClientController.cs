﻿using ClinicService.DAL.Interfaces;
using ClinicService.Data;
using ClinicService.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientRepository _clientRepository;
    private readonly ILogger<ClientController> _logger;

    public ClientController(IClientRepository clientRepository,
        ILogger<ClientController> logger)
    {
        _logger = logger;
        _clientRepository = clientRepository;
    }

    [HttpGet("get-all")]
    [ProducesResponseType(typeof(IList<Client>), StatusCodes.Status200OK)]
    public IActionResult GetAll() =>
        Ok(_clientRepository.GetAll());

    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
    public IActionResult GetById([FromRoute] int clientId) =>
        Ok(_clientRepository.GetById(clientId));

    [HttpPost("create")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public IActionResult Create([FromBody] CreateClientRequest createRequest) =>
        Ok(_clientRepository.Add(new Client
        {
            Document = createRequest.Document,
            Surname = createRequest.Surname,
            FirstName = createRequest.FirstName,
            Patronymic = createRequest.Patronymic
        }));

    [HttpPut("update")]
    public IActionResult Update([FromBody] UpdateClientRequest updateRequest)
    {
        _clientRepository.Update(new Client
        {
            ClientId = updateRequest.ClientId,
            Surname = updateRequest.Surname,
            FirstName = updateRequest.FirstName,
            Patronymic = updateRequest.Patronymic
        });
        return Ok();
    }

    [HttpDelete("delete")]
    public IActionResult Delete([FromQuery] int clientId)
    {
        _clientRepository.Delete(clientId);
        return Ok();
    }
}
