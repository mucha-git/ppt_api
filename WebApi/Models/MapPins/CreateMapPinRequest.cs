using System.ComponentModel.DataAnnotations;
using FluentValidation;
using WebApi.Helpers;

namespace WebApi.Models.MapPins;

public class CreateMapPinRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string PinSrc { get; set; }
    public string IconSrc { get; set; }
    [Required]
    public int Width { get; set; }
    [Required]
    public int Height { get; set; }
    [Required]
    public int YearId { get; set; }
}

public class CreateMapPinRequestValidator : AbstractValidator<CreateMapPinRequest> {
    public CreateMapPinRequestValidator(IValidations validations) {
        RuleFor( v => v.YearId).MustAsync(async (request , _) => {
            return await validations.IsYearValid(request);
        }).WithMessage("Nie można edytować cudzych danych");
    }
} 