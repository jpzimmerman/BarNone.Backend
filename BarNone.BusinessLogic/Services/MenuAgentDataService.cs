using BarNone.Models;
using BarNone.DataLayer;

public class MenuAgentDataService : IMenuRetrievalService
{
    private readonly IMenuDataRepository _dataRepository;
    public MenuAgentDataService(IMenuDataRepository dataRepository)
    {
        _dataRepository = dataRepository;
    }

    public Task<IEnumerable<IMenuItem>> GetAllMenuItems()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<string>> GetTags()
    {
        throw new NotImplementedException();
    }
}