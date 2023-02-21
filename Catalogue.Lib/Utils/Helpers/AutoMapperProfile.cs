namespace Catalogue.Lib.Utils.Helpers;
using AutoMapper;
using Catalogue.Lib.Models.Dto;
using Catalogue.Lib.Models.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Security.Principal;

public class AutoMapperProfile : Profile
{
    // mappings between model and entity objects
    public AutoMapperProfile()
    {
        CreateMap<FileUpload, FileUploadDto>();
        CreateMap<FileUploadDto, FileUpload>();
        CreateMap<WaterBodyPointDto, WaterBodyPoint>();
        CreateMap<WaterBodyPoint, WaterBodyPointDto>();
        CreateMap<AccountDto, Account>();
        CreateMap<Account, AccountDto>();


    }



}