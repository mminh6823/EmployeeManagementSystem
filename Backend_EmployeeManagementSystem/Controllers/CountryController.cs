using Backend_Library.Repositories.Contracts;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController(
          IGenericRepositoryInterface<Country> genericRepositoryInterface,
         ILogger<GenericController<Country>> logger) // Thêm logger
        : GenericController<Country>(genericRepositoryInterface, logger)
    {
    }
}
