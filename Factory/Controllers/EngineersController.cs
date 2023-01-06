using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Factory.Models;

namespace Factory.Controllers
{
  public class EngineersController : Controller
  {
    // Routes
    private readonly FactoryContext _db;

    public EngineersController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Engineers.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Engineer engineer)
    {
      if (!ModelState.IsValid)
      {
        return View(engineer);
      }
      else
      {
        _db.Engineers.Add(engineer);
        _db.SaveChanges();
        return RedirectToAction("Index");  
      }
    }

    public ActionResult Details(int id)
    {
      Engineer thisEngineer = _db.Engineers
                                 .Include(engineer => engineer.EngineerMachines)
                                 .ThenInclude(engineerMachine => engineerMachine.Machine)
                                 .FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(thisEngineer);
    }

    public ActionResult Edit(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult Edit(Engineer engineer)
    {
      if (!ModelState.IsValid)
      {
        return View(engineer);
      }
      else
      {
        _db.Engineers.Update(engineer);
        _db.SaveChanges();
        return RedirectToAction("Details", new { id = engineer.EngineerId});  
      }
    }

    [HttpPost]
    public ActionResult Delete(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      _db.Engineers.Remove(thisEngineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddMachine(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Model");
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult AddMachine(Engineer engineer, int machineId)
    {
      // query to see if assocation exists, make null if not
      #nullable enable
      EngineerMachine? association = _db.EngineerMachines
                                         .FirstOrDefault(assoc => (assoc.EngineerId == engineer.EngineerId &&  assoc.MachineId == machineId));
      #nullable disable
      // if assoc does not exist
      if (association == null && machineId != 0)
      {
        _db.EngineerMachines
           .Add(new EngineerMachine() { MachineId = machineId, EngineerId = engineer.EngineerId});
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = engineer.EngineerId});
    }

    [HttpPost]
    public ActionResult RemoveMachine(int id)
    {
      EngineerMachine assocation = _db.EngineerMachines.FirstOrDefault(assoc => assoc.EngineerMachineId == id);
      _db.EngineerMachines.Remove(assocation);
      _db.SaveChanges();
      return RedirectToAction("Details", "Engineers", new { id = assocation.Engineer.EngineerId});
    }
  }
}