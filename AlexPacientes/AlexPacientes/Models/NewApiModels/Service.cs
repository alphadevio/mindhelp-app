using Newtonsoft.Json;

namespace AlexPacientes.Models.NewApiModels
{
    public class Service<T1, T2>
    {
        public int ID { get; set; }
        public bool Deleted { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int Duration { get; set; }
        [JsonProperty("category_id")]
        public T1 Category { get; set; }
        [JsonProperty("image_media_id")]
        public T2 ImageMedia { get; set; }
    }
}
