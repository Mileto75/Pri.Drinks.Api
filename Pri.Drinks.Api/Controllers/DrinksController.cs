using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pri.Drinks.Core.Interfaces.Services;

namespace Pri.Drinks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        private readonly IDrinkService _drinkService;

        public DrinksController(IDrinkService drinkService)
        {
            _drinkService = drinkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var drinks = await _drinkService.GetAllAsync();
            return Ok(drinks);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok($"drinks with id:{id}");
        }
        [HttpGet("{name}")]
        public IActionResult SearchByName(string name)
        {
            return Ok($"You searched drink:{name}");
        }
        [HttpPost]
        public IActionResult Create()
        {
            return Ok("Created");
        }
        [HttpPut]
        public IActionResult Update()
        {
            return Ok("Updated");
        }
        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok("Deleted");
        }
    }
}
