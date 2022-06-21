using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace Data.Base
{
    public class Migrations : BaseRepository
    {
        public async Task CreateTablesAsync()
        {
            try
            {
                string person = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/Scripts/person.sql");

                string database = "POCDb";

                using (var myConn = new SqlConnection(Connstring))
                {
                    myConn.Open();

                    var strCreate = $"USE MASTER IF EXISTS (SELECT name FROM master.sys.databases WHERE name = N'{database}') DROP DATABASE {database} CREATE DATABASE {database}";

                    using (var myCommand = new SqlCommand(strCreate, myConn))
                    {
                        myCommand.ExecuteNonQuery();
                    }

                    using (var myCommand = new SqlCommand(person, myConn))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                }

                await InsertDataAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task InsertDataAsync()
        {
            var strData = await ReadDataAsync();

            var objPerson = JsonConvert.DeserializeObject<List<Person>>(strData);

            using (var connection = new SqlConnection(ConnstringDbPoc))
            {
                connection.Open();

                using (var copy = new SqlBulkCopy(connection))
                {
                    var dt = new DataTable("Person");
                    dt.Columns.Add("_id", typeof(string));
                    dt.Columns.Add("index", typeof(int));
                    dt.Columns.Add("guid", typeof(Guid));
                    dt.Columns.Add("isActive", typeof(bool));
                    dt.Columns.Add("balance", typeof(string));
                    dt.Columns.Add("picture", typeof(string));
                    dt.Columns.Add("age", typeof(int));
                    dt.Columns.Add("eyeColor", typeof(string));
                    dt.Columns.Add("name", typeof(string));
                    dt.Columns.Add("gender", typeof(string));
                    dt.Columns.Add("company", typeof(string));
                    dt.Columns.Add("email", typeof(string));
                    dt.Columns.Add("phone", typeof(string));
                    dt.Columns.Add("address", typeof(string));
                    dt.Columns.Add("about", typeof(string));
                    dt.Columns.Add("registered", typeof(string));
                    dt.Columns.Add("latitude", typeof(double));
                    dt.Columns.Add("longitude", typeof(double));
                    dt.Columns.Add("greeting", typeof(string));
                    dt.Columns.Add("favoriteFruit", typeof(string));

                    foreach (var item in objPerson)
                    {
                        dt.Rows.Add(item._id, item.index, item.guid, item.isActive, item.balance, item.picture,
                            item.age, item.eyeColor, item.name, item.gender, item.company, item.email, item.phone,
                            item.address,
                            item.about, item.registered, item.latitude, item.longitude, item.greeting,
                            item.favoriteFruit);
                    }

                    copy.DestinationTableName = "Person";
                    try
                    {
                        copy.WriteToServer(dt);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);

                        connection.Close();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public async Task<string> ReadDataAsync()
        {
            string data = await File.ReadAllTextAsync(AppDomain.CurrentDomain.BaseDirectory + "/Data/jsondata.json");
            return data;
        }
    }
}