using BarNone.DataLayer;
using BarNone.Models;
using Newtonsoft.Json;
using Moq;
using Xunit;

namespace BarNone.BusinessLogic.Services.Tests
{
    public class MenuDataServiceTests
    {
        private readonly Mock<IMenuDataRepository> _repositoryMock;
        private readonly IMenuItem singleMenuItem = new MenuItem() { Id = 0, Name = "Item1", Tags = ["Tag1", "Tag2"] };
        private readonly List<TagCocktailMapItem> _testTagCocktailMap;
        private readonly MenuItem testData;

        public MenuDataServiceTests()
        { 
            _testTagCocktailMap = new List<TagCocktailMapItem>()
            {
                new TagCocktailMapItem() { TagId = 0, DrinkId = 0, TagName = "Tag1"},
                new TagCocktailMapItem() { TagId = 1, DrinkId = 0, TagName = "Tag2" }
            };
            var serializedTestData = JsonConvert.SerializeObject(singleMenuItem);
            testData = JsonConvert.DeserializeObject<MenuItem>(serializedTestData) ?? new MenuItem();


            _repositoryMock = new Mock<IMenuDataRepository>();
            _repositoryMock.Setup(m => m.GetAllMenuItems().Result).Returns(value: new List<IMenuItem>() { testData });
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
            Xunit.Assert.Equivalent(new List<IMenuItem>() { testData }, result);
            Xunit.Assert.Equal(singleMenuItem.Tags, result.ToList<IMenuItem>()[0].Tags);
        }

        [Fact()]
        public async Task GetAllMenuItems_MismatchedTags_ExpectingDifferentTagList()
        {
            // Arrange
            var menuDataService = new MenuDataService(dataRepository: _repositoryMock.Object);

            // Assert
            var result = await menuDataService.GetAllMenuItems();
            result.ToList<IMenuItem>()[0].Tags = ["Tag3", "Tag4"];

            // Act
            Xunit.Assert.NotEqual(singleMenuItem.Tags, result.ToList<IMenuItem>()[0].Tags);
        }
    }
}