namespace Data.Base
{
    public class BaseRepository
    {
        public string Connstring = "Data Source=sqlserver; Initial Catalog=master;Encrypt=false; User Id=sa; Password=@Passw0rd;";
        public string ConnstringDbPoc = "Data Source=sqlserver; Initial Catalog=POCDb; User Id=sa;Encrypt=false; Password=@Passw0rd;";
    }
}