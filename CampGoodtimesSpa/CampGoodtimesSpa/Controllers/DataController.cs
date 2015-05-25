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
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace CampGoodtimesSpa.Models.Controllers
{
    public class DataController : BaseController
    {
        private ICampData campData;
        private const string CsvSubdir = "CsvFiles";

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
        public Task<Person> GetPerson(int id)
        {
            return campData.GetPersonAsync(id);
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

        [HttpGet]
        public Task<IEnumerable<Donor>> GetDonors()
        {
            return campData.GetDonorsAsync();
        }

        [HttpGet]
        public Task<Donor> GetDonor(int id)
        {
            return campData.GetDonorAsync(id);
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

        [HttpPut]
        public async Task<HttpResponseMessage> UploadDonorCsv(string id)
        {
            try
            {
                var userName = await GetSignedInUserAsync();
                var rootPath = GetServerRootPath();
                var csvRoot = Path.Combine(rootPath, CsvSubdir);
                if (!Directory.Exists(csvRoot))
                {
                    Directory.CreateDirectory(csvRoot);
                }

                if (string.IsNullOrEmpty(userName))
                {
                    this.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }

                // Check if the request contains multipart/form-data.
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                var provider = new MultipartFormDataStreamProvider(csvRoot);

                // Read the form data and return an async task.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    var fileName = file.Headers.ContentDisposition.FileName;
                    var filePath = Path.Combine(Path.GetDirectoryName(file.LocalFileName), TrimName(fileName));

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    File.Move(file.LocalFileName, filePath);
                    await ProcessDonorCsvAsync(filePath, userName);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception while processing csv file");
                System.Diagnostics.Debug.WriteLine(e.ToString());
                throw;
            }
        }

        private async Task ProcessDonorCsvAsync(string filePath, string userName)
        {
            using (var streamReader = new StreamReader(filePath))
            {
                TextFieldParser parser = new TextFieldParser(streamReader);
                parser.Delimiters = new string[] { "," };
                string[] fields = null;
                do
                {
                    fields = parser.ReadFields();
                    if (fields != null && fields.Length > 0)
                    {
                        await StoreDonorCsvFields(fields, userName);
                    }
                } while (fields != null);
            }
        }

        private async Task StoreDonorCsvFields(string[] fields, string userName)
        {
            if (fields.Length == 3)
            {
                var date = DateTime.MinValue;
                if (DateTime.TryParse(fields[1], out date))
                {
                    Donor donor = new Donor
                    {
                        Giver = fields[0],
                        DonationDate = date,
                        InHonorOf = fields[2]
                    };
                    await campData.UpsertDonorAsync(donor, userName);
                }
            }
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
