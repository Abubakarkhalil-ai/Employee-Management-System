using System;
using System.Collections.Generic;

namespace EmployeeManagementSystem
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
        public List<LeaveRecord> Leaves { get; set; }

        public Employee(int id, string fullName, string department, string role)
        {
            Id = id;
            FullName = fullName;
            Department = department;
            Role = role;
            Leaves = new List<LeaveRecord>();
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {FullName}, Department: {Department}, Role: {Role}";
        }
    }
}
