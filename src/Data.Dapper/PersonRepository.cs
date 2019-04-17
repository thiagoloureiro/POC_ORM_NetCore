using Dapper;
using Data.Base;
using Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Data.Dapper
{
    public class PersonRepository : BaseRepository
    {
        public void WarmUp()
        {
            using (var con = new SqlConnection(ConnstringDbPoc))
            {
                var sql = @"select TOP 1 * from Person";
                con.Query<Person>(sql).ToList();
            }
        }

        public Person[] GetAllPerson()
        {
            List<Person> ret;
            using (var con = new SqlConnection(ConnstringDbPoc))
            {
                var sql = @"select * from Person";
                ret = con.Query<Person>(sql).ToList();
            }
            return ret.ToArray();
        }
    }
}