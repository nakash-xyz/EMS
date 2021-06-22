using API.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("tblEmployees");
            modelBuilder.Entity<Employee>().Property(e => e.FirstName).HasColumnType("VARCHAR(100)").IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.LastName).HasColumnType("VARCHAR(100)").IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.Designation).HasColumnType("VARCHAR(100)");
            modelBuilder.Entity<Employee>().Property(e => e.Department).HasColumnType("VARCHAR(50)");
            
            base.OnModelCreating(modelBuilder);
        }
    }
}