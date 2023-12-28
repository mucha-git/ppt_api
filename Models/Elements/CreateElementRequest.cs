using System.ComponentModel.DataAnnotations;
using FluentValidation;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Models.Elements;

public class CreateElementRequest {
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

    public int? Order {get; set;}
    public int ViewId { get; set; }

    public int YearId { get; set; }
}

public class CreateElementRequestValidator : AbstractValidator<CreateElementRequest> {
    public CreateElementRequestValidator(IValidations validations) {
        RuleFor( v => v.YearId).MustAsync(async (request , _) => {
            return await validations.IsYearValid(request);
        }).WithMessage("Nie można edytować cudzych danych");
        RuleFor( v => v).MustAsync(async (request , _) => {
            return await validations.IsViewValid(request.YearId, request.ViewId);
        }).WithMessage("Nie można edytować cudzych danych");
        RuleFor( v => v).MustAsync(async (request , _) => {
            switch (request.Type)
            {
                case ElementType.Map: 
                    return await validations.IsMapValid((int)request.MapId);
                case ElementType.Navigation:
                    return await validations.IsMapValid((int)request.DestinationViewId);
                default:
                    return true;
            }
        }).WithMessage("Nie można edytować cudzych danych");
    }
} 