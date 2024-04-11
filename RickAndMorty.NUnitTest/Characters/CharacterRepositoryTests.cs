using NUnit.Framework;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using RickAndMorty.Net.Api.Models.Dto;
using RickAndMortyApiCrawler.Core.Repositories;
using RickAndMortyApiCrawler.Data.Context;
using RickAndMorty.NUnitTest;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace RickAndMortyApiCrawler.Core.Tests.Repositories
{
    [TestFixture]
    public class CharacterRepositoryTests : BaseTest
    {

        [Test]
        public async Task RemoveAllCharacterAsync_ShouldRemoveAllCharacters()
        {
            // Arrange
            var characterDtoFake = new CharacterDto[]
            {
                new CharacterDto { Name = "Rick Sanchez" },
                new CharacterDto { Name = "Morty Smith" },
                new CharacterDto { Name = "Summer Smith" }
            };

            var charactersDTOs = characterDtoFake.Select(obj => _mapper.Map<CharacterDto>(obj)).ToArray();

            await _characterRepository.AddNewCharactersAsync(charactersDTOs);

            // Act
            await _characterRepository.RemoveAllCharacterAsync();

            // Assert
            var characters = await _dbContext.Characters.ToListAsync();
            Assert.IsEmpty(characters);
        }

        [Test]
        public async Task RemoveAllCharacterAsync_ShouldRollbackTransaction_OnException()
        {
            // Arrange
            var characterDtoFake = new CharacterDto[]
            {
                new CharacterDto { Name = "Rick Sanchez" },
                new CharacterDto { Name = "Morty Smith" },
                new CharacterDto { Name = "Summer Smith" }
            };

            var charactersDTOs = characterDtoFake.Select(obj => _mapper.Map<CharacterDto>(obj)).ToArray();

            await _characterRepository.AddNewCharactersAsync(charactersDTOs);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () =>
            {
                await _characterRepository.RemoveAllCharacterAsync(CancellationToken.None);
            });

            var characters = await _dbContext.Characters.ToListAsync();
            Assert.IsNotEmpty(characters);
        }
    }
}
