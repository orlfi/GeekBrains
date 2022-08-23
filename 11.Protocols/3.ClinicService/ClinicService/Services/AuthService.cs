﻿using ClinicService.Models.Responses;
using ClinicService.Protos;
using ClinicService.Services.Interfaces;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using static ClinicService.Protos.AuthenticateService;

namespace ClinicService.Services;

[Authorize]
public class AuthService : AuthenticateServiceBase
{
    private readonly IAuthenticateService _authenticateService;

    public AuthService(IAuthenticateService authenticateService)
    {
        _authenticateService = authenticateService;
    }

    [AllowAnonymous]
    public override Task<Protos.AuthenticationResponse> Login(Protos.AuthenticationRequest request, ServerCallContext context)
    {
        var authenticationResponse = _authenticateService.Login(new Models.Requests.AuthenticationRequest()
        {
            Login = request.UserName,
            Password = request.Password
        });

        var response = new Protos.AuthenticationResponse
        {
            Status = (int)authenticationResponse.Status,
        };

        if (authenticationResponse.Status == 0)
        {
            context.ResponseTrailers.Add("X-Session-Token", authenticationResponse.SessionInfo.SessionToken);
            response.SessionInfo = new SessionInfo
            {
                SessionId = authenticationResponse.SessionInfo.SessionId,
                SessionToken = authenticationResponse.SessionInfo.SessionToken,
                Account = new AccountDto
                {
                    AccountId = authenticationResponse.SessionInfo.Account.AccountId,
                    EMail = authenticationResponse.SessionInfo.Account.EMail,
                    FirstName = authenticationResponse.SessionInfo.Account.FirstName,
                    LastName = authenticationResponse.SessionInfo.Account.LastName,
                    SecondName = authenticationResponse.SessionInfo.Account.SecondName,
                    Locked = authenticationResponse.SessionInfo.Account.Locked
                }
            };
        }

        return Task.FromResult(response);
    }

    public override Task<Protos.GetSessionInfoResponse> GetSessionInfo(Protos.GetSessionInfoRequest request, ServerCallContext context)
    {
        var authorizationHeader = context.RequestHeaders.FirstOrDefault(header => header.Key == "Authorization");
        if (authorizationHeader == null)
            return Task.FromResult(new GetSessionInfoResponse
            {
                ErrCode = 10001
            });

        //Bearer XXXXXXXXXXXXXXXXXXXXXXXX
        if (AuthenticationHeaderValue.TryParse(authorizationHeader.Value, out var headerValue))
        {
            var scheme = headerValue.Scheme; // "Bearer"
            var sessionToken = headerValue.Parameter; // Token
            if (string.IsNullOrEmpty(sessionToken))
                return Task.FromResult(new GetSessionInfoResponse
                {
                    ErrCode = 10002
                });

            var sessionInfo = _authenticateService.GetSessionInfo(sessionToken);
            if (sessionInfo == null)
                return Task.FromResult(new GetSessionInfoResponse
                {
                    ErrCode = 10003
                });

            return Task.FromResult(new GetSessionInfoResponse
            {
                SessionInfo = new SessionInfo
                {
                    SessionId = sessionInfo.SessionId,
                    SessionToken = sessionInfo.SessionToken,
                    Account = new AccountDto
                    {
                        AccountId = sessionInfo.Account.AccountId,
                        EMail = sessionInfo.Account.EMail,
                        FirstName = sessionInfo.Account.FirstName,
                        LastName = sessionInfo.Account.LastName,
                        SecondName = sessionInfo.Account.SecondName,
                        Locked = sessionInfo.Account.Locked
                    }
                }
            });
        }
        else
            return Task.FromResult(new GetSessionInfoResponse
            {
                ErrCode = 10004
            });
    }
}
