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
    public partial class LegionFactions : ContentPage
    {
        FactionsViewModel viewModel;

        public LegionFactions()
        {
            InitializeComponent();

            BindingContext = viewModel = new FactionsViewModel();
            viewModel.Xpac = "Legion";
            viewModel.Title = "Legion";
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