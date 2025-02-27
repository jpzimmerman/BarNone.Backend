using BarNone.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BarNone.DataLayer
{
    public class MenuMsDataRepository : DataRepository, IMenuDataRepository
    {
        private readonly IDbConnection _connection;

        public MenuMsDataRepository(IDbConnection connection) : base(connection)
        {
            _connection = connection;
        }

        public Task AddGuestOrder(GuestOrder order)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IMenuItem>> GetAllMenuItems()
        {
            var menuItems = new List<IMenuItem>();

            try
            {
                var dataTable = await base.GetItems(Constants.GetMenuItemsSp);
                menuItems = (from item in dataTable.AsEnumerable()
                             select new MenuItem()
                             {
                                 Id = Convert.ToInt32(item["Id"]),
                                 Name = item["Name"].ToString() ?? string.Empty,
                                 Description = item["Description"].ToString() ?? string.Empty,
                                 Price = (float)Convert.ToDouble(item["Price"]),
                                 Category = item["Category"].ToString() ?? string.Empty,
                                 Tags = []
                             }).ToList<IMenuItem>();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAllMenuItems() error: {ex.Message}");
            }

            return menuItems;
        }

        public async Task<IEnumerable<TagCocktailMapItem>> GetTagCocktailMap()
        {
            var tagsCocktailsEntries = new List<TagCocktailMapItem>();
            try
            {
                var dataTable = await base.GetItems(Constants.GetTagCocktailMapSp);
                tagsCocktailsEntries = (from item in dataTable.AsEnumerable()
                             select new TagCocktailMapItem()
                             {
                                 TagId = Convert.ToInt32(item["TagId"]),
                                 DrinkId = Convert.ToInt32(item["TagId"]),
                                 TagName = item["TagName"].ToString() ?? string.Empty
                             }).ToList<TagCocktailMapItem>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetTagCocktailMap() error: {ex.Message}");
            }

            return tagsCocktailsEntries;
        }

        public async Task<IEnumerable<string>> GetTags()
        {
            var tagList = new List<String>();
            try
            {
                var dataTable = await base.GetItems(Constants.GetTagsSp);
                tagList = dataTable.AsEnumerable()
                    .Select(item => item.Field<String>("TagName") ?? string.Empty)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetTags() error: {ex.Message}");
            }

            return tagList;
        }
    }
}
