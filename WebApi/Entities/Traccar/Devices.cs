using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApi.Entities.Traccar;
[Table("tc_devices")]
public class Devices
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("name", TypeName = "varchar(128)")]
    public string Name { get; set; }
    
    [Column("status", TypeName = "varchar(8)")]
    public string Status { get; set; }
    
    [Column("groupid")]
    public int GroupId { get; set; }
    
    public Groups Group { get; set; }
    
    [Column("positionid")]
    public int PositionId { get; set; }
    
    public Positions Position { get; set; }
}