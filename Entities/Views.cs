using System.ComponentModel.DataAnnotations.Schema;
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

    public int YearId { get; set; }
    public Years Year { get; set; }

    public int? ViewId { get; set; }
    public Views View { get; set; } 
    public IEnumerable<Views> ViewsList { get; set; }

    //public IEnumerable<Elements> Elements { get; set; }
}