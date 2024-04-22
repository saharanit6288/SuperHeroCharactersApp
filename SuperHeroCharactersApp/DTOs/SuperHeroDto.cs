namespace SuperHeroCharactersApp.DTOs
{
    public record struct SuperHeroDto(
        string Name,
        string FirstName,
        string LastName,
        string Place, 
        string? submittedBy);
}
