using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PrimaryKey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyLongStrController : ControllerBase
    {
        [HttpGet]
        public KeyModel Get()
        {
            long id= RoadFlow.Utility.IdGeneratorHelper.Instance.GetId();
            return new KeyModel { Code = 0, Msg = id.ToString() };
        }
    }
}
