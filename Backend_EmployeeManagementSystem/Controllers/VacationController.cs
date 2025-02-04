using Backend_Library.Repositories.Contracts;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationController(
        IGenericRepositoryInterface<Vacation> genericRepositoryInterface,
         ILogger<GenericController<Vacation>> logger) // Thêm logger
        : GenericController<Vacation>(genericRepositoryInterface, logger)
    {
    }
}
