using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities;

public class UserRole
{
    [Key]
    public int Id { get; set; }
    public int RoleId { get; set; }
    public int UserId { get; set; }
}
