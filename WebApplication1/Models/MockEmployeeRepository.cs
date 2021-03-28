using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _empList;

        public MockEmployeeRepository()
        {
            _empList = new List<Employee>()
        {
            new Employee() { Id = 1, Name = "Rahul", Email = "1@emp.com", Department = Departments.HR },
            new Employee() { Id = 2, Name = "Manish", Email = "2@emp.com", Department = Departments.IT },
            new Employee() { Id = 3, Name = "Rina", Email = "3@emp.com", Department = Departments.Payroll },
            new Employee() { Id = 4, Name = "Karina", Email = "4@emp.com", Department = Departments.Representative }
        };
        }

        public Employee AddEmployee(Employee employee)
        {
            employee.Id = _empList.Max(e => e.Id) + 1;
            _empList.Add(employee);
            return employee;
        }

        public Employee DeleteEmployee(int id)
        {
            Employee employee = _empList.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _empList.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _empList;
        }

        public Employee GetEmployee(int id)
        {
            return _empList.FirstOrDefault(e => e.Id == id);
        }

        public Employee UpdateEmployee(Employee employeeChanges)
        {

            Employee employee = _empList.FirstOrDefault(e => e.Id == employeeChanges.Id);
            if (employee != null)
            {
                _empList.Remove(employee);
                employee.Id = employeeChanges.Id;
                employee.Name = employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;
                _empList.Add( employee);
            }
            return employee;
        }
    }
}