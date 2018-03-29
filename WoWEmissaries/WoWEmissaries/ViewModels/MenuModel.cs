using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using WoWEmissaries.Models;
using WoWEmissaries.Views;

namespace WoWEmissaries.ViewModels
{
  class MenuModel : INotifyPropertyChanged
  {
    public ObservableCollection<MainMenuItem> MenuItems { get; set; }
    string Title = "WoW Emissaries";

    public MenuModel()
    {
      MenuItems = new ObservableCollection<MainMenuItem>(new[]
      {
                    new MainMenuItem { Id = 0, Title = "Legion", Icon="legion_logo.png", TargetType=typeof(LegionFactions) },
                    new MainMenuItem { Id = 1, Title = "Battle for Azeroth", Icon="bfa_logo.png", TargetType=typeof(BfAFactions) }
                });
    }

    #region INotifyPropertyChanged Implementation
    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
      if (PropertyChanged == null)
        return;

      PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

  }
}
