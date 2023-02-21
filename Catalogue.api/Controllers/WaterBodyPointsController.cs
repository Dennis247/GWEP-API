using Catalogue.api.Utils.Response;
using Catalogue.Lib.Models.Dto;
using Catalogue.Lib.Services;
using Catalogue.Lib.Utils.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalogue.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaterPointController : ControllerBase
    {
        private readonly IWaterBodyPointServices _waterPointServices;
        public WaterPointController(IWaterBodyPointServices waterPointServices)
        {
            _waterPointServices = waterPointServices;
        }


        [HttpPost("UploadWaterPointData")]
        public async Task<IActionResult> UploadWaterPointData(IFormFile formFile)
        {
            var result = await _waterPointServices.ImportWaterBodyPointDataAsync(formFile);
            return Ok(result);
        }




        [HttpGet("GetWaterBodyPointsByPhase")]
        public async Task<IActionResult> GetWaterBodyPointsByPhase(string phase)
        {
            var result = await _waterPointServices.GetWaterBodyPointsByPhase(phase);
            return Ok(result);
        }


        [HttpGet("GetAllWaterPoints")]
        public async Task<IActionResult> GetAllWaterPoints()
        {
            var result = await _waterPointServices.GetAllWaterPoints();
            return Ok(result);
        }


        [HttpGet("GetWaterPointsPhasesName")]
        public async Task<IActionResult> GetWaterPointsPhases()
        {
            var result =  _waterPointServices.GetWaterPointsPhases();
            return Ok(result);
        }

        [HttpGet("GetPaaginatedWaterBodyPoints")]
        public  IActionResult GetPaaginatedWaterBodyPoints([FromQuery] PaginationFilter filter, string phase)
        {
            var route = $"{Request.Path.Value}?name={phase}";
            var result =  _waterPointServices.GetPaaginatedWaterBodyPoints(filter, phase, route);
            return Ok(result);
        }



        [HttpPost("UpdateWaterBodyVisitation")]
        public async Task<IActionResult> UpdateWaterBodyVisitation(UpdateWaterBodyVisitation updateWaterBodyVisitation)
        {
          

            var result = await _waterPointServices.UpdateWaterBodyVisitation(updateWaterBodyVisitation);
            return Ok(result);
        }


        [HttpPost("UpdateWaterBodyPresence")]
        public async Task<IActionResult> UpdateWaterBodyPresence(UpdateWaterBodyPresence updateWaterBodyPresence)
        {
            var result = await _waterPointServices.UpdateWaterBodyPresence(updateWaterBodyPresence);
            return Ok(result);
        }

        [HttpPost("UpdateWaterBodyDepression")]
        public async Task<IActionResult> UpdateWaterBodyDepression(UpdateWaterBodyDepression updateWaterBodyDepression)
        {
            var result = await _waterPointServices.UpdateWaterBodyDepression(updateWaterBodyDepression);
            return Ok(result);
        }


    }
}

