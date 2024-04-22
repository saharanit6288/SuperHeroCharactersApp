using SuperHeroCharactersApp.DTOs;
using SuperHeroCharactersApp.Models;

namespace SuperHeroCharactersApp.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<List<Character>> GetAllCharacters();
        Task<Character?> GetCharacterById(int id);
        Task<List<Character>> AddCharacter(CharacterCreateDto request);
        Task<List<Character>?> UpdateCharacter(int id, CharacterCreateDto request);
        Task<List<Character>?> DeleteCharacter(int id);
    }
}
