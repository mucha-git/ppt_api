using System.ComponentModel.DataAnnotations;
using FluentValidation;
using WebApi.Helpers;

namespace WebApi.Models.Views;

public class UpdateViewRequest {
    [Required]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public ListType Type {get; set;}

    public string HeaderText { get; set; }

    public ScreenType? ScreenType { get; set; }

    public string ExternalUrl { get; set; }

    public string ImgSrc { get; set; }

    public int Order {get; set;}

    public int YearId { get; set; }

    public int? ViewId { get; set; }
    
    public bool IsSearchable { get; set; }
}

public class UpdateViewRequestValidator : AbstractValidator<UpdateViewRequest> {
    public UpdateViewRequestValidator(IValidations validations) {
        RuleFor( v => v.YearId).MustAsync(async (request , _) => {
            return await validations.IsYearValid(request);
        }).WithMessage("Nie można edytować cudzych danych");
        RuleFor( v => v).MustAsync(async (request , _) => {
            return await validations.IsViewValid(request.YearId, request.Id) 
                    && (request.ViewId == null || await validations.IsViewValid(request.YearId, (int)request.ViewId));
        }).WithMessage("Nie można edytować cudzych danych");
    }
} 