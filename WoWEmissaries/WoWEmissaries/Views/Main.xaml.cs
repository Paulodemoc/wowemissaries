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
    public Main()
    {
      InitializeComponent();
      MasterPage.ListView.ItemSelected += ListView_ItemSelected;

      //only start the service is there is outdated emissaries
      if (MockDataStore.factions.Where(f => f.ExpireOn.Date <= DateTime.Now.Date).Count() > 0)
        MessagingCenter.Send(new StartWowheadParse(), "StartWowheadParse");

      MessagingCenter.Subscribe<CancelledMessage>(this, "CancelledMessage", message =>
      {
        Device.BeginInvokeOnMainThread(() =>
        {
          MessagingCenter.Send(new StopWowheadParse(), "StopWowheadParse");
        });
      });

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
    }
  }
}