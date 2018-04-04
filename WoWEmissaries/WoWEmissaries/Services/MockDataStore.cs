using Android.App;
using PCLStorage;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WoWEmissaries.Models;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(WoWEmissaries.Services.MockDataStore))]
namespace WoWEmissaries.Services
{
  public class MockDataStore : IDataStore<Faction>
  {
    static public List<Faction> factions = new List<Faction>{
                new Faction { Name = "Argussian Reach", Icon="argussianreach.jpg", Expansion="Legion", Tracked=false, ExpireOn=DateTime.MinValue },
                new Faction { Name = "Army of the Light", Icon="armyofthelight.jpg", Expansion="Legion", Tracked=false, ExpireOn=DateTime.MinValue },
                new Faction { Name = "Court of Farondis", Icon="courtoffarondis.jpg", Expansion="Legion", Tracked=false, ExpireOn=DateTime.MinValue },
                new Faction { Name = "The Dreamweavers", Icon="dreamweavers.jpg", Expansion="Legion", Tracked=false, ExpireOn=DateTime.MinValue },
                new Faction { Name = "Highmountain Tribes", Icon="highmountaintribes.jpg", Expansion="Legion", Tracked=false, ExpireOn=DateTime.MinValue },
                new Faction { Name = "The Kirin Tor of Dalaran", Icon="kirintor.jpg", Expansion="Legion", Tracked=false, ExpireOn=DateTime.MinValue },
                new Faction { Name = "Nightfallen", Icon="nightfallen.jpg", Expansion="Legion", Tracked=false, ExpireOn=DateTime.MinValue },
                new Faction { Name = "The Valarjar", Icon="valarjar.jpg", Expansion="Legion", Tracked=false, ExpireOn=DateTime.MinValue },
                new Faction { Name = "The Wardens", Icon="wardens.jpg", Expansion="Legion", Tracked=false, ExpireOn=DateTime.MinValue },
                new Faction { Name = "Teste", Icon="icon.jpg", Expansion="BFA", Tracked=false, ExpireOn=DateTime.MinValue },
                new Faction { Name = "Armies of Legionfall", Icon="armiesoflegionfall.jpg", Expansion="Legion", Tracked=false, ExpireOn=DateTime.MinValue }
            };
    static bool Initialized = false;

    public MockDataStore()
    {
    }

    public async Task<bool> UpdateFactionAsync()
    {
      SaveLocalData();
      return await Task.FromResult(true);
    }

    public async Task<IEnumerable<Faction>> GetFactionsAsync(string xpac, bool forceRefresh = false)
    {
      if (!Initialized)
        await ReadLocalData();
      SortFactions();
      return await Task.FromResult(factions.Where(x => x.Expansion.Equals(xpac)).ToList<Faction>());
    }

    public static async Task ReadLocalData()
    {
      try
      {
        IFile factionFile = await FileSystem.Current.LocalStorage.GetFileAsync("factiondata.wow");
        string[] factionData = (await factionFile.ReadAllTextAsync()).Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        factionFile = null;
        foreach (string data in factionData)
        {
          string[] info = data.Split(';');
          if (info.Length == 3)
          {
            Faction faction = factions.First(f => f.Name.Equals(info[0]));
            if (faction != null)
            {
              faction.Tracked = info[1].Equals("1");
              DateTime expiration = DateTime.MinValue;
              DateTime.TryParse(info[2], out expiration);
              if (expiration > DateTime.Now)
                faction.ExpireOn = expiration;
            }
          }
        }
      }
      catch
      {

      }
      Initialized = true;
    }

    public void UpdateEmissaries(Dictionary<string, DateTime> activeOnes)
    {
      try
      {
        foreach (KeyValuePair<string, DateTime> active in activeOnes)
        {
          Faction faction = factions.First(f => f.Name.Equals(active.Key));
          if (faction != null)
          {
            if (faction.ExpireOn == DateTime.MinValue && faction.Tracked) faction.Notificate = true;
            if (active.Value > DateTime.Now)
              faction.ExpireOn = active.Value;
          }
        }
        Notificate();
      }
      catch
      {

      }
      finally
      {
        SaveLocalData();
      }
    }

    private void Notificate()
    {
      List<Faction> toNotificate = factions.Where(f => f.Notificate).ToList();
      foreach (Faction faction in toNotificate)
      {
        CrossLocalNotifications.Current.Show("New Emisssary", faction.Name);
        faction.Notificate = false;
      }
    }

    async public static void SaveLocalData()
    {
      List<Faction> FactionsToSave = factions.Where(f => f.Tracked || f.ExpireOn != DateTime.MinValue).ToList();
      StringBuilder dataToSave = new StringBuilder();

      foreach (Faction f in FactionsToSave)
      {
        dataToSave.AppendLine($"{f.Name};{(f.Tracked ? 1 : 0)};{f.ExpireOn}");
      }

      try
      {
        IFile factionFile = await FileSystem.Current.LocalStorage.CreateFileAsync("factiondata.wow", CreationCollisionOption.ReplaceExisting);

        await factionFile.WriteAllTextAsync(dataToSave.ToString());

        factionFile = null;
      }
      catch
      {

      }
    }

    private void SortFactions()
    {
      factions.Sort(delegate (Faction x, Faction y)
      {
        if (y.ExpireOn != DateTime.MinValue)
        {
          if (x.ExpireOn != DateTime.MinValue)
          {
            if (y.ExpireOn < x.ExpireOn) return 1;
            else return -1;
          }
          else return 1;
        }
        else if (x.ExpireOn != DateTime.MinValue)
        {
          if (y.ExpireOn != DateTime.MinValue)
          {
            if (x.ExpireOn < y.ExpireOn) return -1;
            else return 1;
          }
          else return -1;
        }
        else return x.Name.CompareTo(y.Name);
      });
    }
  }
}