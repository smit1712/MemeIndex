using MemeIndexClientLibrary;
using NUnit.Framework;
using Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeIndexClientTestsCore
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAllMemesTest()
        {
            // arrange
            MemeIndexClient client = new();

            // act
            ICollection<Meme> memes = await client.MemeService.GetAllMemes();
            // assert
            Assert.NotNull(memes);
        }

        [Test]
        public async Task GetMemesWithMaxAmountTest()
        {
            // arrange
            MemeIndexClient client = new();

            // act
            ICollection<Meme> memes = await client.MemeService.GetMemesWithMaxAmount(100);
            // assert
            Assert.NotNull(memes);
        }

        [Test]
        public async Task GetMemesByCategoryTest()
        {
            // arrange
            MemeIndexClient client = new();

            // act
            ICollection<Meme> memes = await client.MemeService.GetMemesByCategory("Animemes");
            // assert
            Assert.NotNull(memes);
        }

        [Test]
        public async Task GetCategoriesTest()
        {
            // arrange
            MemeIndexClient client = new();

            // act
            ICollection<Meme> memes = await client.MemeService.GetCategoriesWithMaxAmount(100);
            // assert
            Assert.NotNull(memes);
        }
    }
}