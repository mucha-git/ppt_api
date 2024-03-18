using System.ComponentModel.DataAnnotations;
using FluentValidation;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Models.Pilgrimages;

public class UpdatePilgrimageRequest {
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public bool isActive {get; set;}

    public string LogoSrc { get; set; }
    public string OneSignal {get; set;}
    public string OneSignalApiKey {get; set;}
}

public class UpdatePilgrimageRequestValidator : AbstractValidator<UpdatePilgrimageRequest> {
    public UpdatePilgrimageRequestValidator(IAccount account) {
        RuleFor( v => v.Id).Must(request => {
            return account.Account.Role == Role.Admin || account.Account.PilgrimageId == request;
        }).WithMessage("Nie można edytować cudzych danych");
    }
} 