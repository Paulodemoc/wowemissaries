using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoWEmissaries.Views;

namespace WoWEmissaries.Models
{

  public class MainMenuItem
  {
    public MainMenuItem()
    {
      TargetType = typeof(LegionFactions);
    }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Icon { get; set; }

    public Type TargetType { get; set; }
  }
}