using System.Data.SqlClient;
using Data.Base;

namespace Data.ADO
{
    public class MessageRepository : BaseRepository
    {
        public void WarmUp()
        {
            var mapper = new MessageMapper();
            using (var con = new SqlConnection(Connstring))
            {
                con.Open();
                var command = new SqlCommand
                {
                    CommandText = @"select TOP 1 * from messages",
                    Connection = con
                };

                mapper.ReadMultiple(command);
            }
        }
    }
}