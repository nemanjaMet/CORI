using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CORI.Models
{
    public class HtmlListModel
    {
        public List<HtmlString> Elements { get; set; }

        public HtmlListModel()
        {
            Elements = new List<HtmlString>(3);
            HtmlString hs1 = new HtmlString("<div>Testiranje bbbb.</div>");
            Elements.Add(hs1);
            Elements.Add(hs1);
            Elements.Add(hs1);
        }

        public HtmlString Render()
        {
            string html = "";
            foreach (HtmlString hs in Elements)
            {
                html += "<div style=\"border: 1px solid white\">";
                html += hs.ToString();
                html += "</div>";
            }
            HtmlString kantice = new HtmlString(html);
            return kantice;
        }
    }
}