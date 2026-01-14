using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TechApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {

        public ActionResult SaveInventoryData() {             // Placeholder for saving inventory data logic


            return Ok("Inventory data saved successfully.");
        }
    }
}
