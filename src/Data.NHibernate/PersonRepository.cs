using Data.Base;
using Model;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using System.Collections.Generic;
using System.Reflection;

namespace Data.NHibernate
{
    public class PersonRepository : BaseRepository
    {
        public Person[] GetAllPerson()
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

            IList<Person> Person;

            using (var session = sefact.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    Person = session.CreateCriteria<Person>().List<Person>();
                    tx.Commit();
                }
            }
            object[] array = new object[Person.Count];
            return (Person[])array;
        }
    }
}