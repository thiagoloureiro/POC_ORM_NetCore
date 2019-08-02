using Data.Base;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MI.Persistence.NHibernate.PostgreSQL;
using MI.Persistence.PostgreSQL;
using Model;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.NHibernate
{
    public class PersonRepository : BaseRepository
    {
        public static ISessionFactory SessionFactory;

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

        public async Task WriteToDbAsync(List<Person> personList)
        {
            var connStr = ConnstringDbPoc;

            var cfg = new Configuration();

            cfg.DataBaseIntegration(c =>
            {
                c.ConnectionString = connStr;
                c.AutoCommentSql = false;
                c.BatchSize = 10000;
                c.OrderInserts = true;
                c.LogFormattedSql = false;
                c.LogSqlInConsole = false;
                c.Dialect<MsSql2012Dialect>();
            });

            cfg.AddMappingsFromAssembly(typeof(Person).Assembly);

            var sessionFactory = cfg.BuildSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                session.SetBatchSize(1000);
                using (var transaction = session.BeginTransaction())
                {
                    foreach (var item in personList)
                    {
                        await session.PersistAsync(item).ConfigureAwait(false);
                    }

                    await transaction.CommitAsync().ConfigureAwait(false);
                }
            }
        }

        public async Task WriteToDbAsync2(List<Person> personList)
        {
            var config = new NpgsqlConfiguration(new NpgsqlConnectionString
            {
                Host = "localhost",
                Port = 5433,
                Database = "mi_object_versioning_test",
                Username = "postgres",
                Password = "mi-dev-password"
            })
                .LogSqlInConsole(false)
                // .AddMappingAssembly(Assembly.GetExecutingAssembly())
                .CompileMappings();

            config.AddMappingsFromAssembly(typeof(Person).Assembly);

            // Create the schema
            await config.CreateSchema().ConfigureAwait(false);
            SessionFactory = config
                .UseBatching(100)
                .BuildSessionFactory();

            using (var session = SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    foreach (var item in personList)
                    {
                        await session.PersistAsync(item).ConfigureAwait(false);
                    }

                    await transaction.CommitAsync().ConfigureAwait(false);
                }
            }
        }
    }
}