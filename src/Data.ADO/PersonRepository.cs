using Data.Base;
using Model;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data.ADO
{
    public class PersonRepository : BaseRepository
    {
        public async Task WarmUpAsync()
        {
            var mapper = new PersonMapper();
            using (var con = new SqlConnection(ConnstringDbPoc))
            {
                con.Open();
                var command = new SqlCommand
                {
                    CommandText = @"select TOP 1 * from Person",
                    Connection = con
                };

                await mapper.ReadMultipleAsync(command);
            }
        }

        public async Task<Person[]> GetAllPersonAsync()
        {
            var mapper = new PersonMapper();
            using (var con = new SqlConnection(ConnstringDbPoc))
            {
                con.Open();
                var command = new SqlCommand
                {
                    CommandText = @"select * from Person",
                    Connection = con
                };
                return await mapper.ReadMultipleAsync(command);
            }
        }
    }
}