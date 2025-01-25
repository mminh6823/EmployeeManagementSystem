using Backend_Library.Repositories.Contracts;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backend_EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralDepartmentController(
        IGenericRepositoryInterface<GeneralDepartment> genericRepositoryInterface,
        ILogger<GenericController<GeneralDepartment>> logger // Thêm logger
    ) : GenericController<GeneralDepartment>(genericRepositoryInterface, logger) // Truyền logger tới lớp cha
    {
    }
}
