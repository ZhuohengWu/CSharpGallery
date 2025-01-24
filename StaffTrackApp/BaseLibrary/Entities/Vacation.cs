using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities;

public class Vacation : RecordBaseEntity
{
    [Required] public DateTime StartDate { get; set; }
    [Required] public int NumOfDays { get; set; }
    public DateTime EndDate => StartDate.AddDays(NumOfDays);

    public int VacationTypeId { get; set; }
    public VacationType? VacationType { get; set; }
}
