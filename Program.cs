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
                        Console.WriteLine("Search by ID - Coming soon");
                        break;
                    case "3":
                        Console.WriteLine("Search by Name - Coming soon");
                        break;
                    case "4":
                        Console.WriteLine("Add Leave - Coming soon");
                        break;
                    case "5":
                        Console.WriteLine("View Leaves - Coming soon");
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
