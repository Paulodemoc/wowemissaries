using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WoWEmissaries.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddFactionAsync(T faction);
        Task<bool> UpdateFactionAsync(T faction);
        Task<bool> DeleteFactionAsync(T faction);
        Task<T> GetFactionAsync(string id);
        Task<IEnumerable<T>> GetFactionsAsync(string xpac, bool forceRefresh = false);
    }
}
