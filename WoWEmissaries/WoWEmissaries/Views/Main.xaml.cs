using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoWEmissaries.Messages;
using WoWEmissaries.Models;
using WoWEmissaries.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WoWEmissaries.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class Main : MasterDetailPage
  {
    ToolbarItem settings;

    public Main()
    {
      InitializeComponent();
      MasterPage.ListView.ItemSelected += ListView_ItemSelected;

      settings = new ToolbarItem
      {
        Icon = "settingsico.png",
        Name = "Settings",
        Command = new Command(this.ShowSettingsPage),
      };

      this.ToolbarItems.Add(settings);

      //only start the service is there is outdated emissaries
      if ((MockDataStore.factions.Where(f => f.ExpireOn != DateTime.MinValue).Count() > 0 //there are active emissaries
          && MockDataStore.factions.Where(f => f.ExpireOn.Date <= DateTime.Now.Date && f.ExpireOn != DateTime.MinValue).Count() > 0) //some are expiring or expired
        || (MockDataStore.factions.Where(f => f.ExpireOn != DateTime.MinValue).Count() == 0)) //there are no active emissaries
        MessagingCenter.Send(new StartWowheadParse(), "StartWowheadParse");

      MessagingCenter.Subscribe<CancelledMessage>(this, "CancelledMessage", message =>
      {
        Device.BeginInvokeOnMainThread(() =>
        {
          MessagingCenter.Send(new StopWowheadParse(), "StopWowheadParse");
        });
      });
    }

    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
      var item = e.SelectedItem as MainMenuItem;
      if (item == null)
        return;

      var page = (Page)Activator.CreateInstance(item.TargetType);
      page.Title = item.Title;

      Detail = new NavigationPage(page);
      IsPresented = false;

      MasterPage.ListView.SelectedItem = null;
      if (this.ToolbarItems.Count == 0)
        this.ToolbarItems.Add(settings);
    }

    private void ShowSettingsPage()
    {
      var page = (Page)Activator.CreateInstance(typeof(SettingsPage));
      Detail = new NavigationPage(page);
      IsPresented = false;
      this.ToolbarItems.Clear();
    }
  }
}