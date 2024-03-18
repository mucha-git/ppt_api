namespace WebApi.Models.Pilgrimages;

using System.ComponentModel.DataAnnotations;
using FluentValidation;
using WebApi.Services;

public class GetPilgrimageRequest
{
    [Required]
    public int PilgrimageId { get; set; }
}