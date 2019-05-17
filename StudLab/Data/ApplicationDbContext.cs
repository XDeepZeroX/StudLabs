using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using StudLab.Model;
using StudLab.Models.TablesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudLab.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TableTransportTask> TransportTables { get; set; }
        public DbSet<MultiCriteriaTask> MultiCriteriaTables { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Unit>().HasQueryFilter(p => !p.IsDeleted);

            //modelBuilder.Entity<TableEntity>(entity =>
            //{
            //    entity.HasOne(d => d.Transport)
            //        .WithMany(p => p.TableEntities)
            //        .HasForeignKey(d => d.TransportId);
            //});
        }
    }
}
