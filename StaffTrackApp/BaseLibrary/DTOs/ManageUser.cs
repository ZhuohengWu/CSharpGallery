﻿using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.DTOs;

public class ManageUser
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public int UserId { get; set; }
    public string? Role { get; set; }
}
