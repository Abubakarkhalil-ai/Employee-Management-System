using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace EmployeeManagementSystem
{
    /// <summary>
    /// Handles reading from and writing to the SQLite database.
    /// This class provides methods to load employees and leave records
    /// from the database into the BST, and save BST data back to the database.
    /// Reference: Microsoft (2024) Microsoft.Data.Sqlite overview.
    /// Available at: https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/ (Accessed: March 2026).
    /// </summary>
    public class DatabaseHelper
    {
        // Connection string pointing to the SQLite database file
        private string connectionString;

        /// <summary>
        /// Constructor that sets the database file path.
        /// </summary>
        public DatabaseHelper(string databasePath)
        {
            connectionString = $"Data Source={databasePath}";
        }

        /// <summary>
        /// Creates the database tables if they do not already exist.
        /// This ensures the program can run even on a fresh database file.
        /// </summary>
        public void InitialiseDatabase()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string createEmployeesTable = @"
                    CREATE TABLE IF NOT EXISTS Employees (
                        Id INTEGER PRIMARY KEY,
                        FullName TEXT NOT NULL,
                        Department TEXT NOT NULL,
                        Role TEXT NOT NULL
                    );";

                string createLeaveTable = @"
                    CREATE TABLE IF NOT EXISTS LeaveRecords (
                        LeaveId INTEGER PRIMARY KEY AUTOINCREMENT,
                        EmployeeId INTEGER NOT NULL,
                        LeaveType TEXT NOT NULL,
                        StartDate TEXT NOT NULL,
                        EndDate TEXT NOT NULL,
                        Reason TEXT NOT NULL,
                        FOREIGN KEY (EmployeeId) REFERENCES Employees(Id)
                    );";

                using (var command = new SqliteCommand(createEmployeesTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (var command = new SqliteCommand(createLeaveTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Database initialised successfully.");
            }
        }

        /// <summary>
        /// Loads all employees and their leave records from the database
        /// and inserts them into the provided BST.
        /// Time Complexity: O(n log n) average for n employees inserted into BST.
        /// </summary>
        public void LoadFromDatabase(EmployeeBST bst)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                // Load all employees
                string selectEmployees = "SELECT Id, FullName, Department, Role FROM Employees;";

                using (var command = new SqliteCommand(selectEmployees, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string fullName = reader.GetString(1);
                            string department = reader.GetString(2);
                            string role = reader.GetString(3);

                            // Only insert if not already in BST
                            if (bst.SearchById(id) == null)
                            {
                                bst.Insert(new Employee(id, fullName, department, role));
                            }
                        }
                    }
                }

                // Load all leave records and assign to employees
                string selectLeaves = "SELECT EmployeeId, LeaveType, StartDate, EndDate, Reason FROM LeaveRecords;";

                using (var command = new SqliteCommand(selectLeaves, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int employeeId = reader.GetInt32(0);
                            string leaveType = reader.GetString(1);
                            string startDate = reader.GetString(2);
                            string endDate = reader.GetString(3);
                            string reason = reader.GetString(4);

                            LeaveRecord leave = new LeaveRecord(leaveType, startDate, endDate, reason);
                            bst.AddLeaveToEmployee(employeeId, leave);
                        }
                    }
                }

                Console.WriteLine("Data loaded from database successfully.");
            }
        }

        /// <summary>
        /// Saves all employees and their leave records from the BST into the database.
        /// Clears existing data first to avoid duplicates, then inserts fresh data.
        /// </summary>
        public void SaveToDatabase(EmployeeBST bst)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                // Clear existing data (leave records first due to foreign key)
                using (var command = new SqliteCommand("DELETE FROM LeaveRecords;", connection))
                {
                    command.ExecuteNonQuery();
                }

                using (var command = new SqliteCommand("DELETE FROM Employees;", connection))
                {
                    command.ExecuteNonQuery();
                }

                // Get all employees from BST
                List<Employee> employees = bst.GetAllEmployees();

                foreach (var employee in employees)
                {
                    // Insert employee
                    string insertEmployee = @"
                        INSERT INTO Employees (Id, FullName, Department, Role)
                        VALUES (@id, @name, @dept, @role);";

                    using (var command = new SqliteCommand(insertEmployee, connection))
                    {
                        command.Parameters.AddWithValue("@id", employee.Id);
                        command.Parameters.AddWithValue("@name", employee.FullName);
                        command.Parameters.AddWithValue("@dept", employee.Department);
                        command.Parameters.AddWithValue("@role", employee.Role);
                        command.ExecuteNonQuery();
                    }

                    // Insert leave records for this employee
                    foreach (var leave in employee.Leaves)
                    {
                        string insertLeave = @"
                            INSERT INTO LeaveRecords (EmployeeId, LeaveType, StartDate, EndDate, Reason)
                            VALUES (@empId, @type, @start, @end, @reason);";

                        using (var command = new SqliteCommand(insertLeave, connection))
                        {
                            command.Parameters.AddWithValue("@empId", employee.Id);
                            command.Parameters.AddWithValue("@type", leave.LeaveType);
                            command.Parameters.AddWithValue("@start", leave.StartDate);
                            command.Parameters.AddWithValue("@end", leave.EndDate);
                            command.Parameters.AddWithValue("@reason", leave.Reason);
                            command.ExecuteNonQuery();
                        }
                    }
                }

                Console.WriteLine("Data saved to database successfully.");
            }
        }
    }
}
