using System;
using System.IO;
using System.Collections.Generic;

namespace EmployeeManagementSystem
{
    class Program
    {
        static EmployeeBST employeeTree = new EmployeeBST();

        static void Main(string[] args)
        {
            Console.WriteLine(" Employee Management System");

            while (true)
            {
                ShowMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddEmployee();
                        break;
                    case "2":
                        SearchEmployeeById();
                        break;
                    case "3":
                        SearchEmployeeByName();
                        break;
                    case "4":
                        AddLeave();
                        break;
                    case "5":
                        ViewEmployeeLeaves();
                        break;
                    case "6":
                        Console.WriteLine("Display All - Coming soon");
                        break;
                    case "7":
                        Console.WriteLine("Load from File - Coming soon");
                        break;
                    case "8":
                        Console.WriteLine("Save to File - Coming soon");
                        break;
                    case "9":
                        Console.WriteLine("Exiting program...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. Search Employee by ID");
            Console.WriteLine("3. Search Employee by Name");
            Console.WriteLine("4. Add Leave to Employee");
            Console.WriteLine("5. View Employee Leaves");
            Console.WriteLine("6. Display All Employees");
            Console.WriteLine("7. Load Employees from File");
            Console.WriteLine("8. Save Employees to File");
            Console.WriteLine("9. Exit");
            Console.Write("Choose an option: ");
        }

        static void AddEmployee()
        {
            int id = ReadInt("Enter employee ID: ");
            Console.Write("Enter full name: ");
            string name = Console.ReadLine();
            Console.Write("Enter department: ");
            string department = Console.ReadLine();
            Console.Write("Enter role: ");
            string role = Console.ReadLine();

            Employee existing = employeeTree.SearchById(id);
            if (existing != null)
            {
                Console.WriteLine("Employee with this ID already exists.");
                return;
            }

            Employee employee = new Employee(id, name, department, role);
            employeeTree.Insert(employee);
            Console.WriteLine("Employee added successfully.");
        }

        static void SearchEmployeeById()
        {
            int id = ReadInt("Enter employee ID to search: ");
            Employee employee = employeeTree.SearchById(id);

            if (employee == null)
                Console.WriteLine("Employee not found.");
            else
                Console.WriteLine(employee);
        }

        static void SearchEmployeeByName()
        {
            Console.Write("Enter employee name to search: ");
            string name = Console.ReadLine();

            List<Employee> results = employeeTree.SearchByName(name);

            if (results.Count == 0)
            {
                Console.WriteLine("No employees found.");
                return;
            }

            Console.WriteLine("Search results:");
            foreach (var employee in results)
            {
                Console.WriteLine(employee);
            }
        }

        static void AddLeave()
        {
            int id = ReadInt("Enter employee ID: ");
            Employee employee = employeeTree.SearchById(id);

            if (employee == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            Console.Write("Enter leave type: ");
            string leaveType = Console.ReadLine();

            Console.Write("Enter start date (YYYY-MM-DD): ");
            string startDate = Console.ReadLine();

            Console.Write("Enter end date (YYYY-MM-DD): ");
            string endDate = Console.ReadLine();

            Console.Write("Enter reason: ");
            string reason = Console.ReadLine();

            LeaveRecord leave = new LeaveRecord(leaveType, startDate, endDate, reason);
            employeeTree.AddLeaveToEmployee(id, leave);

            Console.WriteLine("Leave added successfully.");
        }

        static void ViewEmployeeLeaves()
        {
            int id = ReadInt("Enter employee ID: ");
            Employee employee = employeeTree.SearchById(id);

            if (employee == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            Console.WriteLine(employee);

            if (employee.Leaves.Count == 0)
            {
                Console.WriteLine("No leave records found.");
                return;
            }

            Console.WriteLine("Leave Records:");
            foreach (var leave in employee.Leaves)
            {
                Console.WriteLine("- " + leave);
            }
        }

        static int ReadInt(string message)
        {
            int value;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (int.TryParse(input, out value))
                    return value;

                Console.WriteLine("Invalid number. Please try again.");
            }
        }
    }
}
