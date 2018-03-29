using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WoWEmissaries.Models;

[assembly: Xamarin.Forms.Dependency(typeof(WoWEmissaries.Services.MockDataStore))]
namespace WoWEmissaries.Services
{
  public class MockDataStore : IDataStore<Faction>
  {
    List<Faction> factions;

    public MockDataStore()
    {
      factions = new List<Faction>
      {
        new Faction { Name = "Argussian Reach", Icon="argussianreach.jpg", Expansion="Legion" },
        new Faction { Name = "Army of the Light", Icon="armyofthelight.jpg", Expansion="Legion" },
        new Faction { Name = "Court of Farondis", Icon="courtoffarondis.jpg", Expansion="Legion" },
        new Faction { Name = "Dreamweavers", Icon="dreamweavers.jpg", Expansion="Legion" },
        new Faction { Name = "Highmountain Tribes", Icon="highmountaintribes.jpg", Expansion="Legion" },
        new Faction { Name = "Kirin Tor", Icon="kirintor.jpg", Expansion="Legion" },
        new Faction { Name = "Nightfallen", Icon="nightfallen.jpg", Expansion="Legion" },
        new Faction { Name = "Valarjar", Icon="valarjar.jpg", Expansion="Legion" },
        new Faction { Name = "Wardens", Icon="wardens.jpg", Expansion="Legion" },
        new Faction { Name = "Teste", Icon="icon.jpg", Expansion="BFA" },
      };
    }

    public async Task<bool> AddFactionAsync(Faction faction)
    {
      factions.Add(faction);

      return await Task.FromResult(true);
    }

    public async Task<bool> UpdateFactionAsync(Faction faction)
    {
      var _faction = factions.Where((Faction arg) => arg.Name == faction.Name).FirstOrDefault();
      factions.Remove(_faction);
      factions.Add(faction);

      return await Task.FromResult(true);
    }

    public async Task<bool> DeleteFactionAsync(Faction faction)
    {
      var _faction = factions.Where((Faction arg) => arg.Name == faction.Name).FirstOrDefault();
      factions.Remove(_faction);

      return await Task.FromResult(true);
    }

    public async Task<Faction> GetFactionAsync(string name)
    {
      return await Task.FromResult(factions.FirstOrDefault(s => s.Name == name));
    }

    public async Task<IEnumerable<Faction>> GetFactionsAsync(string xpac, bool forceRefresh = false)
    {
      return await Task.FromResult(factions.Where(x => x.Expansion.Equals(xpac)).ToList<Faction>());
    }
  }
}