﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using WoWEmissaries.Models;
using WoWEmissaries.Views;

namespace WoWEmissaries.ViewModels
{
  public class FactionsViewModel : BaseViewModel
  {
    public ObservableCollection<Faction> Factions { get; set; }
    public Command LoadFactionsCommand { get; set; }
    public Command TrackFactionsCommand { get; set; }
    public string Xpac { get; set; }

    public FactionsViewModel()
    {
      Title = "Factions";
      Factions = new ObservableCollection<Faction>();
      LoadFactionsCommand = new Command(async () => await ExecuteLoadFactionsCommand(Xpac));
      TrackFactionsCommand = new Command(async () => await TrackFaction());
    }

    async Task ExecuteLoadFactionsCommand(string xpac)
    {
      IsBusy = true;
      try
      {
        Factions.Clear();
        var items = await DataStore.GetFactionsAsync(xpac, true);
        foreach (var item in items)
        {
          Factions.Add(item);
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex);
      }
      finally
      {
        IsBusy = false;
      }
    }

    async Task TrackFaction()
    {
      try
      {
        await DataStore.UpdateFactionAsync();
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex);
      }
      finally
      {
      }
    }
  }
}