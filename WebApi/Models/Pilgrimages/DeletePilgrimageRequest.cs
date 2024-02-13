using System.ComponentModel.DataAnnotations;
using FluentValidation;
using WebApi.Helpers;

namespace WebApi.Models.Years;

public class DeletePilgrimageRequest {
    [Required]
    public int Id {get; set;}
}