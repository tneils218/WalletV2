using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WalletV2.Controllers.Request;
using WalletV2.Services;
using WalletV2.Services.DTOs;

namespace WalletV2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalletController : ControllerBase
{
    private readonly IWalletService _walletService;
    private readonly IWalletQueueService _walletQueueService;
    private readonly KafkaProducer<Null, string> _kafkaProducer;

    public WalletController(IWalletService walletService, IWalletQueueService walletQueueService, KafkaProducer<Null, string> kafkaProduce)
    {
        _walletService = walletService;
        _walletQueueService = walletQueueService;
        _kafkaProducer = kafkaProduce;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWallet([FromQuery] string? id = "")
    {
        var wallets = await _walletService.GetAllWallet(id ?? "");
        return Ok(wallets);
    }

    [HttpPost]
    public async Task<IActionResult> CreateWallet(WalletRequest request)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        try
        {
            var walletDto = WalletDto.Create(request.AccountId);
            var wallets = await _walletService.CreateWallet(walletDto);
            return CreatedAtAction(nameof(CreateWallet), new { }, wallets);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> TransferWallet(int id, int walletId, WalletTransferRequest request)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        try
        {
            //    await _walletQueueService.Queue(WalletQueueDto.Create(
            //                request.ActionTypeId, walletId, request.Amount, id, request.ReceiverId, request.ReceiverWalletId));
            var data = JsonSerializer.Serialize((WalletQueueDto.Create(
                             request.ActionTypeId, walletId, request.Amount, id, request.ReceiverId, request.ReceiverWalletId)));
            var message = new Message<Null, string> { Value = data };

            _kafkaProducer.Produce(message, "wallet-input");

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddMoney([FromBody] WalletAddMoneyRequest request)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        try
        {
            await _walletQueueService.Queue(WalletQueueDto.CreateForAdd(
                        request.WalletId, request.Amount, request.ActionTypeId));
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("withdraw")]
    public async Task<IActionResult> WithdrawMoney([FromBody] WalletWithdrawMoneyRequest request)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        try
        {
            await _walletQueueService.Queue(WalletQueueDto.CreateForAdd(
                        request.WalletId, request.Amount, request.ActionTypeId));
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}