using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WoWEmissaries.Models;
using WoWEmissaries.Views;
using WoWEmissaries.ViewModels;
using WoWEmissaries.Services;
using WoWEmissaries.Messages;

namespace WoWEmissaries.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class LegionFactions : ContentPage
  {
    FactionsViewModel viewModel;

    public LegionFactions()
    {
      InitializeComponent();

      BindingContext = viewModel = new FactionsViewModel();
      viewModel.Xpac = "Legion";
      viewModel.Title = "Legion";

      MessagingCenter.Subscribe<ActiveEmissariesMessage>(this, "ActiveEmissariesMessage", message =>
      {
        Device.BeginInvokeOnMainThread(() =>
        {
          MockDataStore dataStore = new MockDataStore();
          dataStore.UpdateEmissaries(message.ActiveEmissaries);
          //if you already have the next 3 emissaries active and none is expiring today, finish the service
          if (MockDataStore.factions.Where(f => f.ExpireOn.Date == DateTime.Now.Date).Count() == 0 &&
              MockDataStore.factions.Where(f => f.ExpireOn.Date != DateTime.MinValue).Count() > 2)
          {
            MessagingCenter.Send(new StopWowheadParse(), "StopWowheadParse");
          }
          viewModel.LoadFactionsCommand.Execute(null);
        });
      });
    }

    async void ItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
      FactionsListView.SelectedItem = null;
    }

    async void TrackFaction(object sender, ToggledEventArgs e)
    {
      viewModel.TrackFactionsCommand.Execute(null);
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();

      if (viewModel.Factions.Count == 0)
        viewModel.LoadFactionsCommand.Execute(null);
    }
  }
}