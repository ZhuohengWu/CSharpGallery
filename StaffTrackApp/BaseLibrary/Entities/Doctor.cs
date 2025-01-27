using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities;

public class Doctor : RecordBaseEntity
{
    [Required] public DateTime Date { get; set; }
    [Required] public string Diagnose { get; set; } = string.Empty;
    [Required] public string Recommondation { get; set; } = string.Empty;
}
