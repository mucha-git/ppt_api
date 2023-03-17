using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.OneSignal;

public class CreatePostMessage
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Content { get; set; }
}