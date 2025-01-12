using BarNone.Models;

namespace BarNone.BusinessLogic.Builders
{
    public class MenuItemBuilder
    {
        private readonly IMenuItem _menuItem;

        public MenuItemBuilder(IMenuItem menuItem)
        { 
            _menuItem = menuItem;
        }

        public MenuItemBuilder AddTags(string[] tags)
        {
            _menuItem.Tags = tags;
            return this;
        }

        public IMenuItem Build() 
        {
            return _menuItem;
        }
    }
}
