using ExcelReader.NathanJenner.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExcelReader.NathanJenner.Repository;

public class PlayerRepository(ApplicationDbContext _dbContext)
{
    public List<PlayerEntity> GetAll()
    {
        return _dbContext.Set<PlayerEntity>().ToList();
    }

    public void Add(PlayerEntity entity)
    {
        Console.Out.WriteLine("Adding a Player to the database");
        _dbContext.Set<PlayerEntity>().Add(entity);
        _dbContext.SaveChanges();
    }

    public void Update(PlayerEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }
}
