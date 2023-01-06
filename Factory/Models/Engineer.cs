using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models

{
  public class Engineer
  {
    // properties, constructors, methods, etc. go here
    public int EngineerId { get; set; }
    [Required(ErrorMessage = "Engineer Name is required")]
    public string Name { get; set; }
    public bool Status { get; set; }
    public List<EngineerMachine> EngineerMachines { get; }
    
  }
}
