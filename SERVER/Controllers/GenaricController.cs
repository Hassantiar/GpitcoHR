using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serverlibrary.Repository.Contact;

namespace SERVER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenaricController<T>(IGenaricReopInterface<T>genaricReopInterface) : Controller where T : class
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()=>Ok(await genaricReopInterface.GetAll());
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("invalid requst");
            return Ok(await genaricReopInterface.DeleteById(id));
        }
        [HttpGet("Single/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest("invalied requst");
            return Ok(await genaricReopInterface.GetById(id));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(T model)
        {
            if (model is null) return BadRequest("bad requst ");
            return Ok(await genaricReopInterface.Insert(model));
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(T model)
        {
            if (model is null) return BadRequest("bad request");
            return Ok(await genaricReopInterface.Update(model));
        }
    }
}
