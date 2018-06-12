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
                Id = reader.GetInt32(0),
                Message = reader.IsDBNull(1) ? null : reader.GetString(1)
            };
        }
    }
}