using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Helpers;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi.Entities.ElementsTypes;
using Org.BouncyCastle.Asn1.X509.Qualified;

namespace WebApi.Entities;

public class Elements : IDivider, IGraphicAndText, IMap, INavigation, IYoutube
{
    public int Id { get; set; }
    public ElementType Type { get; set; }
    #region Divider
    [NotMapped]
    public string Color { get; set; }
    
    [NotMapped]
    public int? Margin { get; set; }
    [NotMapped]
    public int? Height { get; set; }
    #endregion

    #region Graphic And Text
    [NotMapped]
    public string Text { get; set; }
    [NotMapped]
    [Column(TypeName = "varchar(1000)")]
    public string ImgSrc { get; set; }
    #endregion

    #region Youtube
    [NotMapped]
    public bool? Autoplay { get; set; }
    [NotMapped]
    [Column(TypeName = "varchar(1000)")]
    public string Playlist { get; set; }
    #endregion

    #region Map
    [NotMapped]
    public int? MapHeight { get; set; }

    public int? MapId { get; set; }
    public Maps Map { get; set; }
    #endregion

    #region Navigation
    [NotMapped]
    public int? DestinationViewId { get; set; }

    #endregion

    public int? Order { get; set; }
    public int ViewId { get; set; }
    [JsonIgnore]
    public Views View { get; set; }

    public int YearId { get; set; }
    [JsonIgnore]
    public Years Year { get; set; }

    // to jest zapisywane w bazie
    [JsonIgnore]
    public string Values { get; set; }

    public void SetValues(){
        var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        ElementValues ev = new ElementValues(){Color = Color, Margin = Margin, Height = Height, Text = Text, ImgSrc = ImgSrc, Autoplay = Autoplay, Playlist = Playlist, MapHeight = MapHeight, DestinationViewId = DestinationViewId};
            Values = JsonSerializer.Serialize(ev, jsonOptions);
    }

    public void SetPropsValues() {
        var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        var elementValues = JsonSerializer.Deserialize<ElementValues>(Values, jsonOptions);
        Color = elementValues.Color;
        Margin = elementValues.Margin;
        Height = elementValues.Height;
        Text = elementValues.Text;
        ImgSrc = elementValues.ImgSrc;
        Autoplay = elementValues.Autoplay;
        Playlist = elementValues.Playlist;
        MapHeight = elementValues.MapHeight;
        DestinationViewId = elementValues.DestinationViewId;
    }
}