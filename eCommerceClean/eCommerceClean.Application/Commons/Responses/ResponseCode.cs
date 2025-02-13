using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceClean.Application.Commons.Responses;

public enum ResponseCode
{
    Success = 0,
    BadRequest = 1,
    Unauthorized = 2,
    NotFound = 3,
    InternalServerError = 4
}
