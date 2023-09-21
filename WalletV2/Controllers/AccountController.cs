using Microsoft.AspNetCore.Mvc;
using WalletV2.Controllers.Request;
using WalletV2.Services;
using WalletV2.Services.DTOs;

namespace WalletV2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;

    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var accounts = await _accountService.GetAccounts();
            return Ok(accounts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);

        }

    }
    [HttpPost]
    public async Task<IActionResult> CreateAccount(AccountRequest request)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        try
        {
            var accountDto = AccountDto.Create(request.UserName, request.FullName, request.Email, request.Dob, request.AccountTypeId);
            var account = await _accountService.CreateAccount(accountDto);
            return CreatedAtAction(nameof(CreateAccount), new { }, account);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);

        }

    }
    [HttpPut("{id}/status")]
    public async Task<IActionResult> ActiveStatus(int id)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        try
        {
            var accounts = await _accountService.ActiveStatus(id);
            return Ok(accounts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);

        }

    }
}