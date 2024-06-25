using Base.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serverlibrary.Repository.Contact;

namespace SERVER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController(IGenaricReopInterface<City> genaricReopInterface):GenaricController<City>(genaricReopInterface)
    {
    }
}
