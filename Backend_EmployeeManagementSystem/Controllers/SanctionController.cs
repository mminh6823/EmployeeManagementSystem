using Backend_Library.Repositories.Contracts;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanctionController(
        IGenericRepositoryInterface<Sanction> genericRepositoryInterface,
         ILogger<GenericController<Sanction>> logger) // Thêm logger
        : GenericController<Sanction>(genericRepositoryInterface, logger)
    {
    }
}
