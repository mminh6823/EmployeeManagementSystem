using Backend_Library.Repositories.Contracts;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController(
       IGenericRepositoryInterface<City> genericRepositoryInterface,
         ILogger<GenericController<City>> logger) // Thêm logger
        : GenericController<City>(genericRepositoryInterface, logger)
    {
    }
}
