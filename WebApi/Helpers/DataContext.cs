using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Helpers;
public class DataContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Coordinates> Coordinates { get; set; }
    public DbSet<Elements> Elements { get; set; }
    public DbSet<MapPins> MapPins { get; set; }
    public DbSet<Maps> Maps { get; set; }
    public DbSet<Markers> Markers { get; set; }
    public DbSet<Pilgrimages> Pilgrimages { get; set; }
    public DbSet<Views> Views { get; set; }
    public DbSet<Years> Years { get; set; }

    private readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration, DbContextOptions<DataContext> options) : base(options)
    {
        Configuration = configuration;
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Years>()
            .Property(b => b.ColumnsCount)
            .HasDefaultValue(1);
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder options)
    //{
    //    // connect to sqlite database
    //    options.UseFirebird(Configuration.GetConnectionString("WebApiDatabase"));
    //}
}