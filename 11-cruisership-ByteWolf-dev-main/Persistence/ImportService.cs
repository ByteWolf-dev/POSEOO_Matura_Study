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
    private IUnitOfWork _uow;

    public ImportService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task ImportDbAsync()
    {
        var cruiserCsvList = await new CsvImport<CruiserCsv>().ReadAsync("ImportData/Schiffe.txt");

        var          companyCache  = new Dictionary<string, ShippingCompany>(StringComparer.OrdinalIgnoreCase);
        const string NoCompanyName = "<No Company>";

        foreach (var csv in cruiserCsvList)
        {
            string companyName = string.IsNullOrWhiteSpace(csv.Reederei) ? NoCompanyName : csv.Reederei;
            if (!companyCache.TryGetValue(companyName, out var company))
            {
                company = (await _uow.ShippingCompanyRepository.GetAsync())
                    .FirstOrDefault(c => c.Name == companyName);

                if (company == null)
                {
                    company = new ShippingCompany { Name = companyName };
                    await _uow.ShippingCompanyRepository.AddAsync(company);
                }

                companyCache[companyName] = company;
            }

            var oldNames = new List<string>();
            var remark   = csv.Bemerkungen;

            if (!string.IsNullOrWhiteSpace(remark))
            {
                var parts        = remark.Split(',');
                var cleanedParts = new List<string>();

                foreach (var part in parts)
                {
                    var trimmed = part.Trim();
                    if (trimmed.StartsWith("ex ", StringComparison.OrdinalIgnoreCase))
                    {
                        var name = trimmed.Substring(3).Trim();
                        if (!string.IsNullOrWhiteSpace(name))
                            oldNames.Add(name);
                    }
                    else
                    {
                        cleanedParts.Add(trimmed);
                    }
                }

                remark = cleanedParts.Count > 0 ? string.Join(", ", cleanedParts) : null;
            }

            var ship = new CruiseShip
            {
                Name               = csv.Name,
                YearOfConstruction = csv.BJ,
                Tonnage            = csv.BRZ.HasValue ? (uint?)Math.Round(csv.BRZ.Value) : null,
                Length             = csv.Laenge,
                Cabins             = csv.Kab.HasValue ? (uint?)Math.Round(csv.Kab.Value) : null,
                Passengers         = csv.Pass.HasValue ? (uint?)Math.Round(csv.Pass.Value) : null,
                Crew               = csv.Bes.HasValue ? (uint?)Math.Round(csv.Bes.Value) : null,
                Remark             = remark,
                ShippingCompany    = company,
                ShipNames          = oldNames.Select(name => new ShipName { Name = name }).ToList()
            };

            await _uow.CruiseShipRepository.AddAsync(ship);
        }

        await _uow.SaveChangesAsync();
    }
}