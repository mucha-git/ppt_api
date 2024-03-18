namespace WebApi.Models.Pilgrimages;

using System.ComponentModel.DataAnnotations;
using FluentValidation;
using WebApi.Helpers;
using WebApi.Services;

public class GetYearDataRequest
{
    [Required]
    public int YearId {get; set; }
}

public class GetYearDataRequestValidator : AbstractValidator<GetYearDataRequest> {
    public GetYearDataRequestValidator(IValidations validations, IAccount account) {
        RuleFor( v => v.YearId).MustAsync(async (request , _) => {
            return account.Account == null || await validations.IsYearValid(request);
        }).WithMessage("Nie można edytować cudzych danych");
    }
} 