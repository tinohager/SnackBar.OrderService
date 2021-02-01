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
                new Article { Id = 1, Name = "Hamburger", Icon = "lunch_dining" },
                new Article { Id = 2, Name = "Cheese Burger", Icon = "lunch_dining" },
                new Article { Id = 3, Name = "American Burger", Icon = "lunch_dining" },
                new Article { Id = 4, Name = "Pommes", Icon = "restaurant" },
                new Article { Id = 5, Name = "Currywurst", Icon = "local_pizza" },
                new Article { Id = 6, Name = "Chicken Nugets", Icon = "tapas" },
                new Article { Id = 7, Name = "Hot Dog", Icon = "restaurant" },
                new Article { Id = 8, Name = "Chicken Sandwich", Icon = "bakery_dining" },
                new Article { Id = 9, Name = "Pizza", Icon = "local_pizza" },
                new Article { Id = 10, Name = "Super Salad", Icon = "rice_bowl" },
            };

            return StatusCode(StatusCodes.Status200OK, items);
        }
    }
}
