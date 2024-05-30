using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreContext _storeContext;

        public BuggyController(StoreContext storeContext)
        {
            this._storeContext = storeContext;
        }

        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> TestAuth()
        {
            return "sucess";
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            return NotFound();
        }

        [HttpGet("servererror")]
        public ActionResult GetSereverError()
        {
            var thing = _storeContext.Products.Find(55);
            
            var url = thing.PictureURl;

            return Ok();
        }
    }
}
