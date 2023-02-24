using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Pilgrimages
{
    public int Id { get; set; }

    [Column(TypeName = "varchar(500)")]
    public string Name { get; set; }
    
    public bool isActive {get; set;}

    [Column(TypeName = "varchar(1000)")]
    public string LogoSrc { get; set; }

    public IEnumerable<Account> Accounts { get; set; }

    public IEnumerable<Years> Years { get; set; }
}