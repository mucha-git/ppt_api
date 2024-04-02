using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Helpers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApi.Entities.ViewsTypes;

public class ViewValues
{
    public bool IsSearchable { get; set; }
}