namespace Persistence;

using System.Collections;

using Core.Contracts;
using Core.Entities;

using System.Threading.Tasks;
using System.Linq;

using Base.Tools.CsvImport;

using Persistence.ImportData;

public class ImportService : IImportService
{
    public ImportService()
    {
    }

    public async Task ImportDbAsync()
    {
        var cruiserCsv = await new CsvImport<CruiserCsv>().ReadAsync("ImportData/Schiffe.txt");

        ///TODO: Import data into database
        /// you need a uow (Constructor injection)
    }
}