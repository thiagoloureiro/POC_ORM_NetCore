using Data.Base;
using Model;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using System.Collections.Generic;
using System.Reflection;

namespace Data.NHibernate
{
    public class MessageRepository : BaseRepository
    {
        public Messages[] GetAllMessages()
        {
            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = Connstring;
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2008Dialect>();
            });
            cfg.AddAssembly(Assembly.GetExecutingAssembly());
            var sefact = cfg.BuildSessionFactory();

            IList<Messages> messages;

            using (var session = sefact.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    messages = session.CreateCriteria<Messages>().List<Messages>();
                    tx.Commit();
                }
            }
            object[] array = new object[messages.Count];
            return (Messages[])array;
        }
    }
}