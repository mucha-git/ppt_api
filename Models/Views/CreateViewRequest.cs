using System.ComponentModel.DataAnnotations;
using WebApi.Helpers;

namespace WebApi.Models.Views;

public class CreateViewRequest {
    [Required]
    public string Title { get; set; }
    [Required]
    public ListType Type {get; set;}

    public string HeaderText { get; set; }

    public ScreenType? ScreenType { get; set; }

    public string ExternalUrl { get; set; }

    public string ImgSrc { get; set; }

    public int? Order {get; set;}

    public int YearId { get; set; }

    public int? ViewId { get; set; }
}