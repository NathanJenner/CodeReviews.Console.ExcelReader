using ExcelReader.NathanJenner.Entities;
using ExcelReader.NathanJenner.Repository;
using Spectre.Console;

namespace ExcelReader.NathanJenner.Services;

public class PlayerServices(PlayerRepository _playerRepository)
{
    public void PlayerExcelToDbService(IEnumerable<PlayerEntity> values)
    {
        foreach (var value in values)
        {
            _playerRepository.Add(value);
        }
    }

    public void DisplayEntities()
    {
        var list = _playerRepository.GetAll();

        AnsiConsole.Markup("[red]Here are the Entity details [/]\n\n");

        var table = new Table();
        table.AddColumn("FullName");
        table.AddColumn("Position");
        table.AddColumn("Number");

        foreach (var entity in list)
        {
            table.AddRow(entity.FullName.ToString(), entity.Position.ToString(), entity.Number.ToString());
        }
        AnsiConsole.Write(table);


    }
}
