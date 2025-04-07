using Core.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

using Base.Web.Controller;

using Core.DataTransferObjects;
using Core.Entities;

/// <summary>
/// REST Controller for ShippingCompany`s.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ShippingCompanyController : ControllerBase
{
    private readonly IUnitOfWork                        _uow;
    private readonly ILogger<ShippingCompanyController> _logger;

    /// <summary>
    /// Constructor of ShippingCompanyController.
    /// </summary>
    /// <param name="uow"></param>
    /// <param name="logger"></param>
    public ShippingCompanyController(IUnitOfWork uow, ILogger<ShippingCompanyController> logger)
    {
        _uow    = uow;
        _logger = logger;
    }

    #region Dto

    public record CruiserShipDto(
        int      Id,
        string   Name,
        uint     YearOfConstruction,
        uint?    Tonnage,
        decimal? Length,
        uint?    Cabins,
        uint?    Passengers,
        uint?    Crew,
        string?  Remark,
        string?  oldNames
    );

    /// <summary>
    /// ShippingCompanyDto
    /// </summary>
    public record ShippingCompanyDto(
        int                    Id,
        string                 Name,
        string?                City,
        string?                Plz,
        string?                Street,
        string?                StreetNo,
        IList<CruiserShipDto>? CruiseShips);

    ShippingCompany ToEntity(ShippingCompanyDto dto)
    {
        return new ShippingCompany()
        {
            Id       = dto.Id,
            Name     = dto.Name,
            Street   = dto.Street,
            StreetNo = dto.StreetNo,
            City     = dto.City,
            PLZ      = dto.Plz,
        };
    }

    ShippingCompanyDto? ToDto(ShippingCompany? entity)
    {
        if (entity is null)
        {
            return null;
        }

        return new ShippingCompanyDto(entity.Id,
            entity.Name,
            entity.City,
            entity.PLZ,
            entity.Street,
            entity.StreetNo,
            entity.CruiseShips.Select(s => ToDto(s)).ToList());
    }

    IList<ShippingCompanyDto>? ToDto(IList<ShippingCompany>? list)
    {
        if (list is null)
        {
            return null;
        }

        return list.Select(x => ToDto(x)!).ToList();
    }

    CruiserShipDto? ToDto(CruiseShip? entity)
    {
        if (entity is null)
        {
            return null;
        }

        return new
            CruiserShipDto(entity.Id,
                entity.Name,
                entity.YearOfConstruction,
                entity.Tonnage,
                entity.Length,
                entity.Cabins,
                entity.Passengers,
                entity.Crew,
                entity.Remark,
                string.Join(",", entity.ShipNames!.Select(s => s.Name)));
    }

    #endregion

    #region default REST

    /// <summary>
    /// Get all ShippingCompanies.
    /// </summary>
    /// <param name="sort">Optional sort by property.</param>
    /// <returns></returns>
    [HttpGet("overview")]
    public async Task<ActionResult<List<ShippingCompanyDto>>> GetAsync(string? sort)
    {
        var allEntities = (await _uow.ShippingCompanyRepository.GetAsync(includeProperties: ["CruiseShips", "CruiseShips.ShipNames"]))
            .ToList()
            .Select(ToDto)
            .ToList();
        
        return await this.NotFoundOrOk(allEntities);

    }

    /// <summary>
    /// Get a specified ShippingCompany.
    /// </summary>
    /// <param name="id">The id of the ShippingCompany.</param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ShippingCompanyDto>> GetAsync(int id)
    {

        var entity = await _uow.ShippingCompanyRepository.GetByIdAsync(id,
            nameof(ShippingCompany.CruiseShips),
            $"{nameof(ShippingCompany.CruiseShips)}.{nameof(CruiseShip.ShipNames)}"
        );
        
        return await this.NotFoundOrOk(ToDto(entity));
    }

    /// <summary>
    /// Add a new ShippingCompany to the database.
    /// </summary>
    /// <param name="value">Values of the new ShippingCompany.</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ShippingCompanyDto>> AddAsync([FromBody] ShippingCompanyDto value)
    {
        using (var trans = _uow.BeginTransaction())
        {
            var entity = ToEntity(value);
            await _uow.ShippingCompanyRepository.AddAsync(entity);

            await trans.CommitTransactionAsync();

            var newId  = entity.Id;
            var newUri = this.GetCurrentUri() + "/" + newId;
            return Created(newUri, ToDto(await _uow.ShippingCompanyRepository.GetByIdAsync(newId)));
        }
    }

    /// <summary>
    /// Update the specified ShippingCompany.
    /// </summary>
    /// <param name="id">Id of the ShippingCompany.</param>
    /// <param name="value">New values of the ShippingCompany.</param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] ShippingCompanyDto value)
    {
        if (id.CompareTo(value.Id) != 0)
        {
            return BadRequest("Mismatch between id and dto.Id");
        }

        using (var trans = _uow.BeginTransaction())
        {
            var entity = await _uow.ShippingCompanyRepository.GetByIdAsync(id);
            if (entity is null)
            {
                return NotFound();
            }
            
            entity.Name     = value.Name;
            entity.Street   = value.Street;
            entity.StreetNo = value.StreetNo;
            entity.City     = value.City;
            entity.PLZ      = value.Plz;
            
            await _uow.SaveChangesAsync();
            await trans.CommitTransactionAsync();
        }

        return NoContent();
    }

    /// <summary>
    /// Delete a ShippingCompany specified by the id.
    /// </summary>
    /// <param name="id">The id of the ShippingCompany.</param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        using (var trans = _uow.BeginTransaction())
        {
            var entry = await _uow.ShippingCompanyRepository.GetByIdAsync(id);
            if (entry is not null)
            {
                _uow.ShippingCompanyRepository.Remove(entry);
            }

            await trans.CommitTransactionAsync();

            return NoContent();
        }
    }

    #endregion
}