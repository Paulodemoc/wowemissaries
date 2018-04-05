﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace WoWEmissaries.Droid
{
  [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true, Label = "WoW Emissaries", Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class Splash : Activity
  {
    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);

      StartActivity(typeof(MainActivity));
    }
  }
}