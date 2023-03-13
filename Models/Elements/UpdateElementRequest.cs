using System.ComponentModel.DataAnnotations;
using WebApi.Helpers;

namespace WebApi.Models.Elements;

public class UpdateElementRequest {
    [Required]
    public int Id { get; set; }
    [Required]
    public ElementType Type { get; set; }
    #region Divider
    public string Color { get; set; }

    public int? Margin { get; set; }

    public int? Height {get; set; }
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
    //public string MapSrc { get; set; }

    public int? mapHeight { get; set; }
    
    public int? MapId { get; set; }
    #endregion

    #region Navigation
    public int? DestinationViewId { get; set; }
    #endregion

    public int Order {get; set;}
    public int ViewId { get; set; }

    public int YearId { get; set; }
}