using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApi.Entities.Traccar;
[Table("tc_groups")]
public class Groups
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("name", TypeName = "varchar(128)")]
    public string Name { get; set; }
    
    public IEnumerable<Devices> Devices { get; set; }
}