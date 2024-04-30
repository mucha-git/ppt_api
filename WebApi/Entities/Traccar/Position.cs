using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApi.Entities.Traccar;
[Table("tc_positions")]
public class Positions
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("latitude")]
    public double Latitude { get; set; }
    
    [Column("longitude")]
    public double Longitude { get; set; }
}