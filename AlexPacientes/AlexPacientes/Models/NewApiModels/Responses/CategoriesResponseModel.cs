using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels.Responses
{
    public class CategoriesResponseModel : ResponseBaseModel<CategoryListModel>
    {
        
    }
    public class CategoryListModel
    {
        public List<CategoryModel> Items { get; set; }
    }

    public class CategoryModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        [Newtonsoft.Json.JsonProperty("image_media_id")]
        public PhotoMedia ImageMedia { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string Image => ImageMedia?.Url ?? "IconMindPlaceholder.png";
    }
}
