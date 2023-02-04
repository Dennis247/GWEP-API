﻿using Catalogue.api.Utils.Response;
using Catalogue.Lib.Models.Dto;
using Catalogue.Lib.Services;
using Catalogue.Lib.Utils.Filters;
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

        [HttpGet("GetUploadedWaterDetections")]
        public async Task<IActionResult> GetUploadedWaterDetections()
        {
            var result = await _waterDetectionServices.GetUploadedWaterDetections();
            return Ok(result);
        }


        [HttpGet("GetWaterBodyDetails")]
        public async Task<IActionResult> GetWaterBodyDetails([FromQuery] PaginationFilter filter,string name)
        {
            var route = $"{Request.Path.Value}?name={name}";
            var result = await _waterDetectionServices.GetWaterBodyDetails(filter, name,route);
            return Ok(result);
        }


     
    }
}



//flow todo


//get files

//view paginsated contenst of the file