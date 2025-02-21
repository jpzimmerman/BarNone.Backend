namespace BarNone.DataLayer
{
    interface IDataRepository
    {
        Task AddItem(string storedProcedureName, Dictionary<string, object> parameters);
    }
}
