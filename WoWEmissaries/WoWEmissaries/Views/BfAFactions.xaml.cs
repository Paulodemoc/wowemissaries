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

namespace WoWEmissaries.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class BfAFactions : ContentPage
  {
    FactionsViewModel viewModel;

    public BfAFactions()
    {
      InitializeComponent();

      BindingContext = viewModel = new FactionsViewModel();
      viewModel.Xpac = "BFA";
      viewModel.Title = "Battle for Azeroth";
    }

    async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
      var item = args.SelectedItem as Faction;
      if (item == null)
        return;

      // Manually deselect item.
      FactionsListView.SelectedItem = null;
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();

      if (viewModel.Factions.Count == 0)
        viewModel.LoadFactionsCommand.Execute(null);
    }
  }
}