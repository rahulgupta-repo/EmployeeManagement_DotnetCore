using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
               new Employee
               {
                   Id = 1,
                   Name = "Sonu",
                   Department = Departments.IT,
                   Email = "IT@mail.com"
               },
               new Employee
               {
                   Id = 2,
                   Name = "Ramu",
                   Department = Departments.HR,
                   Email = "hr@mail.com"
               }
           );
        }
    }
}
