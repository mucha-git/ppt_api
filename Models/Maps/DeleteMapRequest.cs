using System.ComponentModel.DataAnnotations;
using FluentValidation;
using WebApi.Helpers;

namespace WebApi.Models.Maps;

public class DeleteMapRequest {
    [Required]
    public int Id {get; set;}
    [Required]
    public int YearId {get; set;}
}

public class DeleteMapRequestValidator : AbstractValidator<DeleteMapRequest> {
    public DeleteMapRequestValidator(IValidations validations) {
        RuleFor( v => v.YearId).MustAsync(async (request , _) => {
            return await validations.IsYearValid(request);
        }).WithMessage("Nie można edytować cudzych danych");
        RuleFor( v => v.Id).MustAsync(async (request , _) => {
            return await validations.IsMapValid(request);
        }).WithMessage("Nie można edytować cudzych danych");
    }
} 