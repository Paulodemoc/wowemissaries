using System;
using System.Collections.Generic;
using System.Text;

namespace WoWEmissaries.Models
{
  public class Faction
  {
    public string Name;
    public string Icon;

    public Faction(string name, string icon)
    {
      Name = name;
      Icon = icon;
    }

    public Faction()
    {
      Name = "";
      Icon = "";
    }
  }
}
