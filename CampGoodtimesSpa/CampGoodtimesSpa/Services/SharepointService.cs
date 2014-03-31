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
            return GetFeedAsync(newsFeedUrl, ParseNewsFromTheDirectorFeedItem);
        }

        public Task<IEnumerable<CampEventElement>> GetCampEventsAsync(string eventsFeedUrl)
        {
            return GetFeedAsync(eventsFeedUrl, ParseCampEventItem);
        }

        public Task<IEnumerable<SponsorsFeedElement>> GetSponsorsAsync(string sponsorsFeedUrl)
        {
            return GetFeedAsync(sponsorsFeedUrl, ParseSponsorsFeed);
        }

        public Task<IEnumerable<Employee>> GetStaffAsync(string staffFeedUrl)
        {
            return GetFeedAsync(staffFeedUrl, ParseEmployeeItem).ContinueWith((completed) =>
            {
                return completed.Result.OrderBy(t => t.Order).AsEnumerable();
            });
        }

        public Task<IEnumerable<Employee>> GetBoardAsync(string boardFeedUrl)
        {
            return GetFeedAsync(boardFeedUrl, ParseEmployeeItem).ContinueWith((completed) =>
            {
                return completed.Result.OrderBy(t => t.Order).AsEnumerable();
            });
        }

        public Task<IEnumerable<Volunteer>> GetVolunteersAsync(string volunteerFeedUrl)
        {
            return GetFeedAsync(volunteerFeedUrl, ParseVolunteerItem).ContinueWith((completed) =>
            {
                return completed.Result.OrderBy(t => t.Category).OrderBy(t => t.Order).AsEnumerable();
            });
        }

        public Task<IEnumerable<CampsElement>> GetCampsAsync(string campsFeedUrl)
        {
            return GetFeedAsync(campsFeedUrl, ParseCampsFeedItem).ContinueWith((completed) => 
            {
                return completed.Result.Where(t => t.ShowOnWebsite).OrderBy(t => t.Order).AsEnumerable();
            });
        }

        private Task<IEnumerable<TElem>> GetFeedAsync<TElem>(string newsFeedUrl, Func<XElement, TElem> elementParser)
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

        private SponsorsFeedElement ParseSponsorsFeed(XElement feedXml)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(feedXml.Element("description").Value);
            var feedItem = new SponsorsFeedElement();

            var result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Name:']");
            if (result.Any())
            {
                feedItem.Name = result.First().NextSibling.InnerText.Trim();
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Visible on Website:']");
            if (result.Any())
            {
                feedItem.VisibleOnWebsite = result.First().NextSibling.InnerText.Trim().ToLower().Equals("yes") ? true : false;
            }

            // optional fields
            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='SponsorImageSmall:']");
            if (result != null && result.Any())
            {
                feedItem.SponsorImageUrlSmall = result.First().ParentNode.SelectSingleNode("a").Attributes["href"].Value;
            }

            return feedItem;
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
            
            // optional fields
            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='EventImageSmall:']");
            if (result != null && result.Any())
            {
                feedItem.EventImageSmall = result.First().ParentNode.SelectSingleNode("a").Attributes["href"].Value;
            }
            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='EventImageLarge:']");
            if (result != null && result.Any())
            {
                feedItem.EventImageLarge = result.First().ParentNode.SelectSingleNode("a").Attributes["href"].Value;
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

        private Employee ParseEmployeeItem(XElement feedXml)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(feedXml.Element("description").Value);
            var feedItem = new Employee();

            var result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Name:']");
            if (result.Any())
            {
                feedItem.Name = WebUtility.HtmlDecode(result.First().NextSibling.InnerText.Trim());
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Title:']");
            if (result != null && result.Any())
            {
                feedItem.Title = result.First().NextSibling.InnerText.Trim();
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Category:']");
            if (result != null && result.Any())
            {
                feedItem.Title = result.First().NextSibling.InnerText.Trim();
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Picture:']");
            if (result != null && result.Any())
            {
                feedItem.PictureUrl = result.First().ParentNode.SelectSingleNode("a").Attributes["href"].Value;
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Order:']");
            if (result != null && result.Any())
            {
                feedItem.Order = 9999; // put it at the back of the list by default;
                int order;
                if (int.TryParse(result.First().NextSibling.InnerText.Trim().ToLower(), out order))
                {
                    feedItem.Order = order;
                }
            }

            return feedItem;
        }

        private Volunteer ParseVolunteerItem(XElement feedXml)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(feedXml.Element("description").Value);
            var feedItem = new Volunteer();

            var result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Name:']");
            if (result.Any())
            {
                feedItem.Name = WebUtility.HtmlDecode(result.First().NextSibling.InnerText.Trim());
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Title:']");
            if (result != null && result.Any())
            {
                feedItem.Title = result.First().NextSibling.InnerText.Trim();
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Category:']");
            if (result != null && result.Any())
            {
                feedItem.Category = result.First().NextSibling.InnerText.Trim();
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Picture:']");
            if (result != null && result.Any())
            {
                feedItem.Picture = result.First().ParentNode.SelectSingleNode("a").Attributes["href"].Value;
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Order:']");
            if (result != null && result.Any())
            {
                feedItem.Order = 9999; // put it at the back of the list by default;
                int order;
                if (int.TryParse(result.First().NextSibling.InnerText.Trim().ToLower(), out order))
                {
                    feedItem.Order = order;
                }
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='ShowOnWebsite:']");
            if (result.Any())
            {
                feedItem.ShowOnWebsite = result.First().NextSibling.InnerText.Trim().ToLower().Equals("yes") ? true : false;
            }

            return feedItem;
        }

        private CampsElement ParseCampsFeedItem(XElement feedXml)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(feedXml.Element("description").Value);
            var feedItem = new CampsElement();

            var result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Title:']");
            if (result.Any())
            {
                feedItem.Title = WebUtility.HtmlDecode(result.First().NextSibling.InnerText.Trim());
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Attendance:']");
            if (result.Any())
            {
                feedItem.Attendance = WebUtility.HtmlDecode(result.First().NextSibling.InnerText.Trim());
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='DateString:']");
            if (result.Any())
            {
                feedItem.DateString = WebUtility.HtmlDecode(result.First().NextSibling.InnerText.Trim());
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='ShortDescription:']");
            if (result.Any())
            {
                feedItem.ShortDescription = WebUtility.HtmlDecode(result.First().NextSibling.InnerText.Trim());
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='BannerPicture:']");
            if (result != null && result.Any())
            {
                feedItem.BannerPicture = result.First().ParentNode.SelectSingleNode("a").Attributes["href"].Value;
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='Order:']");
            if (result != null && result.Any())
            {
                feedItem.Order = 9999; // put it at the back of the list by default;
                int order;
                if (int.TryParse(result.First().NextSibling.InnerText.Trim().ToLower(), out order))
                {
                    feedItem.Order = order;
                }
            }

            result = htmlDoc.DocumentNode.SelectNodes("div/b[text()='ShowOnWebsite:']");
            if (result.Any())
            {
                feedItem.ShowOnWebsite = result.First().NextSibling.InnerText.Trim().ToLower().Equals("yes") ? true : false;
            }

            return feedItem;
        }
    }
}