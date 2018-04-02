using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WoWEmissaries.Services
{
    public interface IDataStore<T>
    {
        Task<bool> UpdateFactionAsync();
        Task<IEnumerable<T>> GetFactionsAsync(string xpac, bool forceRefresh = false);
    }
}
