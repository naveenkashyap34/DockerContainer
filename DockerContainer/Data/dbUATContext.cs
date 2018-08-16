using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DockerContainer.Data
{
    public partial class dbUATContext : DbContext
    {
        public dbUATContext()
        {
        }

        public dbUATContext(DbContextOptions<dbUATContext> options)
            : base(options)
        {
        }
        public virtual DbSet<TblUser> TblUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var host = Environment.GetEnvironmentVariable("DBHOST") ?? "DESKTOP-K9SUDVU\\SQLEXPRESS";
                var database = Environment.GetEnvironmentVariable("DB") ?? "dbUAT";
                var password = Environment.GetEnvironmentVariable("DBPASSWORD") ?? "naveen@11";
                var userid = Environment.GetEnvironmentVariable("DBUSERID") ?? "sa";
                var connection = @"Server=" + host + ";Database=" + database + ";user id=" + userid + "; Trusted_Connection=True;ConnectRetryCount=0";
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.ToTable("tblUser");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);
            });
        }
    }
}
