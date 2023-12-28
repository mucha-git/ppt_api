using System.ComponentModel.DataAnnotations;
using FluentValidation;
using WebApi.Helpers;

namespace WebApi.Models.Years;

public class DeleteYearRequest {
    [Required]
    public int Id {get; set;}
}

public class DeleteYearRequestValidator : AbstractValidator<DeleteYearRequest> {
    public DeleteYearRequestValidator(IValidations validations) {
        RuleFor( v => v.Id).MustAsync(async (request , _) => {
            return await validations.IsYearValid(request);
        }).WithMessage("Nie można edytować cudzych danych");
    }
} 