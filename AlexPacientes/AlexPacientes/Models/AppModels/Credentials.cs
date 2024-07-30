using SQLite;

namespace AlexPacientes.Models.AppModels
{
    [Table("Credentials")]
    public class Credentials
    {
        [PrimaryKey, NotNull, AutoIncrement]
        public int IDPK { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserID { get; set; }
    }
}
