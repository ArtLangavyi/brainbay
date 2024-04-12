
using Microsoft.EntityFrameworkCore;

using RickAndMortyApiCrawler.Core.Models.ImportCharacter;

namespace RickAndMorty.NUnitTest.Characters;
public class TestImport : BaseTest
{

    [Test]
    public async Task Import_ShouldBeSameNumberOfCharacters()
    {
        // Arrange
        ImportFilter importFilter = new()
        {
            Status = RickAndMortyApiCrawler.Core.Models.ImportCharacter.Enums.ImportFilterStatusEnum.Alive,
        };

        // Act
        var charactersList = await _importService.LoadAndAddNewCharacterAsync(importFilter, CancellationToken.None);

        await _importService.SaveCharactersToDb(charactersList, CancellationToken.None);

        var importedCharacters = await _dbContext.Characters.AsNoTracking().ToListAsync();

        // Assert
        Assert.That(importedCharacters, Has.Count.EqualTo(charactersList.Count), "The number of characters returned does not match the expected count.");
    }
}
