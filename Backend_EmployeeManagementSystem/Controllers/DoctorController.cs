using Backend_Library.Repositories.Contracts;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController(
        IGenericRepositoryInterface<Doctor> genericRepositoryInterface,
         ILogger<GenericController<Doctor>> logger) // Thêm logger
        : GenericController<Doctor>(genericRepositoryInterface, logger)
    {
    }
}
