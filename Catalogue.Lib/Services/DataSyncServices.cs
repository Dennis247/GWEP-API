using AutoMapper;
using Catalogue.api.Utils.Response;
using Catalogue.Lib.Data;
using Catalogue.Lib.Models.Dto;
using Catalogue.Lib.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue.Lib.Services
{
    public interface IDataSyncService
    {
        //push sync
        public Response<bool> SyncWaterBodyData(List<DataSyncDto> waterBodyData);

    }


    public class DataSyncServices : IDataSyncService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public DataSyncServices(ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }
        public Response<bool> SyncWaterBodyData(List<DataSyncDto> waterBodyData)
        {
            if(waterBodyData.Any())
            {
                var waterBodyIds = waterBodyData.Select(x => x.Id);

                //upated list for sync
                List<WaterBodyDetectionData> waterDetectionDataFOrUpdate = new List<WaterBodyDetectionData>();

                //water body for update
                var waterBodyFromDB = _applicationDbContext.WaterBodyDetectionDatas.Where(x => (waterBodyIds.Contains(x.Id)));

                foreach (var item in waterBodyData)
                {
                    var wdata = waterBodyFromDB.FirstOrDefault(x => x.Id == item.Id);
                    if (wdata != null)
                    {
                        if (item.Date > wdata.LastTimeUpdated)
                        {
                            wdata.LastTimeUpdated = item.Date;
                            wdata.IsWaterBodyPresent = item.IsWaterBodyPresent;
                            wdata.HasBeenVisited = item.HasBeenVisited;
                            wdata.LastUpdatedBy = item.LastUpdatedBy;
                        }
                        waterDetectionDataFOrUpdate.Add(wdata);
                    }
                }

                _applicationDbContext.UpdateRange(waterDetectionDataFOrUpdate);
                _applicationDbContext.SaveChanges();
                return new Response<bool>
                {
                    Data = true,
                    Message = "Sync Sucessfull",
                    Succeeded = true

                };

            }
            return new Response<bool>
            {
                Data = true,
                Message = "Sync Sucessfull",
                Succeeded = true

            };


        }
    }
}
