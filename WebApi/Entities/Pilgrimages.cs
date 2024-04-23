using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi.Entities.PilgrimagesTypes;

namespace WebApi.Entities;

public class Pilgrimages : PilgrimageValues
{
    public int Id { get; set; }

    [Column(TypeName = "varchar(500)")]
    public string Name { get; set; }
    
    public bool isActive {get; set;}

    [Column(TypeName = "varchar(1000)")]
    public string LogoSrc { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string OneSignal {get; set;}

    [Column(TypeName = "varchar(50)")]
    public string OneSignalApiKey {get; set;}

    public IEnumerable<Account> Accounts { get; set; }

    public IEnumerable<Years> Years { get; set; }
    
    [JsonIgnore]
    public string Values { get; set; }
    
    [NotMapped]
    public int? GroupId { get; set; }
    
    public void SetValues(){
        var jsonOptions = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        PilgrimageValues vv = new PilgrimageValues(){ GroupId = GroupId};
        Values = JsonSerializer.Serialize(vv, jsonOptions);
    }

    public void SetPropsValues() {
        var jsonOptions = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        if (Values is null)
        {
            GroupId = null;
        }
        else
        {
            var pilgrimageValues = JsonSerializer.Deserialize<PilgrimageValues>(Values, jsonOptions);
            GroupId = pilgrimageValues.GroupId;
        }
    }
}