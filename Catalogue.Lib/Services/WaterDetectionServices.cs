using AutoMapper;
using Catalogue.api.Utils.Response;
using Catalogue.Lib.Data;
using Catalogue.Lib.Models.Dto;
using Catalogue.Lib.Models.Entities;
using Catalogue.Lib.Utils.Filters;
using Catalogue.Lib.Utils.Helpers;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue.Lib.Services
{
    public interface IWaterDetectionServices
    {
        public   Task<Response<string>> ImportWaterBodyDataAsync(IFormFile formFile);

        public Task<Response<IEnumerable<FileUploadDto>>> GetUploadedWaterDetections();

        public  Task<PagedResponse<WaterBodyData>> GetWaterBodyDetails([FromQuery] PaginationFilter filter,string name, string route);

        public  Task<Response<WaterBodyData>> GetWaterBodyDataByName(string name);

        public  Task<Response<string>> UpdateWaterBodyVisitation(UpdateWaterBodyVisitation updateWaterBodyVisitation);

        public Task<Response<string>> UpdateWaterBodyPresence(UpdateWaterBodyPresence updateWaterBodyPresence);

    }
    public class WaterDetectionServices : IWaterDetectionServices
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;


        public WaterDetectionServices(ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IUriService uriService)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _uriService = uriService;   
        }

        public async Task<Response<IEnumerable<FileUploadDto>>> GetUploadedWaterDetections()
        {
            List<FileUpload> waterDetections = _applicationDbContext.FileUploads.ToList();
            var dataToReturn = _mapper.Map<IEnumerable<FileUploadDto>>(waterDetections);
            return new Response<IEnumerable<FileUploadDto>>
            {
                Succeeded = true,
                Message = "sucessful",
                Data = dataToReturn
            };
        }

        public async Task<Response<string>> ImportWaterBodyDataAsync(IFormFile formFile)
        {

            if (formFile == null || formFile.Length <= 0)
            {
                throw new AppException("File cannot be null or empty");
            }

            string fileExtension = Path.GetExtension(formFile.FileName);
            if (!fileExtension.Contains("json"))
            {
                throw new AppException("File format must be in json format");
            }

            var folderName = Path.Combine("AppUploads", "WaterBodyData");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }
            var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');

           

            var fullPath = Path.Combine(pathToSave, fileName);
            var filePath = Path.Combine(folderName, fileName);
            filePath = filePath.Replace("\\", "//");

            using (var stream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                formFile.CopyTo(stream);
            }
            FileUpload fileUpload = new FileUpload
            {
                filePath = filePath,
                fileName = fileName,
                Name = ""
                
            };
            _applicationDbContext.FileUploads.Add(fileUpload);
            _applicationDbContext.SaveChanges();

            string json = File.ReadAllText(fullPath);
            var data = JsonConvert.DeserializeObject<WaterBodyData>(json);
            fileUpload.Name = data!.name;
            _applicationDbContext.FileUploads.Update(fileUpload);
                if (data != null)
                {
                    WaterBodyDetectionData waterBodyDetectionData = new WaterBodyDetectionData();
                    List<WaterBodyDetectionData> dataToSave = new List<WaterBodyDetectionData>();
                    var sync = new object();
                    Parallel.ForEach(data.features.AsEnumerable(), item =>
                    {
                        lock (sync)
                        {
                            waterBodyDetectionData = new WaterBodyDetectionData
                            {
                                Created = DateTime.Now,
                                crs = JsonConvert.SerializeObject(data.crs),
                                name = data.name,
                                type = data.type,
                                featureGometry = JsonConvert.SerializeObject(item.geometry),
                                featureProperties = JsonConvert.SerializeObject(item.properties),
                                featureType = JsonConvert.SerializeObject(item.type),
                                fileId = fileUpload.Id,
                                HasBeenVisited= false,
                                IsWaterBodyPresent= false,
                            };
                            dataToSave.Add(waterBodyDetectionData);
                        }
                    });

            //        await _applicationDbContext.BulkInsertAsync(dataToSave);
                  await  _applicationDbContext.AddRangeAsync(dataToSave);
                    _applicationDbContext.SaveChanges();

                    return new Response<string>
                    {
                        Data = "",
                        Message = "Data Upload Sucessful",
                        Succeeded = true
                    };
                }

       



            return new Response<string>
                    {
                        Data = "",
                        Message = "Failed to upload empty data",
                        Succeeded = false
                    };
                
            

        }
   
        public async Task<PagedResponse<WaterBodyData>> GetWaterBodyDetails([FromQuery] PaginationFilter filter,string name, string route)
        {
            filter.sortBy = string.IsNullOrEmpty(filter.sortBy) ? "Name" : filter.sortBy;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            int totalRecords = 0;

            WaterBodyData pagedData = new WaterBodyData();

            var result = _applicationDbContext.WaterBodyDetectionDatas.Where(x => x.name.ToLower().Contains(name.ToLower()))
                 .Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToList();
            totalRecords = _applicationDbContext.WaterBodyDetectionDatas.Count();

            if(result.Count > 0 )
            {
                pagedData = new WaterBodyData
                {
                    type = result[0].type,
                    crs = JsonConvert.DeserializeObject<Crs>(result[0].crs)!,
                    name = result[0].name,
                    features = result.Select(x => new Feature
                    {
                        type = x.type,
                        WaterBodyId =x.Id,
                        geometry = JsonConvert.DeserializeObject<Geometry>(x.featureGometry)!,
                        properties = JsonConvert.DeserializeObject<Properties>(x.featureProperties)!
                    }).ToList()

                };

                var totalPages = totalRecords / (double)validFilter.PageSize;
                int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            }

            var pagedReponse = PaginationHelper.CreatePagedReponse2(pagedData, validFilter, totalRecords, _uriService, route);

            return pagedReponse;


        }

        public async Task<Response<WaterBodyData>> GetWaterBodyDataByName(string name)
        {

            WaterBodyData waterBodyData = new WaterBodyData();
            var result = _applicationDbContext.WaterBodyDetectionDatas.Where(x => x.name.ToLower().Contains(name.ToLower())).Take(100);
            if (result.Count() > 0)
            {
                var fd = result.FirstOrDefault()!;
                waterBodyData = new WaterBodyData
                {
                    type = fd.type,
                    crs = JsonConvert.DeserializeObject<Crs>(fd.crs)!,
                    name = fd.name,
                    features = result.Select(x => new Feature
                    {
                        WaterBodyId = x.Id,
                        type = x.type,
                        geometry = JsonConvert.DeserializeObject<Geometry>(x.featureGometry)!,
                        properties = JsonConvert.DeserializeObject<Properties>(x.featureProperties)!
                    }).ToList()

                };
            }

            return new Response<WaterBodyData>
            {
                Data = waterBodyData,
                Message = "Sucessful",
                Succeeded = true
            };

        }

        public async Task<Response<string>> UpdateWaterBodyVisitation(UpdateWaterBodyVisitation updateWaterBodyVisitation)
        {
            var data = _applicationDbContext.WaterBodyDetectionDatas.
                Where(x=>x.featureProperties.Contains(updateWaterBodyVisitation.Id)).FirstOrDefault();

            if(data == null)
            {
                throw new Exception("Water body data not valid");
            }
            data.HasBeenVisited = updateWaterBodyVisitation.IsVisisted;
            _applicationDbContext.Update(data);
            _applicationDbContext.SaveChanges();

            return new Response<string>
            {
                Data = "",
                Succeeded = true,
                Message = "Water body update sucessfull"
            };
        }

        public async Task<Response<string>> UpdateWaterBodyPresence(UpdateWaterBodyPresence updateWaterBodyPresence)
        {
            var data = _applicationDbContext.WaterBodyDetectionDatas.
                Where(x => x.featureProperties.Contains(updateWaterBodyPresence.Id)).FirstOrDefault();


            if (data == null)
            {
                throw new Exception("Water body data not valid");
            }
            data.IsWaterBodyPresent = updateWaterBodyPresence.IsWaterBodyPresent;
            _applicationDbContext.Update(data);
            _applicationDbContext.SaveChanges();

            return new Response<string>
            {
                Data = "",
                Succeeded = true,
                Message = "Water body update sucessfull"
            };
        }
    }
}

