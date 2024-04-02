using System.ComponentModel.DataAnnotations;
using FluentValidation;
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
    public bool IsSearchable { get; set; }
}

public class CreateViewRequestValidator : AbstractValidator<CreateViewRequest> {
    public CreateViewRequestValidator(IValidations validations) {
        RuleFor( v => v.YearId).MustAsync(async (request , _) => {
            return await validations.IsYearValid(request);
        }).WithMessage("Nie można edytować cudzych danych");
        RuleFor( v => v).MustAsync(async (request , _) => {
            return request.ViewId == null || await validations.IsViewValid(request.YearId, (int)request.ViewId);
        }).WithMessage("Nie można edytować cudzych danych");
    }
} 