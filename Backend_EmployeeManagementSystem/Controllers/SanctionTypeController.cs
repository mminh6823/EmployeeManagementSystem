using Backend_Library.Repositories.Contracts;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanctionTypeController(
        IGenericRepositoryInterface<SanctionType> genericRepositoryInterface,
         ILogger<GenericController<SanctionType>> logger) // Thêm logger
        : GenericController<SanctionType>(genericRepositoryInterface, logger)
    {
    }
}
