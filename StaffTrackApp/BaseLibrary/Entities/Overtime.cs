using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities;

public class Overtime : RecordBaseEntity
{
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }
    public int NumOfDays => (EndDate - StartDate).Days;

    public OvertimeType? OvertimeType { get; set; }
    [Required]
    public int OvertimeTypeId { get; set; }
}
