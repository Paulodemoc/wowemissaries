using System;
using System.Collections.Generic;
using System.Text;

namespace WoWEmissaries.Models
{
    public class Faction
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Expansion { get; set; }
        public bool Tracked { get; set; }
        public DateTime ExpireOn { get; set; }
        public bool Notificate { get; set; }
        public bool ShowExpiration
        {
            get
            {
                return ExpireOn != DateTime.MinValue;
            }
        }
        public Xamarin.Forms.TextAlignment NamePositioning
        {
            get
            {
                if (ShowExpiration) return Xamarin.Forms.TextAlignment.Start;
                else return Xamarin.Forms.TextAlignment.Center;
            }
        }
    }
}
