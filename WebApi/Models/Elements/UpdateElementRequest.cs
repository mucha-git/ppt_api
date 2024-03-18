using System.ComponentModel.DataAnnotations;
using FluentValidation;
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

    public int? MapHeight { get; set; }
    
    public int? MapId { get; set; }
    #endregion

    #region Navigation
    public int? DestinationViewId { get; set; }
    #endregion

    #region View
    public string ExternalUrl { get; set; }
    public string Title { get; set; }
    public ListType? ViewType {get; set;}

    public string HeaderText { get; set; }

    public ScreenType? ScreenType { get; set; }
    #endregion

    public int Order {get; set;}
    public int ViewId { get; set; }

    public int YearId { get; set; }
}

public class UpdateElementRequestValidator : AbstractValidator<UpdateElementRequest> {
    public UpdateElementRequestValidator(IValidations validations) {
        RuleFor( v => v.YearId).MustAsync(async (request , _) => {
            return await validations.IsYearValid(request);
        }).WithMessage("Nie można edytować cudzych danych");
        RuleFor( v => v).MustAsync(async (request , _) => {
            return await validations.IsViewValid(request.YearId, request.ViewId);
        }).WithMessage("Nie można edytować cudzych danych");
        RuleFor( v => v).MustAsync(async (request , _) => {
            return request.DestinationViewId == null || await validations.IsViewValid(request.YearId, (int)request.DestinationViewId);
        }).WithMessage("Nie można edytować cudzych danych");
        RuleFor( v => v.MapId).MustAsync(async (request , _) => {
            return request == null || await validations.IsMapValid((int)request);
        }).WithMessage("Nie można edytować cudzych danych");
    }
} 