using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampGoodtimesSpa.Models.Camp;

namespace CampGoodtimesSpa.Models.Services
{
    public interface ICampData
    {
        Task<IEnumerable<Person>> GetPeopleAsync(PersonType type);
        Task<bool> DeletePersonAsync(int id, string byUserName);
        Task<bool> UpsertPersonAsync(Person person, string byUserName);

        Task<Article> GetArticleAsync(int id);
        Task<IEnumerable<Article>> GetArticlesAsync(ArticleType type);
        Task<bool> DeleteArticleAsync(int id, string byUserName);
        Task<bool> UpsertArticleAsync(Article article, string byUserName);

        Task<IEnumerable<Donor>> GetDonorsAsync();
        Task<bool> DeleteDonorAsync(int id, string byUserName);
        Task<bool> UpsertDonorAsync(Donor donor, string byUserName);
    }
}
