using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;
using CampGoodtimesSpa.Models.Camp;
using System.Configuration;

namespace CampGoodtimesSpa.Models.Services
{
    public class DatabaseService : ICampData, IUserData
    {
        private string connectionString;

        public DatabaseService()
        {
            this.connectionString = ConfigurationManager.AppSettings["CampSiteDatabase"];
        }

        public async Task<IEnumerable<Person>> GetPeopleAsync(PersonType type)
        {
            List<Person> result = new List<Person>();
            using (var connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("[gt].[GetPeople]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    int personType = (int)type;
                    var param = new SqlParameter("@personType", personType);
                    cmd.Parameters.Add(param);

                    var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var el = new Person();
                        el.Id = ReadDataInt32(reader, 0);
                        el.PersonType = ReadDataString(reader, 1);
                        el.CreatedOn = ReadDataDateTime(reader, 2);
                        el.CreatedBy = ReadDataString(reader, 3);
                        el.ModifiedOn = ReadDataDateTime(reader, 4);
                        el.ModifiedBy = ReadDataString(reader, 5);
                        el.DeletedOn = ReadDataDateTime(reader, 6);
                        el.DeletedBy = ReadDataString(reader, 7);
                        el.Name = ReadDataString(reader, 8);
                        el.VisibleOnWebsite = ReadDataBool(reader, 9);
                        el.ImageSmall = ReadDataString(reader, 10);
                        el.ImageLarge = ReadDataString(reader, 11);
                        el.Title = ReadDataString(reader, 12);
                        el.Order = ReadDataInt32(reader, 13);
                        result.Add(el);
                    }
                }
            }
            return result;
        }

        public async Task<Person> GetPersonAsync(int id)
        {
            Person result = null;
            using (var connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("[gt].[GetPerson]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var param = new SqlParameter("@id", id);
                    cmd.Parameters.Add(param);

                    var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var el = new Person();
                        el.Id = ReadDataInt32(reader, 0);
                        el.PersonType = ReadDataString(reader, 1);
                        el.CreatedOn = ReadDataDateTime(reader, 2);
                        el.CreatedBy = ReadDataString(reader, 3);
                        el.ModifiedOn = ReadDataDateTime(reader, 4);
                        el.ModifiedBy = ReadDataString(reader, 5);
                        el.DeletedOn = ReadDataDateTime(reader, 6);
                        el.DeletedBy = ReadDataString(reader, 7);
                        el.Name = ReadDataString(reader, 8);
                        el.VisibleOnWebsite = ReadDataBool(reader, 9);
                        el.ImageSmall = ReadDataString(reader, 10);
                        el.ImageLarge = ReadDataString(reader, 11);
                        el.Title = ReadDataString(reader, 12);
                        el.Order = ReadDataInt32(reader, 13);
                        result = el;
                        break;
                    }
                }
            }
            return result;
        }

        public async Task<bool> DeletePersonAsync(int id, string byUserName)
        {
            bool result = false;
            using (var connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("[gt].[DeletePerson]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    var param = new SqlParameter("@userName", byUserName);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@personId", id);
                    cmd.Parameters.Add(param);

                    int rows = await cmd.ExecuteNonQueryAsync();
                    result = rows != 0;
                }
            }

            return result;
        }

        public async Task<bool> UpsertPersonAsync(Person person, string byUserName)
        {
            bool result = false;
            using (var connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("[gt].[UpsertPerson]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var param = new SqlParameter("@userName", byUserName);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@personId", person.Id);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@personType", ConvertPersonType(person.PersonType));
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@name", person.Name);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@visibleOnWebsite", person.VisibleOnWebsite);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@imageSmall", person.ImageSmall);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@imageLarge", person.ImageLarge);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@title", person.Title);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@order", person.Order);
                    cmd.Parameters.Add(param);
                    
                    int rows = await cmd.ExecuteNonQueryAsync();
                    result = rows != 0;
                }
            }

            return result;
        }

        public async Task<Article> GetArticleAsync(int id)
        {
            Article result = null;
            using (var connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("[gt].[GetArticle]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var param = new SqlParameter("@id", id);
                    cmd.Parameters.Add(param);

                    var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var el = new Article();
                        el.Id = ReadDataInt32(reader, 0);
                        el.ArticleType = ReadDataString(reader, 1);
                        el.Author = ReadDataString(reader, 2);
                        el.CreatedOn = ReadDataDateTime(reader, 3);
                        el.CreatedBy = ReadDataString(reader, 4);
                        el.ModifiedOn = ReadDataDateTime(reader, 5);
                        el.ModifiedBy = ReadDataString(reader, 6);
                        el.DeletedOn = ReadDataDateTime(reader, 7);
                        el.DeletedBy = ReadDataString(reader, 8);
                        el.Title = ReadDataString(reader, 9);
                        el.ShortDescription = ReadDataString(reader, 10);
                        el.Content = ReadDataString(reader, 11);
                        el.Attendance = ReadDataString(reader, 12);
                        el.DateString = ReadDataString(reader, 13);
                        el.ImageSmall = ReadDataString(reader, 14);
                        el.ImageLarge = ReadDataString(reader, 15);
                        el.ShowOnWebsite = ReadDataBool(reader, 16);
                        el.Order = ReadDataInt32(reader, 17);
                        result = el;
                        break;
                    }
                }
            }
            return result;       
        }

        public async Task<IEnumerable<Article>> GetArticlesAsync(ArticleType type)
        {
            List<Article> result = new List<Article>();
            using (var connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("[gt].[GetArticles]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    int articleType = (int)type;
                    var param = new SqlParameter("@articleType", articleType);
                    cmd.Parameters.Add(param);

                    var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var el = new Article();
                        el.Id = ReadDataInt32(reader, 0);
                        el.ArticleType = ReadDataString(reader, 1);
                        el.Author = ReadDataString(reader, 2);
                        el.CreatedOn = ReadDataDateTime(reader, 3);
                        el.CreatedBy = ReadDataString(reader, 4); 
                        el.ModifiedOn = ReadDataDateTime(reader, 5);
                        el.ModifiedBy = ReadDataString(reader, 6);
                        el.DeletedOn = ReadDataDateTime(reader, 7);
                        el.DeletedBy = ReadDataString(reader, 8);
                        el.Title = ReadDataString(reader, 9);
                        el.ShortDescription = ReadDataString(reader, 10);
                        el.Content = ReadDataString(reader, 11);
                        el.Attendance = ReadDataString(reader, 12);
                        el.DateString = ReadDataString(reader, 13);
                        el.ImageSmall = ReadDataString(reader, 14);
                        el.ImageLarge = ReadDataString(reader, 15); 
                        el.ShowOnWebsite = ReadDataBool(reader, 16);
                        el.Order = ReadDataInt32(reader, 17);
                        result.Add(el);
                    }
                }
            }
            return result;
        }

        public async Task<bool> DeleteArticleAsync(int id, string byUserName)
        {
            bool result = false;
            using (var connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("[gt].[DeleteArticle]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var param = new SqlParameter("@userName", byUserName);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@articleId", id);
                    cmd.Parameters.Add(param);

                    int rows = await cmd.ExecuteNonQueryAsync();
                    result = rows != 0;
                }
            }

            return result;
        }

        public async Task<bool> UpsertArticleAsync(Article article, string byUserName)
        {
             bool result = false;
             using (var connection = new SqlConnection(this.connectionString))
             {
                 await connection.OpenAsync();
                 using (var cmd = new SqlCommand("[gt].[UpsertArticle]", connection))
                 {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var param = new SqlParameter("@userName", byUserName);
                    cmd.Parameters.Add(param);
                 
                    param = new SqlParameter("@articleId", article.Id);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@author", article.Author);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@articleType", ConvertArticleType(article.ArticleType));
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@title", article.Title);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@shortDescription", article.ShortDescription);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@content", article.Content);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@attendance", article.Attendance);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@dateString", article.DateString);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@imageSmall ", article.ImageSmall);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@imageLarge", article.ImageLarge);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@showOnWebsite", article.ShowOnWebsite);
                    cmd.Parameters.Add(param);
                    
                    param = new SqlParameter("@order", article.Order);
                    cmd.Parameters.Add(param);

                    int rows = await cmd.ExecuteNonQueryAsync();
                    result = rows != 0;
                 }
             }
             return result;
        }
        
        public async Task<IEnumerable<Donor>> GetDonorsAsync()
        {
            List<Donor> result = new List<Donor>();
            using (var connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("[gt].[GetDonors]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var el = new Donor();
                        el.Id = ReadDataInt32(reader, 0);
                        el.CreatedOn = ReadDataDateTime(reader, 1);
                        el.CreatedBy = ReadDataString(reader, 2);
                        el.ModifiedOn = ReadDataDateTime(reader, 3);
                        el.ModifiedBy = ReadDataString(reader, 4);
                        el.DeletedOn = ReadDataDateTime(reader, 5);
                        el.DeletedBy = ReadDataString(reader, 6);
                        el.DonationDate = ReadDataDateTime(reader, 7);
                        el.Giver = ReadDataString(reader, 8);
                        el.InHonorOf = ReadDataString(reader, 9);
                        result.Add(el);
                    }
                }
            }
            return result;
        }

        public async Task<bool> DeleteDonorAsync(int id, string byUserName)
        {
            bool result = false;
            using (var connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("[gt].[DeleteDonor]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var param = new SqlParameter("@userName", byUserName);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@donorId", id);
                    cmd.Parameters.Add(param);

                    int rows = await cmd.ExecuteNonQueryAsync();
                    result = rows != 0;
                }
            }

            return result;
        }

        public async Task<bool> UpsertDonorAsync(Donor donor, string byUserName)
        {
            bool result = false;
            using (var connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("[gt].[UpsertDonor]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var param = new SqlParameter("@userName", byUserName);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@id", donor.Id);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@donationDate", donor.DonationDate);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@giver", donor.Giver);
                    cmd.Parameters.Add(param);

                    param = new SqlParameter("@inHonorOf", donor.InHonorOf);
                    cmd.Parameters.Add(param);

                    int rows = await cmd.ExecuteNonQueryAsync();
                    result = rows != 0;
                }
            }
            return result;

        }

        private const string UsersCommand = "SELECT Id,FirstName,LastName,UserName,Password FROM [gt].[Users]";
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            List<User> result = new List<User>();
            using (var connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand(UsersCommand, connection))
                {
                    var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var el = new User();
                        el.Id = ReadDataInt32(reader, 0);
                        el.FirstName = ReadDataString(reader, 1);
                        el.LastName = ReadDataString(reader, 2);
                        el.UserName = ReadDataString(reader, 3);
                        el.Password = ReadDataString(reader, 4);
                        result.Add(el);
                    }
                }
            }
            return result;
        }
        
        private const string UserByUserNameAndPasswordCommand = "SELECT Id,FirstName,LastName,UserName,Password FROM [gt].[Users] WHERE UserName='{0}' AND Password='{1}'";
        public async Task<User> GetUserByUserNameAndPasswordAsync(string userName, string password)
        {
            User result = null;
            using (var connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                var command = string.Format(UserByUserNameAndPasswordCommand, userName, password);
                using (var cmd = new SqlCommand(command, connection))
                {
                    var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var el = new User();
                        el.Id = ReadDataInt32(reader, 0);
                        el.FirstName = ReadDataString(reader, 1);
                        el.LastName = ReadDataString(reader, 2);
                        el.UserName = ReadDataString(reader, 3);
                        el.Password = ReadDataString(reader, 4);
                        result = el;
                        break;
                    }
                }
            }
            return result;
        }

        private const string UserByUserNameCommand = "SELECT Id,FirstName,LastName,UserName,Password FROM [gt].[Users] WHERE UserName='{0}'";
        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            User result = null;
            using (var connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                var command = string.Format(UserByUserNameCommand, userName);
                using (var cmd = new SqlCommand(command, connection))
                {
                    var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var el = new User();
                        el.Id = ReadDataInt32(reader, 0);
                        el.FirstName = ReadDataString(reader, 1);
                        el.LastName = ReadDataString(reader, 2);
                        el.UserName = ReadDataString(reader, 3);
                        el.Password = ReadDataString(reader, 4);
                        result = el;
                        break;
                    }
                }
            }
            return result;
        }

        // For returning list of users without the password field
        public async Task<IEnumerable<User>> GetPublicUsersAsync()
        {
            var users = await this.GetUsersAsync();
            foreach (var user in users)
            {
                user.Password = null;
            }
            return users;
        }

        private int? ReadDataInt32(SqlDataReader reader, int ordinal)
        {
            var val = reader.GetSqlInt32(ordinal);
            if (val.IsNull)
            {
                return null;
            }
            return val.Value;
        }

        private bool? ReadDataBool(SqlDataReader reader, int ordinal)
        {
            var val = reader.GetSqlBoolean(ordinal);
            if (val.IsNull)
            {
                return null;
            }
            return val.Value;
        }

        private DateTime? ReadDataDateTime(SqlDataReader reader, int ordinal)
        {
            var val = reader.GetSqlDateTime(ordinal);
            if (val.IsNull)
            {
                return null;
            }
            return val.Value;
        }

        private string ReadDataString(SqlDataReader reader, int ordinal)
        {
            var val = reader.GetSqlString(ordinal);
            if (val.IsNull)
            {
                return null;
            }
            return val.Value;
        }

        private static int ConvertPersonType(string stringPerson)
        {
            int result = 0;
            PersonType personType = PersonType.Unknown;
            if (System.Enum.TryParse<PersonType>(stringPerson, out personType))
            {
                result = (int)personType;
            }
            return result;
        }

        private static int ConvertArticleType(string stringArticle)
        {
            int result = 0;
            ArticleType articleType = ArticleType.Unknown;
            if (System.Enum.TryParse<ArticleType>(stringArticle, out articleType))
            {
                result = (int)articleType;
            }
            return result;
        }

    }
}