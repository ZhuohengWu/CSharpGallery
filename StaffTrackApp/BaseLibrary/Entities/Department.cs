﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BaseLibrary.Entities;

public class Department : BaseEntity
{
    public GeneralDepartment? GeneralDepartment {  get; set; }
    public int GeneralDepartmentId { get; set; }
    [JsonIgnore]
    public List<Branch>? Branches { get; set; }
}
