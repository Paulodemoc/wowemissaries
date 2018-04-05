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
    bool wifi;
    bool notificate;

    public SettingsPage()
    {
      wifi = false;
      notificate = true;

      InitializeComponent();
      if (Application.Current.Properties.ContainsKey("wifionly"))
        wifi = (bool)Application.Current.Properties["wifionly"];
      if (Application.Current.Properties.ContainsKey("notificate"))
        notificate = (bool)Application.Current.Properties["notificate"];

      WifiOnly.IsToggled = wifi;
      Notificate.IsToggled = notificate;
    }

    private async void UpdateSettings(object sender, ToggledEventArgs e)
    {
      Application.Current.Properties["wifionly"] = WifiOnly.IsToggled;
      Application.Current.Properties["notificate"] = Notificate.IsToggled;
      await Application.Current.SavePropertiesAsync();
    }
  }
}