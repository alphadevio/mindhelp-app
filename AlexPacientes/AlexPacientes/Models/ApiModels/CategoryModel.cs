
namespace AlexPacientes.Models.ApiModels
{
    public class CategoryModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [Newtonsoft.Json.JsonProperty("cat_image")]
        public string Image { get; set; }
    }
}
