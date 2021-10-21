using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Excercise3
{
    class Scraper
    {
        public static async Task<ScrapedData> ScrapeEmagUrl(string url)
        {
            // Download the html
            var client = new HttpClient();
            var html = await client.GetStringAsync(url);
            // Use HAP and fizzler to parse and scrape data
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var document = htmlDocument.DocumentNode;
            var titleEl = document.QuerySelectorAll("title").FirstOrDefault();
            var name = titleEl?.InnerText.Replace(" - eMAG.bg", "");
            var priceEl = document.QuerySelectorAll(".product-new-price").FirstOrDefault();
            var priceBase = HttpUtility.HtmlDecode(priceEl.Descendants().FirstOrDefault()?.InnerText);
            if (string.IsNullOrEmpty(priceBase))
            {
                priceBase = "";
            }
            var priceSup = HttpUtility.HtmlDecode(priceEl.QuerySelectorAll("sup").FirstOrDefault()?.InnerText);
            if (string.IsNullOrEmpty(priceSup))
            {
                priceSup = "";
            }
            var priceText = string.Join("", priceBase.Where(c => char.IsDigit(c)).ToArray()) + "." + string.Join("", priceSup.Where(c => char.IsDigit(c)).ToArray());
            var price = float.Parse(priceText);
            var priceCents = (int)Math.Ceiling(price * 100f);
            return new ScrapedData(url, name, priceCents);
        }
    }
}
