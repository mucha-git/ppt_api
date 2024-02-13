using System.ComponentModel.DataAnnotations;
using FluentValidation;
using WebApi.Helpers;

namespace WebApi.Models.Views;

public class DeleteViewRequest {
    [Required]
    public int Id {get; set;}
    [Required]
    public int YearId {get; set;}
}

public class DeleteViewRequestValidator : AbstractValidator<DeleteViewRequest> {
    public DeleteViewRequestValidator(IValidations validations) {
        RuleFor( v => v.YearId).MustAsync(async (request , _) => {
            return await validations.IsYearValid(request);
        }).WithMessage("Nie można edytować cudzych danych");
        RuleFor( v => v).MustAsync(async (request , _) => {
            return await validations.IsViewValid(request.YearId, request.Id);
        }).WithMessage("Nie można edytować cudzych danych");
    }
} 