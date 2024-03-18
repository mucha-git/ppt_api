namespace WebApi.Entities.ElementsTypes;

public interface IYoutube : IElementsTypes
{
    public bool? Autoplay { get; set; }
    public string Playlist { get; set; }
}