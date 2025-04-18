﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.DTOs;

public class Register : AccountBase
{
    [MinLength(5)]
    [MaxLength(100)]
    [Required]
    public string? FullName { get; set; }

    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    [Required]
    public string? ConfirmPassword { get; set; }
}
