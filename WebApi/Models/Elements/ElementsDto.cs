using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi.Entities.ElementsTypes;
using WebApi.Helpers;

namespace WebApi.Models.Elements;

public class ElementsDto : IDivider, IGraphicAndText, IMap, INavigation, IYoutube
{
    public int Id { get; set; }
    public ElementType Type { get; set; }
    #region Divider
    public string Color { get; set; }
    
    public int? Margin { get; set; }
    public int? Height { get; set; }
    #endregion

    #region Graphic And Text
    public string Text { get; set; }
    public string ImgSrc { get; set; }
    #endregion

    #region Youtube
    public bool? Autoplay { get; set; }
    public string Playlist { get; set; }
    #endregion

    #region Map
    public int? MapHeight { get; set; }

    public int? MapId { get; set; }
    #endregion

    #region Navigation
    public int? DestinationViewId { get; set; }

    #endregion

    public int? Order { get; set; }
    public int ViewId { get; set; }

    public int YearId { get; set; }

}