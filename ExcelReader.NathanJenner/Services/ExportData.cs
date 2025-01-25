using ExcelReader.NathanJenner.Entities;
using OfficeOpenXml;
using Spectre.Console;

namespace ExcelReader.NathanJenner.Services;

public class ExportData
{
    public static IEnumerable<PlayerEntity> ExportFromExcel()
    {
        AnsiConsole.Markup("\n\n\n[fuchsia]Accessing the Excel file.[/]\n");
        using (var package = new ExcelPackage(@"")) //ADD FILE PATH
        {
            AnsiConsole.Markup("[fuchsia]Exporting the data from the Excel workbook.[/]\n\n\n");
            var ws = package.Workbook.Worksheets[0];
            IEnumerable<PlayerEntity> exportedPlayers = ws.Cells["A1:C19"].ToCollection<PlayerEntity>();

            return exportedPlayers;
        }
    }
}
