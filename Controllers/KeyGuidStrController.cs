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
    public class KeyGuidStrController : ControllerBase
    {
        [HttpGet]
        public KeyModel Get()
        {
            Guid id = RoadFlow.Utility.GuidExtensions.NewGuid();
            return new KeyModel { Code=0,Msg=id.ToString()} ;
        }
    }
}
