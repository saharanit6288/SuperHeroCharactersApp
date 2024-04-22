using SuperHeroCharactersApp.DTOs;
using SuperHeroCharactersApp.Models;

namespace SuperHeroCharactersApp.Services.SuperHeroService
{
    public interface ISuperHeroService
    {
        Task<List<SuperHero>> GetAllHeroes();
        Task<SuperHero?> GetSingleHero(int id);
        Task<List<SuperHero>> AddHero(SuperHeroDto request);
        Task<List<SuperHero>?> UpdateHero(int id, SuperHeroDto request);
        Task<List<SuperHero>?> DeleteHero(int id);
    }
}
