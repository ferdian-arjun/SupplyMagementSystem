using Microsoft.AspNetCore.Mvc;

namespace SupplyManagementSystem.Controllers;

public class VendorController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    // GET
    public IActionResult Index()
    {
        return View();
    }
}