﻿using AutoMapper;
using Catalogue.api.Utils.Response;
using Catalogue.Lib.Data;
using Catalogue.Lib.Models.Dto;
using Catalogue.Lib.Models.Entities;
using Catalogue.Lib.Utils.Filters;
using Catalogue.Lib.Utils.Helpers;
using CsvHelper;
using CsvHelper.Configuration;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.ConstrainedExecution;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Catalogue.Lib.Services
{
    public interface IWaterBodyPointServices
    {
        PagedResponse<List<WaterBodyPointDto>> GetPaaginatedWaterBodyPoints([FromQuery] PaginationFilter filter, string phase, string route);
        Task<Response<IEnumerable<FileUploadDto>>> GetUploadedWaterBodyPoints();
        Task<Response<List<WaterBodyPointDto>>> GetWaterBodyPointsByPhase(string phase);
        Task<Response<string>> ImportWaterBodyPointDataAsync(IFormFile formFile);
        Task<Response<string>> UpdateWaterBodyPresence(UpdateWaterBodyPresence updateWaterBodyPresence);
        Task<Response<string>> UpdateWaterBodyVisitation(UpdateWaterBodyVisitation updateWaterBodyVisitation);

        Task<Response<string>> UpdateWaterBodyDepression(UpdateWaterBodyDepression updateWaterBodyDepression);

        public Response<List<string>> GetWaterPointsPhases();

        Task<Response<IEnumerable<WaterBodyPointDto>>> GetAllWaterPoints();
    }

    public class WaterBodyPointServices : IWaterBodyPointServices
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public WaterBodyPointServices(ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IUriService uriService)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _uriService = uriService;

        }

        public async Task<Response<IEnumerable<WaterBodyPointDto>>> GetAllWaterPoints()
        {
            var allWaterPoints = _applicationDbContext.WaterBodyPoints.Take(1000).AsEnumerable();
            var dataToReturn = _mapper.Map<IEnumerable<WaterBodyPointDto>>(allWaterPoints);

            return new Response<IEnumerable<WaterBodyPointDto>>
            {
                Data = dataToReturn,
                Message = "Sucessful",
                Succeeded = true

            };
        }

        public PagedResponse<List<WaterBodyPointDto>> GetPaaginatedWaterBodyPoints([FromQuery] PaginationFilter filter, string phase, string route)
        {

            filter.sortBy = string.IsNullOrEmpty(filter.sortBy) ? "Name" : filter.sortBy;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            int totalRecords = 0;

            var result = _applicationDbContext.WaterBodyPoints.Where(x => x.PHASE.ToLower().Contains(phase.ToLower()))
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToList();
            totalRecords = _applicationDbContext.WaterBodyDetectionDatas.Count();

            var dataToReturn = _mapper.Map<List<WaterBodyPointDto>>(result);


            var pagedReponse = PaginationHelper.CreatePagedReponse(dataToReturn, validFilter, totalRecords, _uriService, route);

            return pagedReponse;
        }

        public async Task<Response<IEnumerable<FileUploadDto>>> GetUploadedWaterBodyPoints()
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

        public async Task<Response<List<WaterBodyPointDto>>> GetWaterBodyPointsByPhase(string phase)
        {
            WaterBodyData waterBodyData = new WaterBodyData();
            var result = _applicationDbContext.WaterBodyPoints.Where(x => x.PHASE.ToLower().Contains(phase.ToLower()));
            var dataToReturn = _mapper.Map<List<WaterBodyPointDto>>(result);

            return new Response<List<WaterBodyPointDto>>
            {
                Data = dataToReturn,
                Message = "Sucessful",
                Succeeded = true
            };
        }

        public Response<List<string>> GetWaterPointsPhases()
        {
            var result = _applicationDbContext.WaterBodyPoints.GroupBy(x => x.PHASE).Select(x => x.Key).ToList();

            return new Response<List<string>>
            {
                Data = result,
                Message = "Sucessful",
                Succeeded = true
            };

        }
        public async Task<Response<string>> ImportWaterBodyPointDataAsync(IFormFile formFile)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                throw new AppException("File cannot be null or empty");
            }
            string fileExtension = Path.GetExtension(formFile.FileName);

            if (!fileExtension.Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
                throw new AppException("File  format must be csv");
            }

            //check if file has already been uploaded 
            var existingFIle = _applicationDbContext.FileUploads.FirstOrDefault(x => x.fileName == formFile.FileName);
            if (existingFIle != null)
            {

                throw new AppException("This file has already been uploaded");
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

            if (File.Exists(fullPath))
            {
                try
                {
                    File.Delete(fullPath);
                }
                catch (Exception)
                {


                }

            }


            using (var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                formFile.CopyTo(stream);
                stream.Dispose();
            }

            FileUpload fileUpload = new FileUpload
            {
                filePath = filePath,
                fileName = fileName,
                Name = fileName

            };
            _applicationDbContext.FileUploads.Add(fileUpload);
            _applicationDbContext.SaveChanges();


            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null,
                PrepareHeaderForMatch = args => args.Header.ToLower().Replace(" ", ""),
            };



            using (var reader2 = new StreamReader(fullPath))
            using (var csv = new CsvReader(reader2, config))
            {

                var waterBodyImportDtos = csv.GetRecords<WaterBodyImportDto>().ToList();
                List<WaterBodyPoint> waterBodyPointsForImport = new List<WaterBodyPoint>();
                WaterBodyPoint waterBodyPoint = new WaterBodyPoint();

                var sync = new object();
                Parallel.ForEach(waterBodyImportDtos.AsEnumerable(), item =>
                {
                    lock (sync)
                    {
                        waterBodyPoint = new WaterBodyPoint
                        {

                            AREA_SQM = item.AREA_SQM,
                            CONFIDENCE = item.CONFIDENCE,
                            Created = DateTime.Now,
                            HasBeenVisited = false,
                            IsWaterBodyPresent = false,
                            LATITUDE = item.LATITUDE,
                            LONGITUDE = item.LONGITUDE,
                            OBJECTID = item.OBJECTID,
                            PHASE = item.PHASE,
                            SHAPE_Area = item.SHAPE_Area,
                            SHAPE_Leng = item.SHAPE_Leng,
                            UNIQUE_ID = item.UNIQUE_ID,
                            IsAbateKnownPoint = false,
                        };
                        waterBodyPointsForImport.Add(waterBodyPoint);
                    }
                });

                _applicationDbContext.AddRange(waterBodyPointsForImport);
                _applicationDbContext.SaveChanges();


                return new Response<string>
                {
                    Data = "",
                    Message = "Data Upload Sucessful",
                    Succeeded = true
                };



            }



        }

        public async Task<Response<string>> UpdateWaterBodyDepression(UpdateWaterBodyDepression updateWaterBodyDepression)
        {
            Account? user = _applicationDbContext.Accounts.FirstOrDefault(x => x.Id == updateWaterBodyDepression.AccountId);
            if (user == null)
            {
                throw new AppException("User not valid");
            }

            var data = _applicationDbContext.WaterBodyPoints.
                 FirstOrDefault(x => x.Id == updateWaterBodyDepression.WaterBodyId);

            if (data == null)
            {
                throw new AppException("Water body data not valid");
            }

            //if(updateWaterBodyDepression.Depression == Constants.wetDepressionType 
            //    || updateWaterBodyDepression.Depression == Constants.dryDepressionType)
            //{
                data.Depression = updateWaterBodyDepression.Depression;

                data.LastUpdatedByName = user.FullName;
                data.LastUpdatedBy = user.Id;
                data.LastUpdatedDate = DateTime.UtcNow;


                _applicationDbContext.Update(data);
                _applicationDbContext.SaveChanges();

                return new Response<string>
                {
                    Data = "",
                    Succeeded = true,
                    Message = "Water body update sucessfull"
                };
           // }
          //  throw new AppException("Depression type not valid");

     
        }

        public async Task<Response<string>> UpdateWaterBodyPresence(UpdateWaterBodyPresence updateWaterBodyPresence)
        {
            Account? user = _applicationDbContext.Accounts.FirstOrDefault(x => x.Id == updateWaterBodyPresence.AccountId);
            if (user == null)
            {
                throw new Exception("User not valid");
            }

            var data = _applicationDbContext.WaterBodyPoints.
                 FirstOrDefault(x => x.Id == updateWaterBodyPresence.WaterBodyId);

            if (data == null)
            {
                throw new Exception("Water body data not valid");
            }
            data.IsWaterBodyPresent = updateWaterBodyPresence.IsWaterBodyPresent;

            data.LastUpdatedByName = user.FullName;
            data.LastUpdatedBy = user.Id;
            data.LastUpdatedDate = DateTime.UtcNow;
          

            _applicationDbContext.Update(data);
            _applicationDbContext.SaveChanges();

            return new Response<string>
            {
                Data = "",
                Succeeded = true,
                Message = "Water body update sucessfull"
            };
        }

        public async Task<Response<string>> UpdateWaterBodyVisitation(UpdateWaterBodyVisitation updateWaterBodyVisitation)
        {

            Account user = _applicationDbContext.Accounts.FirstOrDefault(x => x.Id == updateWaterBodyVisitation.AccountId);
            if(user == null)
            {
                throw new Exception("User not valid");
            }


            var data = _applicationDbContext.WaterBodyPoints.
                FirstOrDefault(x => x.Id == updateWaterBodyVisitation.WaterBodyId);

            if (data == null)
            {
                throw new Exception("Water body data not valid");
            }
            data.HasBeenVisited = updateWaterBodyVisitation.IsVisisted;
          
            data.LastUpdatedByName = user.FullName;
            data.LastUpdatedBy = user.Id;
            data.LastUpdatedDate = DateTime.UtcNow;
            data.LastVisistedBy = user.FullName;
            data.LastTimeVisisted= DateTime.UtcNow;


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

