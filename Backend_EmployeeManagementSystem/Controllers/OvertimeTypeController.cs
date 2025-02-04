using Backend_Library.Repositories.Contracts;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OvertimeTypeController(
        IGenericRepositoryInterface<OvertimeType> genericRepositoryInterface,
         ILogger<GenericController<OvertimeType>> logger) // Thêm logger
        : GenericController<OvertimeType>(genericRepositoryInterface, logger)
    {
    }
}
