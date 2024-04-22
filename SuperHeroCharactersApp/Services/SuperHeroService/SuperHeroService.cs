using Microsoft.EntityFrameworkCore;
using SuperHeroCharactersApp.Data;
using SuperHeroCharactersApp.DTOs;
using SuperHeroCharactersApp.Models;

namespace SuperHeroCharactersApp.Services.SuperHeroService
{
    public class SuperHeroService : ISuperHeroService
    {
        private readonly DataContext _context;

        public SuperHeroService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<SuperHero>> AddHero(SuperHeroDto request)
        {
            var hero = new SuperHero
            {
                Name = request.Name,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Place = request.Place,
                CreatedBy = request.submittedBy,
                ModifiedBy = request.submittedBy,
                CreatedOn = System.DateTime.Now,
                ModifiedOn = System.DateTime.Now
            };

            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return await _context.SuperHeroes.ToListAsync();
        }

        public async Task<List<SuperHero>?> DeleteHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero is null)
                return null;

            _context.SuperHeroes.Remove(hero);
            await _context.SaveChangesAsync();

            return await _context.SuperHeroes.ToListAsync();
        }

        public async Task<List<SuperHero>> GetAllHeroes()
        {
            var heroes = await _context.SuperHeroes.ToListAsync();
            return heroes;
        }

        public async Task<SuperHero?> GetSingleHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero is null)
                return null;

            return hero;
        }

        public async Task<List<SuperHero>?> UpdateHero(int id, SuperHeroDto request)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero is null)
                return null;

            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Name = request.Name;
            hero.Place = request.Place;
            hero.ModifiedBy = request.submittedBy;
            hero.ModifiedOn = System.DateTime.Now;

            await _context.SaveChangesAsync();

            return await _context.SuperHeroes.ToListAsync();
        }
    }
}
