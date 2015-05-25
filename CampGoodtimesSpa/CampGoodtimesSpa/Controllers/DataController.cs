using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CampGoodtimesSpa.Models.Services;
using CampGoodtimesSpa.Models.Camp;

namespace CampGoodtimesSpa.Models.Controllers
{
    public class DataController : BaseController
    {
        private ICampData campData;

        public DataController(ICampData campData, ISignInService signInService)
            : base(signInService)
        {
            this.campData = campData;
        }

        [HttpGet]
        public Task<IEnumerable<Person>> Employee()
        {
            return campData.GetPeopleAsync(PersonType.Employee);
        }

        [HttpGet]
        public Task<IEnumerable<Person>> Sponsors()
        {
            return campData.GetPeopleAsync(PersonType.Sponsor);
        }

        [HttpGet]
        public Task<IEnumerable<Person>> Staff()
        {
            return campData.GetPeopleAsync(PersonType.Staff);
        }

        [HttpGet]
        public Task<IEnumerable<Person>> Board()
        {
            return campData.GetPeopleAsync(PersonType.Board);
        }

        [HttpGet]
        public Task<IEnumerable<Person>> Volunteers()
        {
            return campData.GetPeopleAsync(PersonType.Volunteer);
        }

        [HttpGet]
        public Task<IEnumerable<Article>> NewsItems()
        {
            return campData.GetArticlesAsync(ArticleType.NewsFromTheDirector);
        }

        [HttpGet]
        public Task<IEnumerable<Article>> Camps()
        {
            return campData.GetArticlesAsync(ArticleType.Camps);
        }

        [HttpGet]
        public Task<IEnumerable<Article>> Events()
        {
            return campData.GetArticlesAsync(ArticleType.Events);
        }

        [HttpGet]
        public Task<Article> GetArticle(int id)
        {
            return campData.GetArticleAsync(id);
        }

        [HttpPost]
        public async Task<bool> PostArticle(Article article)
        {
            var userName = await GetValidUserAsync();
            return await campData.UpsertArticleAsync(article, userName);
        }

        [HttpPut]
        public async Task<bool> PutArticle(Article article)
        {
            var userName = await GetValidUserAsync();
            return await campData.UpsertArticleAsync(article, userName);
        }

        [HttpDelete]
        public async Task<bool> DeleteArticle(int id)
        {
            var userName = await GetValidUserAsync();
            return await campData.DeleteArticleAsync(id, userName);
        }

        [HttpPost]
        public async Task<bool> PostPerson(Person person)
        {
            var userName = await GetValidUserAsync();
            return await campData.UpsertPersonAsync(person, userName);
        }

        [HttpPut]
        public async Task<bool> PutPerson(Person person)
        {
            var userName = await GetValidUserAsync();
            return await campData.UpsertPersonAsync(person, userName);
        }

        [HttpDelete]
        public async Task<bool> DeletePerson(int id)
        {
            var userName = await GetValidUserAsync();
            return await campData.DeletePersonAsync(id, userName);
        }

        [HttpPost]
        public async Task<bool> PostDonor(Donor donor)
        {
            var userName = await GetValidUserAsync();
            return await campData.UpsertDonorAsync(donor, userName);
        }

        [HttpPut]
        public async Task<bool> PutDonor(Donor donor)
        {
            var userName = await GetValidUserAsync();
            return await campData.UpsertDonorAsync(donor, userName);
        }

        [HttpDelete]
        public async Task<bool> DeleteDonor(int id)
        {
            var userName = await GetValidUserAsync();
            return await campData.DeleteDonorAsync(id, userName);
        }

        private async Task<string> GetValidUserAsync()
        {
            var userName = await GetSignedInUserAsync();
            if (string.IsNullOrEmpty(userName))
            {
                throw new UnauthorizedAccessException();
            }
            return userName;
        }

    }
}
