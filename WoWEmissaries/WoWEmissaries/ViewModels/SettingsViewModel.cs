using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using WoWEmissaries.Models;
using WoWEmissaries.Views;

namespace WoWEmissaries.ViewModels
{
  public class SettingsViewModel : BaseViewModel
  {
    public bool wifiOnly = false;

    public SettingsViewModel()
    {
      Title = "Settings";
      if (Application.Current.Properties.ContainsKey("wifionly"))
        bool.TryParse(Application.Current.Properties["wifionly"].ToString(), out wifiOnly);
    }
  }
}