using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnackBar.OrderService.WebApi.Models;
using System.Threading;

namespace SnackBar.OrderService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Article[]> GetAll(
            CancellationToken cancellationToken = default)
        {
            var items = new Article[]
            {
                new Article { Id = 1, Name = "Cheese Burger", Icon = "lunch_dining" },
                new Article { Id = 2, Name = "Pommes", Icon = "lunch_dining" },
                new Article { Id = 3, Name = "Pizza", Icon = "local_pizza" },
            };

            return StatusCode(StatusCodes.Status200OK, items);
        }
    }
}
