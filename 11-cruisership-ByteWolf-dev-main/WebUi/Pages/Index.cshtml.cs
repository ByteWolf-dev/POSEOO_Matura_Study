using Core.Contracts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUi.Pages;

using Core.DataTransferObjects;
using Core.Entities;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IUnitOfWork         _uow;

    public IndexModel(ILogger<IndexModel> logger, IUnitOfWork uow)
    {
        _logger = logger;
        _uow    = uow;
    }

    public IActionResult OnGet()
    {
        return Page();
    }
}