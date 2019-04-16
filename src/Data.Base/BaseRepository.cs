namespace Data.Base
{
    public class BaseRepository
    {
        public string Connstring = "Data Source=sqlserver; Initial Catalog=master; User Id=sa;Password=@Passw0rd;";
        public string ConnstringDbPoc = "Data Source=sqlserver; Initial Catalog=POCDb; User Id=sa;Password=@Passw0rd;";
    }
}