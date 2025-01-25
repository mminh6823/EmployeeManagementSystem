using Backend_Library.Repositories.Contracts;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController(
        IGenericRepositoryInterface<Branch> genericRepositoryInterface,
         ILogger<GenericController<Branch>> logger) // Thêm logger
        : GenericController<Branch>(genericRepositoryInterface, logger)
    {
    }
}
