using System.Collections.Generic;
using System.Data.SqlClient;

namespace Data.ADO
{
    internal abstract class BaseMapper<TType>
    {
        internal TType[] ReadMultiple(SqlCommand command)
        {
            var ret = new List<TType>();
            using (var reader = command.ExecuteReader())
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

        internal TType ReadSingle(SqlCommand command)
        {
            TType ret = default(TType);
            using (var reader = command.ExecuteReader())
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