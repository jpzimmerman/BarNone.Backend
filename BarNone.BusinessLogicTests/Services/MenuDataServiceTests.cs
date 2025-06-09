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
        private readonly MenuItem testItemData;
        private readonly string[] testTags = ["Tag1", "Tag2"];
        private readonly string[] testTagData;

        public MenuDataServiceTests()
        { 
            _testTagCocktailMap = new List<TagCocktailMapItem>()
            {
                new TagCocktailMapItem() { TagId = 0, DrinkId = 0, TagName = "Tag1"},
                new TagCocktailMapItem() { TagId = 1, DrinkId = 0, TagName = "Tag2" }
            };
            var serializedItemsTestData = JsonConvert.SerializeObject(singleMenuItem);
            testItemData = JsonConvert.DeserializeObject<MenuItem>(serializedItemsTestData) ?? new MenuItem();

            var serializedTagData = JsonConvert.SerializeObject(testTags);
            testTagData = JsonConvert.DeserializeObject<string[]>(serializedTagData) ?? [];



            _repositoryMock = new Mock<IMenuDataRepository>();
            _repositoryMock.Setup(m => m.GetAllMenuItems().Result).Returns(value: new List<IMenuItem>() { testItemData });
            _repositoryMock.Setup(m => m.GetTags().Result).Returns(value: ["Tag1","Tag2"]);
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
            Xunit.Assert.Equivalent(new List<IMenuItem>() { testItemData }, result);
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

        [Fact()]
        public async Task GetAllMenuItems_TagWithNoMatchingDrinkId_ExpectingTagNotToAppearInItemList()
        {
            // Arrange
            var menuDataService = new MenuDataService(dataRepository: _repositoryMock.Object);
            _testTagCocktailMap.Add(new TagCocktailMapItem() { TagId = 999, DrinkId = 1, TagName = "Tag999" });

            // Assert
            var result = await menuDataService.GetAllMenuItems();

            // Act
            Xunit.Assert.DoesNotContain(result, x => x.Tags.Contains("Tag999"));
        }

        [Fact()]
        public async Task GetTags_HappyPath_ExpectingTagList()
        {
            // Arrange
            var menuDataService = new MenuDataService(dataRepository: _repositoryMock.Object);

            // Assert
            var result = await menuDataService.GetTags();

            // Act
            Xunit.Assert.Equal(testTagData, result);
        }
    }
}