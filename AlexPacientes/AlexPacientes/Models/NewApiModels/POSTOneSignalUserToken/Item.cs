
namespace AlexPacientes.Models.NewApiModels.POSTOneSignalUserToken
{
    public class Item
    {
        public int id { get; set; }
        public long created_at { get; set; }
        public long updated_at { get; set; }

        [Newtonsoft.Json.JsonProperty("userId")]
        public int userId { get; set; }
        public string token { get; set; }
    }
}
