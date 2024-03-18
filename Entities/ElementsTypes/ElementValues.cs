using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Helpers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApi.Entities.ElementsTypes;

public class ElementValues : IDivider, IGraphicAndText, IMap, INavigation, IYoutube
{
    public string Color { get; set; }
    public int? Margin { get; set; }
    public int? Height { get; set; }
    public string Text { get; set; }
    public string ImgSrc { get; set; }
    public int? MapHeight { get; set; }
    public int? DestinationViewId { get; set; }
    public bool? Autoplay { get; set; }
    public string Playlist { get; set; }
}