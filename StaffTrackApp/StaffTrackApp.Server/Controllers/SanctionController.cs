﻿using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace StaffTrackApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanctionController(IGenericRepository<Sanction> genericRepository)
        : GenericController<Sanction>(genericRepository)
    {
    }
}
