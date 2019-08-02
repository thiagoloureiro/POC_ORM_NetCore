using System;
using System.Data.SqlClient;
using Model;

namespace Data.ADO
{
    internal class PersonMapper : BaseMapper<Person>
    {
        internal override Person Map(SqlDataReader reader)
        {
            return new Person
            {
                _id = Convert.ToInt64(reader.IsDBNull(0) ? null : reader.GetString(0)),
                name = reader.IsDBNull(1) ? null : reader.GetString(1)
            };
        }
    }
}