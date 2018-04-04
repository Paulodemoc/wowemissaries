using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.LocalNotifications;
using Xamarin.Forms;
using WoWEmissaries.Droid.Services;
using Android.Content;
using WoWEmissaries.Messages;

namespace WoWEmissaries.Droid
{
  [Activity(Label = "WoW Emissaries", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
  {
    protected override void OnCreate(Bundle bundle)
    {
      TabLayoutResource = Resource.Layout.Tabbar;
      ToolbarResource = Resource.Layout.Toolbar;
      LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.legion_logo;

      base.OnCreate(bundle);

      global::Xamarin.Forms.Forms.Init(this, bundle);

      MessagingCenter.Subscribe<StartWowheadParse>(this, "StartWowheadParse", message =>
      {
        var intent = new Intent(this, typeof(Wowhead));
        StartService(intent);
      });

      MessagingCenter.Subscribe<StopWowheadParse>(this, "StopWowheadParse", message =>
      {
        var intent = new Intent(this, typeof(Wowhead));
        StopService(intent);
      });

      LoadApplication(new App());
    }
  }
}

