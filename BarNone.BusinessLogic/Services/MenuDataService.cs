﻿using BarNone.BusinessLogic.Builders;
using BarNone.DataLayer;
using BarNone.Models;
using System.Data;

namespace BarNone.BusinessLogic.Services
{
    public class MenuDataService
    {
        private readonly IMenuDataRepository _dataRepository;

        public MenuDataService(IMenuDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task<IEnumerable<IMenuItem>> GetAllMenuItems()
        {
            var menuItems = await _dataRepository.GetAllMenuItems();
            var tagsCocktailsMap = await _dataRepository.GetTagCocktailMap();

            foreach (var menuItem in menuItems)
            {
                MenuItemBuilder builder = new(menuItem);
                IEnumerable<string> itemTags =
                    from entry in tagsCocktailsMap
                    where entry.DrinkId == menuItem.Id
                    select entry.TagName;

                builder.AddTags(itemTags.ToArray()).Build();
            }
            return menuItems;
        }

        public async Task AddOrder(GuestOrder order) => await _dataRepository.AddGuestOrder(order);

        public async Task<IEnumerable<string>> GetTags() => await _dataRepository.GetTags();
    }
}
