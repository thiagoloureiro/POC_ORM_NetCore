using Dapper;
using Data.Base;
using Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Dapper
{
    public class PersonRepository : BaseRepository
    {
        public async Task WarmUpAsync()
        {
            using (var con = new SqlConnection(ConnstringDbPoc))
            {
                var sql = @"select TOP 1 * from Person";
                var ret = await con.QueryAsync<Person>(sql);
            }
        }

        public async Task<Person[]> GetAllPersonAsync()
        {
            using (var con = new SqlConnection(ConnstringDbPoc))
            {
                var sql = @"select * from Person";
                var ret = await con.QueryAsync<Person>(sql);
                return ret.ToArray();
            }
        }
    }
}