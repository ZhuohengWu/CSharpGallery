using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities;

public class City:BaseEntity
{
    public int CountryId { get; set; }
    public Country? Country { get; set; }

    public List<Town>? Towns { get; set; }
}
