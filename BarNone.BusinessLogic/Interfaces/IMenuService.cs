using BarNone.Models;

public interface IMenuRetrievalService
{
    Task<IEnumerable<IMenuItem>> GetAllMenuItems();
    Task<IEnumerable<string>> GetTags();
}