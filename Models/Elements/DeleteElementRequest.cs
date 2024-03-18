using System.ComponentModel.DataAnnotations;
using FluentValidation;
using WebApi.Helpers;

namespace WebApi.Models.Elements;

public class DeleteElementRequest {
    [Required]
    public int Id {get; set;}
    [Required]
    public int YearId {get; set;}
}

public class DeleteElementRequestValidator : AbstractValidator<DeleteElementRequest> {
    public DeleteElementRequestValidator(IValidations validations) {
        RuleFor( v => v.YearId).MustAsync(async (request , _) => {
            return await validations.IsYearValid(request);
        }).WithMessage("Nie można edytować cudzych danych");
        RuleFor( v => v.Id).MustAsync(async (request , _) => {
            return await validations.IsElementValid(request);
        }).WithMessage("Nie można edytować cudzych danych");
    }
} 