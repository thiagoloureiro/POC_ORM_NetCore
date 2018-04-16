using Dapper;
using Data.Base;
using Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Data.Dapper
{
    public class MessageRepository : BaseRepository
    {
        public void WarmUp()
        {
            using (var con = new SqlConnection(Connstring))
            {
                var sql = @"select TOP 1 * from messages";
                con.Query<Messages>(sql).ToList();
            }
        }

        public Messages[] GetAllMessages()
        {
            List<Messages> ret;
            using (var con = new SqlConnection(Connstring))
            {
                var sql = @"select * from messages";
                ret = con.Query<Messages>(sql).ToList();
            }
            return ret.ToArray();
        }
    }
}