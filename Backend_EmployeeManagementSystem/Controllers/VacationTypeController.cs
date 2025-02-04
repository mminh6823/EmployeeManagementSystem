using Backend_Library.Repositories.Contracts;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationTypeController(
        IGenericRepositoryInterface<VacationType> genericRepositoryInterface,
         ILogger<GenericController<VacationType>> logger) // Thêm logger
        : GenericController<VacationType>(genericRepositoryInterface, logger)
    {
    }
}
