using System;
using System.Collections.Generic;

namespace EmployeeManagementSystem
{
    /// <summary>
    /// Represents an employee in the management system.
    /// Stores personal details and associated leave records.
    /// </summary>
    public class Employee
    {
        // Unique identifier for the employee
        public int Id { get; set; }

        // Full name of the employee
        public string FullName { get; set; }

        // Department the employee belongs to
        public string Department { get; set; }

        // Job role of the employee
        public string Role { get; set; }

        // List of leave records associated with this employee
        public List<LeaveRecord> Leaves { get; set; }

        /// <summary>
        /// Constructor to create a new Employee with the given details.
        /// Initialises an empty list of leave records.
        /// </summary>
        public Employee(int id, string fullName, string department, string role)
        {
            Id = id;
            FullName = fullName;
            Department = department;
            Role = role;
            Leaves = new List<LeaveRecord>();
        }

        /// <summary>
        /// Returns a formatted string representation of the employee.
        /// </summary>
        public override string ToString()
        {
            return $"ID: {Id}, Name: {FullName}, Department: {Department}, Role: {Role}";
        }
    }
}
