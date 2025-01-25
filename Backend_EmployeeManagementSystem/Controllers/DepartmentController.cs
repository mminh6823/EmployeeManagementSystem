using Backend_Library.Repositories.Contracts;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController(
        IGenericRepositoryInterface<Department> genericRepositoryInterface,
         ILogger<GenericController<Department>> logger) // Thêm logger
        : GenericController<Department>(genericRepositoryInterface, logger)
    {
    }
}
