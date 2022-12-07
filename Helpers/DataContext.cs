using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Helpers;
public class DataContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }

    private readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration, DbContextOptions<DataContext> options) : base(options)
    {
        Configuration = configuration;
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder options)
    //{
    //    // connect to sqlite database
    //    options.UseFirebird(Configuration.GetConnectionString("WebApiDatabase"));
    //}
}