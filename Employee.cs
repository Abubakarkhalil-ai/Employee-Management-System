using System;

namespace EmployeeManagementSystem
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }

        public Employee(int id, string fullName, string department, string role)
        {
            Id = id;
            FullName = fullName;
            Department = department;
            Role = role;
        }
    }
}
