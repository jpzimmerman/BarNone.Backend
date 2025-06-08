using BarNone.DataLayer;
using BarNone.Models;
using Moq;
using Xunit;

namespace BarNone.BusinessLogic.Services.Tests
{
    public class MenuDataServiceTests
    {
        private readonly Mock<IMenuDataRepository> _repositoryMock;
        private readonly IMenuItem singleMenuItem = new MenuItem() { Id = 0, Name = "Item1", Tags = ["Tag3", "Tag4"] };
        private readonly List<TagCocktailMapItem> _testTagCocktailMap;

        public MenuDataServiceTests()
        { 
            _testTagCocktailMap = new List<TagCocktailMapItem>()
            {
                new TagCocktailMapItem() { TagId = 0, DrinkId = 0, TagName = "Tag1"},
                new TagCocktailMapItem() { TagId = 1, DrinkId = 0, TagName = "Tag2" }
            };

            _repositoryMock = new Mock<IMenuDataRepository>();
            _repositoryMock.Setup(m => m.GetAllMenuItems().Result).Returns(new List<IMenuItem>() { singleMenuItem });
            _repositoryMock.Setup(m => m.GetTagCocktailMap().Result).Returns(_testTagCocktailMap);

        }

        [Fact()]
        public async Task GetAllMenuItems_HappyPath_ExpectingReturnedListToMatchTestData()
        {
            // Arrange
            var menuDataService = new MenuDataService(dataRepository: _repositoryMock.Object);

            // Assert
            var result = await menuDataService.GetAllMenuItems();
            
            // Act
            Xunit.Assert.Equivalent(new List<IMenuItem>() { singleMenuItem }, result);
            Xunit.Assert.Equal(singleMenuItem.Tags, result.ToList<IMenuItem>()[0].Tags);
        }

        [Fact()]
        public async Task GetAllMenuItems_MismatchedTags_ExpectingDifferentTagList()
        {
            // Arrange
            var menuDataService = new MenuDataService(dataRepository: _repositoryMock.Object);

            // Assert
            var result = await menuDataService.GetAllMenuItems();

            // Act
            Xunit.Assert.Equivalent(new List<IMenuItem>() { singleMenuItem }, result);
            Xunit.Assert.Equal(singleMenuItem.Tags, result.ToList<IMenuItem>()[0].Tags);
        }
    }
}