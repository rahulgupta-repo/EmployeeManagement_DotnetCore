using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        /*
         
            To Use SQL Server we first have to create Migration in order to do so
            open view -> other windows package manger console.... this will open console where we ca use following commands-
            to get entitycore framework help -> get-help about_entityframeworkcore
            Get-Help add-migration
            Add-Migration <Give name of migration> -> to create table from DBset mentioned above
            Update-Database-> creates database in sql server may also give migration name else lates is applied
        */
        //To Seed Initial data while creating Migration

        protected  override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelBuilderExtensions.Seed(modelBuilder); //Seed Dta Migration example
        }
    }
}