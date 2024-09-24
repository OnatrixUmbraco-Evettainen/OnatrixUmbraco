using Microsoft.AspNetCore.Mvc;

namespace OnatrixUmbraco.Controllers
{
    public class TempDataController : Controller
    {
        [HttpPost]
        public IActionResult ClearTempData()
        {
            TempData.Clear();
            return Ok();
        }
    }
}
