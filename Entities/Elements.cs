using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Helpers;

namespace WebApi.Entities;

public class Elements
{
    public int Id { get; set; }
    public ElementType Type { get; set; }
    #region Divider
    [Column(TypeName = "varchar(50)")]
    public string Color { get; set; }

    public int? Margin { get; set; }

    public int? Height {get; set; }
    #endregion

    #region Graphic And Text
    //[Column(TypeName = "varchar(1000)")]
    public string Text { get; set; }

    [Column(TypeName = "varchar(1000)")]
    public string ImgSrc { get; set; }
    #endregion

    #region Youtube
    public bool? Autoplay { get; set; }

    [Column(TypeName = "varchar(1000)")]
    public string Playlist { get; set; }
    #endregion

    #region Map

    public int? mapHeight { get; set; }
    
    public int? MapId { get; set; }
    public Maps Map {get; set;}
    #endregion

    #region Navigation
    public int? DestinationViewId { get; set; }

    #endregion
    
     public int? Order {get; set;}
    public int ViewId { get; set; }
    public Views View {get; set;}

    public int YearId { get; set; }
    public Years Year { get; set; }
    
}