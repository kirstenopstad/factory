using Microsoft.AspNetCore.Mvc;
using Factory.Models;

namespace Factory.Controllers
{
  public class HomeController : Controller
  {
    // Routes
    private readonly FactoryContext _db;

    public HomeController(FactoryContext db)
    {
      _db = db;
    }
    
    public ActionResult Index()
    {
      return View();
    }
  }
}