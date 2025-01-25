using ExcelReader.NathanJenner.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExcelReader.NathanJenner.Repository;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public virtual DbSet<PlayerEntity> Players { get; set; }
}
