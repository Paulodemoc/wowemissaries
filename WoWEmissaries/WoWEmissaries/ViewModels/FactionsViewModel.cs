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

        public FactionsViewModel()
        {
            Title = "Factions";
            Factions = new ObservableCollection<Faction>();
            LoadFactionsCommand = new Command(async () => await ExecuteLoadFactionsCommand());
        }

        async Task ExecuteLoadFactionsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Factions.Clear();
                var items = await DataStore.GetFactionsAsync(true);
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
    }
}