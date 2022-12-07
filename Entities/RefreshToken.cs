namespace WebApi.Entities;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Owned]
public class RefreshToken
{
    [Key]
    public int Id { get; set; }
    public Account Account { get; set; }

    [Column(TypeName = "varchar(1000)")]
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }

    [Column(TypeName = "varchar(80)")]
    public string CreatedByIp { get; set; }
    public DateTime? Revoked { get; set; }

    [Column(TypeName = "varchar(80)")]
    public string RevokedByIp { get; set; }

    [Column(TypeName = "varchar(1000)")]
    public string ReplacedByToken { get; set; }

    [Column(TypeName = "varchar(80)")]
    public string ReasonRevoked { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public bool IsRevoked => Revoked != null;
    public bool IsActive => Revoked == null && !IsExpired;
}