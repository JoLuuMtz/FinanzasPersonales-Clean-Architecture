
using Microsoft.AspNetCore.Mvc;
using FinanzasPersonales.Aplication;    

namespace FinanzasPersonales.API;

[Route("spends/[controller]")]
[ApiController]
public class TransactionSpendController : ControllerBase
{
    private readonly ISpendsService _spendsService;

    public TransactionSpendController(ISpendsService spendsService)
    {
        _spendsService = spendsService;
    }

}
