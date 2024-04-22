namespace SuperHeroCharactersApp.DTOs
{
    public record struct CharacterCreateDto(
        string Name, 
        string? submittedBy,
        BackpackCreateDto Backpack,
        List<WeaponCreateDto> Weapons,
        List<FactionCreateDto> Factions);
}
