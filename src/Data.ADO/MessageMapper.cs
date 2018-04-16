using System.Data.SqlClient;
using Model;

namespace Data.ADO
{
    internal class MessageMapper : BaseMapper<Messages>
    {
        internal override Messages Map(SqlDataReader reader)
        {
            return new Messages
            {
                Id = reader.GetGuid(0),
                Text = reader.IsDBNull(5) ? null : reader.GetString(5),
                DtRegister = reader.GetDateTime(7),
            };
        }
    }
}