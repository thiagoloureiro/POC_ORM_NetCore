using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data.ADO
{
    internal abstract class BaseMapper<TType>
    {
        internal async Task<TType[]> ReadMultipleAsync(SqlCommand command)
        {
            var ret = new List<TType>();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    ret.Add(
                        Map(reader)
                    );
                }
            }
            return ret.ToArray();
        }

        internal async Task<TType> ReadSingleAsync(SqlCommand command)
        {
            TType ret = default(TType);
            using (var reader = await command.ExecuteReaderAsync())
            {
                if (reader.Read())
                {
                    ret = Map(reader);
                }
            }
            return ret;
        }

        internal abstract TType Map(SqlDataReader reader);
    }
}