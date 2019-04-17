using System.Collections.Generic;
using Data.Base;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Model;
using System.Linq;

namespace Data.NHibernate
{
    public class PersonRepository : BaseRepository
    {
        public List<Person> GetAllPerson()
        {
            var connStr = ConnstringDbPoc;
            var sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connStr))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Person>())
                .BuildSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                var person = session.CreateCriteria(typeof(Person)).List<Person>();

                return person.ToList();
            }
        }
    }
}