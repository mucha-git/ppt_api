using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    [Index(nameof(ParentId), IsUnique = false)]
    public class Trade
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account Account { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Symbol { get; set; }

        public double Volume { get; set; }

        [Column(TypeName = "varchar(80)")]
        public string Comment { get; set; }

        public bool IsBuying { get; set; }

        public double? Tp { get; set; }

        public double? Sl { get; set; }

        public int Status { get; set; }

        public double Value { get; set; }
        public int? ParentId { get; set; }
    }
}
