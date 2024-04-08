using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi.Entities.ElementsTypes;
using WebApi.Entities.ViewsTypes;
using WebApi.Helpers;

namespace WebApi.Entities;

public class Views
{
    public int Id { get; set; }

    [Column(TypeName = "varchar(80)")]
    public string Title { get; set; }
    
    public ListType Type {get; set;}

    [Column(TypeName = "varchar(80)")]
    public string HeaderText { get; set; }

    public ScreenType? ScreenType { get; set; }

    [Column(TypeName = "varchar(1000)")]
    public string ExternalUrl { get; set; }

    [Column(TypeName = "varchar(1000)")]
    public string ImgSrc { get; set; }
    public int? Order {get; set;}
    
    [NotMapped]
    public bool IsSearchable { get; set; }
    
    [JsonIgnore]
    public string Values { get; set; }

    public int YearId { get; set; }
    public Years Year { get; set; }

    public int? ViewId { get; set; }
    public Views View { get; set; } 
    public IEnumerable<Views> ViewsList { get; set; }

    //public IEnumerable<Elements> Elements { get; set; }
    
    public void SetValues(){
        var jsonOptions = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        ViewValues vv = new ViewValues(){ IsSearchable = IsSearchable};
        Values = JsonSerializer.Serialize(vv, jsonOptions);
    }

    public void SetPropsValues() {
        var jsonOptions = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        if (Values is null)
        {
            IsSearchable = false;
        }
        else
        {
            var viewValues = JsonSerializer.Deserialize<ViewValues>(Values, jsonOptions);
            IsSearchable = viewValues.IsSearchable && viewValues.IsSearchable;
        }
    }
}