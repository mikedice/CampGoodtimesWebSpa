using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CampGoodtimesSpa.Models.Services;
using CampGoodtimesSpa.Models.Camp;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestPeopleCRUD()
        {
            var db = new DatabaseService();
            var userName = "admin";
            Person testPerson = new Person()
            {
                PersonType = PersonType.Employee.ToString(),
                Name = "testname",
                VisibleOnWebsite = true,
                ImageSmall = "testsmallimage",
                ImageLarge = "testlargeimage",
                Title = "testtitle",
                Order = 0
                
            };

            var result = await db.UpsertPersonAsync(testPerson, userName);
            Assert.IsTrue(result);

            var people = await db.GetPeopleAsync(PersonType.Employee);
            Assert.IsTrue(people.Count() > 0);

            var resultPerson = people.FirstOrDefault();
            Assert.IsNotNull(resultPerson);
            Assert.AreEqual(testPerson.PersonType, resultPerson.PersonType);
            Assert.AreEqual(testPerson.Name, resultPerson.Name);
            Assert.AreEqual(testPerson.VisibleOnWebsite, resultPerson.VisibleOnWebsite);
            Assert.AreEqual(testPerson.ImageSmall, resultPerson.ImageSmall);
            Assert.AreEqual(testPerson.ImageLarge, resultPerson.ImageLarge);
            Assert.AreEqual(testPerson.Title, resultPerson.Title);
            Assert.AreEqual(testPerson.Order, resultPerson.Order);

            resultPerson.Name = "upsert a new name";
            result = await db.UpsertPersonAsync(resultPerson, userName);
            people = await db.GetPeopleAsync(PersonType.Employee);
            Assert.AreEqual(resultPerson.Name, people.FirstOrDefault().Name);
            

            foreach(var person in people)
            {
                await db.DeletePersonAsync(person.Id.Value, userName);
            }

            people = await db.GetPeopleAsync(PersonType.Employee);
            Assert.AreEqual(0, people.Count());
        }
        
        [TestMethod]
        public async Task TestArticleCRUD()
        {
            var db = new DatabaseService();
            var userName = "admin";
            Article ar = new Article
            {
                Author = "testauthor",
                ArticleType = ArticleType.NewsFromTheDirector.ToString(),
                Attendance = "testattendance",
                Content = "testcontent",
                DateString = "testdatestring",
                ImageLarge = "testimagelarge",
                ImageSmall = "testimagesmall",
                ShowOnWebsite = true,
                ShortDescription = "testshortdesc",
                Title = "testtitle",
                Order = 0
            };

            var result = await db.UpsertArticleAsync(ar, userName);
            Assert.IsTrue(result);

            var articles = await db.GetArticlesAsync(ArticleType.NewsFromTheDirector);
            Assert.IsTrue(articles.Count() > 0);

            var articleResult = articles.FirstOrDefault();
            Assert.IsNotNull(articleResult);
            Assert.AreEqual(ar.Author, articleResult.Author);
            Assert.AreEqual(ar.ArticleType, articleResult.ArticleType);
            Assert.AreEqual(ar.Content, articleResult.Content);
            Assert.AreEqual(ar.DateString, articleResult.DateString);
            Assert.AreEqual(ar.ImageLarge, articleResult.ImageLarge);
            Assert.AreEqual(ar.ImageSmall, articleResult.ImageSmall);
            Assert.AreEqual(ar.ShowOnWebsite, articleResult.ShowOnWebsite);
            Assert.AreEqual(ar.ShortDescription, articleResult.ShortDescription);
            Assert.AreEqual(ar.Title, articleResult.Title);
            Assert.AreEqual(ar.Order, articleResult.Order);

            articleResult.Title = "make an upsert test";
            result = await db.UpsertArticleAsync(articleResult, userName);

            articles = await db.GetArticlesAsync(ArticleType.NewsFromTheDirector);
            Assert.AreEqual(1, articles.Count());
            var articleResult2 = articles.FirstOrDefault();
            Assert.IsNotNull(articleResult);
            Assert.AreEqual(articleResult.Title, articleResult2.Title);

            foreach(var a in articles)
            {
                result = await db.DeleteArticleAsync(a.Id.Value, userName);
            }

            articles = await db.GetArticlesAsync(ArticleType.NewsFromTheDirector);
            Assert.AreEqual(0, articles.Count());
        }

        [TestMethod]
        public async Task TestDonorsCRUD()
        {
            DateTime testDt = DateTime.Now;
            var userName = "admin";
            var db = new DatabaseService();
            var donor = new Donor
            {
                Giver = "testgiver",
                InHonorOf = "testinhonorof",
                DonationDate = testDt,
            };

            var result = await db.UpsertDonorAsync(donor, userName);
            Assert.IsTrue(result);

            var donors = await db.GetDonorsAsync();
            Assert.IsTrue(donors.Count() > 0);

            var donorResult = donors.FirstOrDefault();
            Assert.IsNotNull(donorResult);
            Assert.AreEqual(donorResult.Giver, donor.Giver);
            Assert.AreEqual(donorResult.InHonorOf, donor.InHonorOf);
            Assert.AreEqual(donorResult.DonationDate.Value.Month, donor.DonationDate.Value.Month);
            Assert.AreEqual(donorResult.DonationDate.Value.Day, donor.DonationDate.Value.Day);
            Assert.AreEqual(donorResult.DonationDate.Value.Year, donor.DonationDate.Value.Year);

            donorResult.Giver = "upsert giver test";
            result = await db.UpsertDonorAsync(donorResult, userName);
            Assert.IsTrue(result);

            donors = await db.GetDonorsAsync();
            Assert.AreEqual(donorResult.Giver, donors.FirstOrDefault().Giver);

            foreach(var d in donors)
            {
                result = await db.DeleteDonorAsync(d.Id.Value, userName);
                Assert.IsTrue(result);
            }

            donors = await db.GetDonorsAsync();
            Assert.AreEqual(0, donors.Count());
        }
    }
}

