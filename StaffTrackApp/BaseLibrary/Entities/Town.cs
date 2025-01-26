using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BaseLibrary.Entities;

public class Town : BaseEntity
{
    [JsonIgnore]
    public List<Employee>? Employees { get; set; }
    public City? City { get; set; }
    public int CityId { get; set; }
}
