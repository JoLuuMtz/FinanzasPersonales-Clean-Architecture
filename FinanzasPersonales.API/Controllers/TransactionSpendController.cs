using FinanciasPersonalesApiRest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanciasPersonales.API;

[Route("spends/[controller]")]
[ApiController]
public class TransactionSpendController : ControllerBase
{
    private readonly SpendsService _spendsService;

    public TransactionSpendController(SpendsService spendsService)
    {
        _spendsService = spendsService;
    }

}
