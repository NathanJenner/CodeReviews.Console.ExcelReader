using System.ComponentModel.DataAnnotations;

namespace ExcelReader.NathanJenner.Entities;

public class PlayerEntity
{
    [Key]
    public int Id { get; set; }

    public string FullName { get; set; }
    public string Position { get; set; }
    public double Number { get; set; }
}
