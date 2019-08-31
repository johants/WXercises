
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WXercises.Enums;
using WXercises.Models;
using WXercises.Services;

namespace WXercises.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public AnswersController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        // GET api/answers/user
        [Route("user")]
        [HttpGet]
        public ActionResult<User> Get()
        {
            return new User("Johan Sugiarto", "94cd0001-3e70-44d3-a1d1-ad62ba9f5ff2");
        }

        // GET api/answers/sort
        [Route("sort")]
        [HttpGet]
        public async Task<ActionResult<List<Product>>> SortProducts(SortOption sortOption = SortOption.Default)
        {
            return await _productsService.Sort(sortOption);
        }

        // GET api/answers/sort
        [Route("trolleyTotal")]
        [HttpPost]
        public async Task<ActionResult<decimal>> TrolleyTotal([Required] TrolleyTotalRequest request)
        {
            return await _productsService.TrolleyTotal(request);
        }
    }
}
