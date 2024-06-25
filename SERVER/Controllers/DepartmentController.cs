﻿using Base.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serverlibrary.Repository.Contact;

namespace SERVER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController(IGenaricReopInterface<Departrment> genaricReopInterface):GenaricController<Departrment>(genaricReopInterface)
    { 
    }
}