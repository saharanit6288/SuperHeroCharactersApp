using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroCharactersApp.DTOs;
using SuperHeroCharactersApp.Models;
using SuperHeroCharactersApp.Services.SuperHeroService;
using System.Security.Claims;

namespace SuperHeroCharactersApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroService _superHeroService;

        public SuperHeroController(ISuperHeroService superHeroService)
        {
            _superHeroService = superHeroService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {
            return await _superHeroService.GetAllHeroes();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetSingleHero(int id)
        {
            var result = await _superHeroService.GetSingleHero(id);
            if (result is null)
                return NotFound("Hero not found.");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHeroDto request)
        {
            request.submittedBy = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _superHeroService.AddHero(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(int id, SuperHeroDto request)
        {
            request.submittedBy = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _superHeroService.UpdateHero(id, request);
            if (result is null)
                return NotFound("Hero not found.");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var result = await _superHeroService.DeleteHero(id);
            if (result is null)
                return NotFound("Hero not found.");

            return Ok(result);
        }
    }
}
