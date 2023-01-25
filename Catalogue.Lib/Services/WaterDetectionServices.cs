using Catalogue.api.Utils.Response;
using Catalogue.Lib.Data;
using Catalogue.Lib.Models.Dto;
using Catalogue.Lib.Models.Entities;
using Catalogue.Lib.Utils.Helpers;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

    }
    public class WaterDetectionServices : IWaterDetectionServices
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public WaterDetectionServices(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
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
                filePath = filePath
            };
            _applicationDbContext.FileUploads.Add(fileUpload);
            _applicationDbContext.SaveChanges();

            string json = File.ReadAllText(fullPath);
                var data = JsonConvert.DeserializeObject<WaterBodyData>(json);
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
                                fileId = fileUpload.Id
                            };
                            dataToSave.Add(waterBodyDetectionData);
                        }
                    });

                    await _applicationDbContext.BulkInsertAsync(dataToSave);
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
    }
}
