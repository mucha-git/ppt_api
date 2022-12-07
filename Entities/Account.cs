using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Account
{
    public int Id { get; set; }
    [Column(TypeName = "varchar(80)")]
    public string Title { get; set; }
    [Column(TypeName = "varchar(80)")]
    public string FirstName { get; set; }
    [Column(TypeName = "varchar(80)")]
    public string LastName { get; set; }
    [Column(TypeName = "varchar(255)")]
    public string Email { get; set; }
    [Column(TypeName = "varchar(1000)")]
    public string PasswordHash { get; set; }
    public bool AcceptTerms { get; set; }
    public Role Role { get; set; }
    [Column(TypeName = "varchar(1000)")]
    public string VerificationToken { get; set; }
    public DateTime? Verified { get; set; }
    public bool IsVerified => Verified.HasValue || PasswordReset.HasValue;
    [Column(TypeName = "varchar(1000)")]
    public string ResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }
    public DateTime? PasswordReset { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; }

    public bool OwnsToken(string token)
    {
        return this.RefreshTokens?.Find(x => x.Token == token) != null;
    }
}