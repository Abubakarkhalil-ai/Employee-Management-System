using System;
using System.IO;
using System.Collections.Generic;

namespace EmployeeManagementSystem
{
    /// <summary>
    /// Main program class that provides a console-based menu interface
    /// for the Employee Management System.
    /// </summary>
    class Program
    {
        // BST instance to store all employee records
        static EmployeeBST employeeTree = new EmployeeBST();

        /// <summary>
        /// Entry point - runs the main menu loop until user exits.
        /// </summary>
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
                        DisplayAllEmployees();
                        break;
                    case "7":
                        LoadEmployeesFromFile();
                        break;
                    case "8":
                        SaveEmployeesToFile();
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

        /// <summary>
        /// Displays the main menu options to the user.
        /// </summary>
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

        /// <summary>
        /// Prompts user for employee details and adds them to the BST.
        /// Checks for duplicate IDs before inserting.
        /// </summary>
        static void AddEmployee()
        {
            int id = ReadInt("Enter employee ID: ");
            Console.Write("Enter full name: ");
            string name = Console.ReadLine();
            Console.Write("Enter department: ");
            string department = Console.ReadLine();
            Console.Write("Enter role: ");
            string role = Console.ReadLine();

            // Check if employee with this ID already exists
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

        /// <summary>
        /// Searches for an employee by their ID and displays the result.
        /// </summary>
        static void SearchEmployeeById()
        {
            int id = ReadInt("Enter employee ID to search: ");
            Employee employee = employeeTree.SearchById(id);

            if (employee == null)
                Console.WriteLine("Employee not found.");
            else
                Console.WriteLine(employee);
        }

        /// <summary>
        /// Searches for employees by name (partial match) and displays all results.
        /// </summary>
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

        /// <summary>
        /// Adds a leave record to an existing employee.
        /// </summary>
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

        /// <summary>
        /// Displays all leave records for a specific employee.
        /// </summary>
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

        /// <summary>
        /// Displays all employees stored in the BST in sorted order.
        /// </summary>
        static void DisplayAllEmployees()
        {
            employeeTree.DisplayAll();
        }

        /// <summary>
        /// Loads employees from a CSV text file into the BST.
        /// File format: ID,FullName,Department,Role
        /// User specifies the file name at runtime (not hardcoded).
        /// </summary>
        static void LoadEmployeesFromFile()
        {
            Console.Write("Enter file name to load from: ");
            string fileName = Console.ReadLine();

            if (!File.Exists(fileName))
            {
                Console.WriteLine("File not found.");
                return;
            }

            try
            {
                string[] lines = File.ReadAllLines(fileName);

                foreach (string line in lines)
                {
                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split(',');

                    // Validate line has enough fields
                    if (parts.Length < 4)
                        continue;

                    // Validate ID is a valid integer
                    int id;
                    if (!int.TryParse(parts[0], out id))
                        continue;

                    string name = parts[1];
                    string department = parts[2];
                    string role = parts[3];

                    // Only add if employee doesn't already exist
                    if (employeeTree.SearchById(id) == null)
                    {
                        employeeTree.Insert(new Employee(id, name, department, role));
                    }
                }

                Console.WriteLine("Employees loaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading file: " + ex.Message);
            }
        }

        /// <summary>
        /// Saves all employees from the BST to a CSV text file.
        /// </summary>
        static void SaveEmployeesToFile()
        {
            Console.Write("Enter file name to save to: ");
            string fileName = Console.ReadLine();

            try
            {
                List<Employee> employees = employeeTree.GetAllEmployees();
                List<string> lines = new List<string>();

                foreach (var employee in employees)
                {
                    lines.Add($"{employee.Id},{employee.FullName},{employee.Department},{employee.Role}");
                }

                File.WriteAllLines(fileName, lines);
                Console.WriteLine("Employees saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving file: " + ex.Message);
            }
        }

        /// <summary>
        /// Helper method to read and validate an integer from user input.
        /// Keeps prompting until a valid number is entered.
        /// </summary>
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
