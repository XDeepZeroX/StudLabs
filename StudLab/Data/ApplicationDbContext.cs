using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using StudLab.Model;
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

        public DbSet<TableTransportTask> Tables { get; set; }
        public DbSet<User> Transports { get; set; }

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
