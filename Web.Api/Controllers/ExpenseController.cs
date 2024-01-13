using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using Web.Data.Entity;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly ILogger<ExpenseController> _logger;

        public ExpenseController(ILogger<ExpenseController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("AddExpense")]
        public IActionResult AddExpense([FromBody] Expense expense)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (expense.EmployeeId.ToString() != userId)
            {
                // Kullanıcı sadece kendi masrafını ekleyebilir. Başka bir EmployeeId kullanılmışsa hata döndür.
                return BadRequest("Sadece kendi masrafınızı ekleyebilirsiniz.");
            }

            // Diğer işlemleri gerçekleştir...

            // Örneğin, Expense'i veritabanına ekleyebilir veya iş mantığınıza göre başka bir şey yapabilirsiniz.

            return Ok("Masraf başarıyla eklendi.");
        }
    }
}