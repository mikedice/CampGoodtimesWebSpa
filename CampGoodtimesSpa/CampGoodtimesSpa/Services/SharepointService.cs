using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CampGoodtimesSpa.Models.Sharepoint.Feeds;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using HtmlAgilityPack;

namespace CampGoodtimesSpa.Services
{
    public class SharepointService : ISharepointService
    {
        public Task<IEnumerable<NewsFromTheDirectorElement>> GetDirectorNewsFeedAsync(string newsFeedUrl)
        {
            return GetNewsFeedAsync(newsFeedUrl, ParseNewsFromTheDirectorFeedItem);
        }

        public Task<IEnumerable<CampEventElement>> GetCampeEventsAsync(string newsFeedUrl)
        {
            return GetNewsFeedAsync(newsFeedUrl, ParseCampEventItem);
        }

        private Task<IEnumerable<TElem>> GetNewsFeedAsync<TElem>(string newsFeedUrl, Func<XElement, TElem> elementParser)
        {
            var tcs = new TaskCompletionSource<IEnumerable<TElem>>();

            var wc = new WebClient();
            wc.DownloadStringCompleted += (sender, e) =>
            {
                try
                {
                    var feed = new List<TElem>();

                    XDocument doc = XDocument.Parse(e.Result);
                    var items = doc.Root.XPathSelectElements("channel/item");
                    foreach (var item in items)
                    {
                        feed.Add(elementParser(item));
                    }
                    tcs.SetResult(feed.AsReadOnly());
                }
                catch (Exception exception)
                {
                    tcs.SetException(exception);
                }
            };

            wc.DownloadStringAsync(new Uri(newsFeedUrl));
            return tcs.Task;
        }

        private NewsFromTheDirectorElement ParseNewsFromTheDirectorFeedItem(XElement feedXml)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(feedXml.Element("description").Value);
            var feedItem = new NewsFromTheDirectorElement();

            var result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Content:']");
            if (result.Any())
            {
                feedItem.Description = result.First().NextSibling.InnerText.Trim();
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Visible on Website:']");
            if (result.Any())
            {
                feedItem.IsVisible = result.First().NextSibling.InnerText.Trim().ToLower().Equals("yes") ? true : false;
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Created:']");
            if (result.Any())
            {
                feedItem.PublishedOnGmt = DateTime.Parse(result.First().NextSibling.InnerText.Trim());
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Created By:']");
            if (result.Any())
            {
                feedItem.Author = result.First().NextSibling.InnerText.Trim();
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Title:']");
            if (result.Any())
            {
                feedItem.Title = result.First().NextSibling.InnerText.Trim();
            }
            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='EventDate:']");
            if (result.Any())
            {
                feedItem.EventDate = result.First().NextSibling.InnerText.Trim();
            }
            return feedItem;
        }

        private CampEventElement ParseCampEventItem(XElement feedXml)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(feedXml.Element("description").Value);
            var feedItem = new CampEventElement();

            var result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='DetailsPageName:']");
            if (result.Any())
            {
                feedItem.DetailsPageName = result.First().NextSibling.InnerText.Trim();
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='ShowOnWebSite:']");
            if (result.Any())
            {
                feedItem.ShowOnWebsite = result.First().NextSibling.InnerText.Trim().ToLower().Equals("yes") ? true : false;
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Created:']");
            if (result.Any())
            {
                feedItem.PublishedOnGmt = DateTime.Parse(result.First().NextSibling.InnerText.Trim());
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Created By:']");
            if (result.Any())
            {
                feedItem.Author = result.First().NextSibling.InnerText.Trim();
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Title:']");
            if (result.Any())
            {
                feedItem.Title = result.First().NextSibling.InnerText.Trim();
            }
            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='EventDate:']");
            if (result.Any())
            {
                feedItem.EventDate = result.First().NextSibling.InnerText.Trim();
            }
            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Short Description:']");
            if (result.Any())
            {
                feedItem.ShortDescription = result.First().NextSibling.InnerText.Trim();
            }
            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='WelcomeTo:']");
            if (result.Any())
            {
                feedItem.WelcomeTo = result.First().NextSibling.InnerText.Trim();
            }
            return feedItem;
        }
    }
}