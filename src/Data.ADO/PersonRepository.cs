using Data.Base;
using Model;
using System.Data.SqlClient;

namespace Data.ADO
{
    public class PersonRepository : BaseRepository
    {
        public void WarmUp()
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

                mapper.ReadMultiple(command);
            }
        }

        public Person[] GetAllPerson()
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
                return mapper.ReadMultiple(command);
            }
        }
    }
}