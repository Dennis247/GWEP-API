using Catalogue.Lib.Models.Dto;
using Catalogue.Lib.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalogue.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSyncController : ControllerBase
    {

        private readonly IDataSyncService _dataSyncService;
        public DataSyncController(IDataSyncService dataSyncService)
        {
            _dataSyncService = dataSyncService;
        }


        [HttpPost("SyncData")]
        public async Task<IActionResult> SyncData(List<DataSyncDto> waterBodyData)
        {
            var result =  _dataSyncService.SyncWaterBodyData(waterBodyData);
            return Ok(result);
        }
    }
}
