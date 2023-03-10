using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class Machine
  {
    // properties, constructors, methods, etc. go here
    public int MachineId { get; set; }
    [Required(ErrorMessage = "Manufacturer is required")]
    public string Manufacturer { get; set; }
    [Required(ErrorMessage = "Model is required")]
    public string Model { get; set; }
    [Required(ErrorMessage = "Serial number is required.")]
    public string Serial { get; set; }
    [Required(ErrorMessage = "Operating status is required.")]
    public bool OperationalStatus { get; set; }
    [Required(ErrorMessage = "Repair status is required.")]
    public bool RepairStatus { get; set; }
    public Nullable <DateTime> InspectionDate { get; set; }
    public List<EngineerMachine> EngineerMachines { get; }
  }
}
