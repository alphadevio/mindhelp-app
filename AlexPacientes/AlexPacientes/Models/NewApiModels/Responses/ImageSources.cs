using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels.Responses
{
    public class ImageMediaSourceResponseModel:ResponseBaseModel<ImagesMediaListModel>
    {
    }

    public class ImagesMediaListModel
    {
        [JsonProperty("items")]
        public List<ImageMediaSource> Items { get; set; }
    }

    public class ImageMediaSource
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }


        [JsonIgnore]
        public Xamarin.Forms.ImageSource Source { get; set; }
    }
}
