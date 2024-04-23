using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Entities.Traccar;

namespace WebApi.Helpers;
public class TraccarDataContext : DbContext
{
    public DbSet<Devices> Devices { get; set; }
    public DbSet<Positions> Positions { get; set; }
    public DbSet<Groups> Groups { get; set; }

    private readonly IConfiguration Configuration;

    public TraccarDataContext(IConfiguration configuration, DbContextOptions<TraccarDataContext> options) : base(options)
    {
        Configuration = configuration;
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder options)
    //{
    //    // connect to sqlite database
    //    options.UseFirebird(Configuration.GetConnectionString("WebApiDatabase"));
    //}
}