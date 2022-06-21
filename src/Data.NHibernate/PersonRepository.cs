using System.Collections.Generic;
using Data.Base;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Model;
using System.Linq;
using System.Threading.Tasks;

namespace Data.NHibernate
{
    public class PersonRepository : BaseRepository
    {
        public async Task<List<Person>> GetAllPersonAsync()
        {
            var connStr = ConnstringDbPoc;
            var sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connStr))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Person>())
                .BuildSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                var person = await session.CreateCriteria(typeof(Person)).ListAsync<Person>();

                return person.ToList();
            }
        }
    }
}