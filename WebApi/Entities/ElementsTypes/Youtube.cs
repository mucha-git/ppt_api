namespace WebApi.Entities.ElementsTypes;

public class Youtube : IYoutube
{
    public bool? Autoplay { get; set; }
    public string Playlist { get; set; }
}