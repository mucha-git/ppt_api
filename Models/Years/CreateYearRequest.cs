using System.ComponentModel.DataAnnotations;
using FluentValidation;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Models.Years;

public class CreateYearRequest {
    [Required]
    public int Year {get; set;}
    [Required]
    public string YearTopic { get; set; }
    [Required]
    public bool isActive {get; set;}

    public string ImgSrc { get; set; }
    [Required]
    public int PilgrimageId { get; set; }

    public int? SourceYearId { get; set; }
}

public class CreateYearRequestValidator : AbstractValidator<CreateYearRequest> {
    public CreateYearRequestValidator(IAccount account) {
        RuleFor( v => v.PilgrimageId)
            .Must(pilgrimageId => pilgrimageId == account.Account.PilgrimageId)
            .WithMessage("Nie można edytować cudzych danych");
    }
} 