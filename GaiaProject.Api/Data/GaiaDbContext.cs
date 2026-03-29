using GaiaProject.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GaiaProject.Api.Data
{
    public class GaiaDbContext : DbContext
    {
        public GaiaDbContext(DbContextOptions<GaiaDbContext> options) : base(options)
        {
        }

        public DbSet<OperationHistory> OperationHistories { get; set; }
    }
}