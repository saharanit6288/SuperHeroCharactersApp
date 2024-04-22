using Microsoft.EntityFrameworkCore;
using SuperHeroCharactersApp.Data;
using SuperHeroCharactersApp.DTOs;
using SuperHeroCharactersApp.Models;

namespace SuperHeroCharactersApp.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly DataContext _context;

        public CharacterService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Character>> AddCharacter(CharacterCreateDto request)
        {
            var newCharacter = new Character
            {
                Name = request.Name,
                CreatedBy = request.submittedBy,
                ModifiedBy = request.submittedBy,
                CreatedOn = System.DateTime.Now,
                ModifiedOn = System.DateTime.Now,
            };

            var backpack = new Backpack { Description = request.Backpack.Description, Character = newCharacter };
            var weapons = request.Weapons.Select(w => new Weapon { Name = w.Name, Character = newCharacter }).ToList();
            var factions = request.Factions.Select(f => new Faction { Name = f.Name, Characters = new List<Character> { newCharacter } }).ToList();

            newCharacter.Backpack = backpack;
            newCharacter.Weapons = weapons;
            newCharacter.Factions = factions;

            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();

            return await _context.Characters
                .Include(c => c.Backpack)
                .Include(c => c.Weapons)
                .Include(c => c.Factions)
                .ToListAsync();
        }

        public async Task<List<Character>?> DeleteCharacter(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character is null)
                return null;

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            return await _context.Characters
                .Include(c => c.Backpack)
                .Include(c => c.Weapons)
                .Include(c => c.Factions)
                .ToListAsync();
        }

        public async Task<List<Character>> GetAllCharacters()
        {
            var characters = await _context.Characters
                .Include(c => c.Backpack)
                .Include(c => c.Weapons)
                .Include(c => c.Factions)
                .ToListAsync();

            return characters;
        }

        public async Task<Character?> GetCharacterById(int id)
        {
            var character = await _context.Characters
                .Include(c => c.Backpack)
                .Include(c => c.Weapons)
                .Include(c => c.Factions)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (character is null)
                return null;

            return character;
        }

        public async Task<List<Character>?> UpdateCharacter(int id, CharacterCreateDto request)
        {
            var character = await _context.Characters
                .Include(c => c.Backpack)
                .Include(c => c.Weapons)
                .Include(c => c.Factions)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (character is null)
                return null;

            //hero.FirstName = request.FirstName;
            //hero.LastName = request.LastName;
            //hero.Name = request.Name;
            //hero.Place = request.Place;

            //await _context.SaveChangesAsync();

            return await _context.Characters
                .Include(c => c.Backpack)
                .Include(c => c.Weapons)
                .Include(c => c.Factions)
                .ToListAsync();
        }
    }
}
