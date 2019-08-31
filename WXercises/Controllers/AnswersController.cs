
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
        private readonly IUserService _userService;

        public AnswersController(IProductsService productsService, IUserService userService)
        {
            _productsService = productsService;
            _userService = userService;
        }

        // GET api/answers/user
        [Route("user")]
        [HttpGet]
        public ActionResult<User> Get()
        {
            return _userService.GetUser();
        }

        // GET api/answers/sort
        [Route("sort")]
        [HttpGet]
        public async Task<ActionResult<List<Product>>> SortProducts(SortOption sortOption = SortOption.Default)
        {
            return await _productsService.Sort(sortOption);
        }

        // POST api/answers/trolleyTotal
        [Route("trolleyTotal")]
        [HttpPost]
        public async Task<ActionResult<decimal>> TrolleyTotal([Required] TrolleyTotalRequest request)
        {
            return await _productsService.TrolleyTotal(request);
        }
    }
}
