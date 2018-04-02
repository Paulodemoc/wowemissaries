using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoWEmissaries.Services
{
    public class WowheadParse
    {
        public string WowheadUrl
        {
            get
            {
                return "http://www.wowhead.com/";
            }
        }

        public Dictionary<string, DateTime> GetEmissaries()
        {
            Dictionary<string, DateTime> activeEmissaries = new Dictionary<string, DateTime>();
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument wowheadpage = htmlWeb.Load(WowheadUrl);
            HtmlNodeCollection tableEmissary = wowheadpage.DocumentNode.SelectNodes("//*[@id='main-contents']/div[6]/div/div/div[3]/div/div[1]/div/div[1]/table");
            if (tableEmissary != null)
            {
                //HtmlNodeCollection emissariesRows = tableEmissary[0].SelectNodes("//tr");
                List<HtmlNode> emissariesRows = tableEmissary[0].Descendants("tr").ToList<HtmlNode>();
                emissariesRows.RemoveAt(0);
                foreach (HtmlNode emissary in emissariesRows)
                {
                    string faction;
                    DateTime expireDate;
                    faction = emissary.Descendants("td").ToList<HtmlNode>()[1].Descendants("a").ToList<HtmlNode>()[0].InnerText;
                    string factionScript = emissary.Descendants("script").ToList<HtmlNode>()[0].InnerText;
                    factionScript = factionScript.Substring(factionScript.IndexOf("US-emissary-"), 40).Split(',')[1].Replace("\"", "");
                    expireDate = DateTime.Now;
                    if (factionScript.Split(new string[] { "day" }, StringSplitOptions.None).Length > 1)
                    {
                        int days = Convert.ToInt32(factionScript.Split(new string[] { "day" }, StringSplitOptions.None)[0].Trim());
                        expireDate = expireDate.AddDays(days);
                        factionScript = factionScript.Split(new string[] { "day" }, StringSplitOptions.None)[1].Trim();
                    }

                    if (factionScript.Split(new string[] { "hr" }, StringSplitOptions.None).Length > 1)
                    {
                        int hours = Convert.ToInt32(factionScript.Split(new string[] { "hr" }, StringSplitOptions.None)[0].Trim());
                        expireDate = expireDate.AddHours(hours);
                        factionScript = factionScript.Split(new string[] { "hr" }, StringSplitOptions.None)[1].Trim();
                    }

                    if (factionScript.Split(new string[] { "min" }, StringSplitOptions.None).Length > 1)
                    {
                        int minutes = Convert.ToInt32(factionScript.Split(new string[] { "min" }, StringSplitOptions.None)[0].Trim());
                        expireDate = expireDate.AddMinutes(minutes);
                        factionScript = factionScript.Split(new string[] { "min" }, StringSplitOptions.None)[1].Trim();
                    }

                    activeEmissaries.Add(faction, expireDate);
                }
            }
            return activeEmissaries;
        }
    }
}
