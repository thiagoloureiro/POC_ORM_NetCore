using Data.Base;
using Model;
using Npgsql;
using PostgreSQLCopyHelper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data.ADO
{
    public class PersonRepository : BaseRepository
    {
        public void WarmUp()
        {
            var mapper = new PersonMapper();
            using (var con = new SqlConnection(ConnstringDbPoc))
            {
                con.Open();
                var command = new SqlCommand
                {
                    CommandText = @"select TOP 1 * from Person",
                    Connection = con
                };

                mapper.ReadMultiple(command);
            }
        }

        public Person[] GetAllPerson()
        {
            var mapper = new PersonMapper();
            using (var con = new SqlConnection(ConnstringDbPoc))
            {
                con.Open();
                var command = new SqlCommand
                {
                    CommandText = @"select * from Person",
                    Connection = con
                };
                return mapper.ReadMultiple(command);
            }
        }

        public async Task InsertPerson(List<Person> lstPerson)
        {
            using (var connection = new SqlConnection(ConnstringDbPoc))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                {
                    bulkCopy.BatchSize = 1000;
                    bulkCopy.DestinationTableName = "dbo.Person";
                    try
                    {
                        await bulkCopy.WriteToServerAsync(lstPerson.AsDataTable());
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        connection.Close();
                    }
                }

                transaction.Commit();
            }
        }

        public void InsertDummy(List<DummyRecord> lstPerson)
        {
            var copyHelper = new PostgreSQLCopyHelper<DummyRecord>("public", "DummyRecord")
                .MapBigInt("Id", x => x.Id)
                .MapInteger("RevisionNumber", x => x.RevisionNumber)
                .MapInteger("index2", x => x.index2)
                .MapBigInt("Owner", x => x.Owner)
                .MapBigInt("Element", x => x.Element)
                .MapTimeStamp("ExternalRecordDateTime", x => x.ExternalRecordDateTime)
                .MapTimeStamp("ExternalRecordDateTime2", x => x.ExternalRecordDateTime2)
                .MapTimeStamp("ExternalRecordDateTime3", x => x.ExternalRecordDateTime3)
                .MapTimeStamp("ExternalRecordDateTime4", x => x.ExternalRecordDateTime4)
                .MapTimeStamp("ExternalRecordDateTime5", x => x.ExternalRecordDateTime5);

            using (var connection = new NpgsqlConnection("Server=127.0.0.1;Port=5433;Database=mi_object_versioning_test;User Id=postgres;Password=mi-dev-password;"))
            {
                connection.Open();

                copyHelper.SaveAll(connection, lstPerson);
            };
        }
    }
}