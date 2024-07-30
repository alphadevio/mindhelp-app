
namespace AlexPacientes.Models.ApiModels
{
    public class CategoryListModel
    {
        [Newtonsoft.Json.JsonProperty("categlories")]
        public System.Collections.Generic.List<CategoryModel> Categories { get; set; }
    }
}
