using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace StaffTrackApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController(IGenericRepository<Doctor> genericRepository)
        : GenericController<Doctor>(genericRepository)
    {
    }
}
