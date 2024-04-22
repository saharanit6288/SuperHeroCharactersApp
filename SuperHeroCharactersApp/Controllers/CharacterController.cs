using SuperHeroCharactersApp.Data;
using SuperHeroCharactersApp.DTOs;
using SuperHeroCharactersApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroCharactersApp.Services.SuperHeroService;
using SuperHeroCharactersApp.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SuperHeroCharactersApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Character>>> GetAllCharacters()
        {
            return await _characterService.GetAllCharacters();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacterById(int id)
        {
            var result = await _characterService.GetCharacterById(id);
            if (result is null)
                return NotFound("Data not found.");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Character>>> CreateCharacter(CharacterCreateDto request)
        {
            request.submittedBy = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _characterService.AddCharacter(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<SuperHero>>> UpdateCharacter(int id, CharacterCreateDto request)
        {
            request.submittedBy = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _characterService.UpdateCharacter(id, request);
            if (result is null)
                return NotFound("Data not found.");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteCharacter(int id)
        {
            var result = await _characterService.DeleteCharacter(id);
            if (result is null)
                return NotFound("Data not found.");

            return Ok(result);
        }
    }
}
