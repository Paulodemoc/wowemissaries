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
  public partial class SettingsPage : ContentPage
  {
    public SettingsPage()
    {
      InitializeComponent();
      if (Application.Current.Properties.ContainsKey("wifionly"))
        WifiOnly.IsToggled = (bool)Application.Current.Properties["wifionly"];
    }

    private async void UpdateSettings(object sender, ToggledEventArgs e)
    {
      Application.Current.Properties["wifionly"] = WifiOnly.IsToggled;
      Application.Current.SavePropertiesAsync();
    }
  }
}