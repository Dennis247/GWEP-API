using Catalogue.Lib.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Catalogue.Lib.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IHttpContextAccessor _context;
    public IDbConnection connection;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor context, IConfiguration configuration) : base(options)
    {
        _context = context;
        connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));


    }
    public DbSet<WaterBodyDetectionData> WaterBodyDetectionDatas { get; set; }
    public DbSet<FileUpload> FileUploads { get; set; }
    public DbSet<DataSync> DataSyncs { get; set; }
    public DbSet<WaterBodyPoint> WaterBodyPoints { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //    new DbInitializer(modelBuilder).Seed();
    }



}
