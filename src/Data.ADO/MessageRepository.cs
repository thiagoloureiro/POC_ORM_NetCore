using Data.Base;
using Model;
using System.Data.SqlClient;

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

        public Messages[] GetAllMessages()
        {
            var mapper = new MessageMapper();
            using (var con = new SqlConnection(Connstring))
            {
                con.Open();
                var command = new SqlCommand
                {
                    CommandText = @"select * from messages",
                    Connection = con
                };

                return mapper.ReadMultiple(command);
            }
        }
    }
}