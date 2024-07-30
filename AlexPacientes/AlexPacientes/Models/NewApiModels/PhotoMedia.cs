
namespace AlexPacientes.Models.NewApiModels
{
    public class PhotoMedia
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }
        public string Filename { get; set; }

        public static explicit operator PhotoMedia(AlexPacientes.Models.NewApiModels.Responses.ImageMediaSource source)
        {
            return new PhotoMedia
            {
                ID = (int)source.Id,
                Filename = source.Filename,
                Size = source.Size,
                Type = source.Type,
                Url = source.Url
            };
        }
    }
}
