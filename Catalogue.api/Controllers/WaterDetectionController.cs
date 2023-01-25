using Catalogue.Lib.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalogue.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaterDetectionController : ControllerBase
    {
        private readonly IWaterDetectionServices _waterDetectionServices;
        public WaterDetectionController(IWaterDetectionServices waterDetectionServices)
        {
            _waterDetectionServices = waterDetectionServices;
        }


        [HttpPost("UploadWaterDetectionData")]
        public async Task<IActionResult> UploadWaterDetectionData(IFormFile formFile)
        {
            var result = await _waterDetectionServices.ImportWaterBodyDataAsync(formFile);
            return Ok(result);
        }
    }
}



//flow todo


//get files

//view paginsated contenst of the file