using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagementSystem;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Tests
{
    [TestClass]
    public class FileDataTests
    {
        /// <summary>
        /// Tests that simulate the file parsing logic used in Program.cs
        /// without depending on actual file I/O.
        /// </summary>

        [TestMethod]
        public void ParseLine_ValidFormat_CreatesEmployee()
        {
            // Arrange - simulate a line from the CSV file
            string line = "101,Ali Khan,HR,Manager";
            string[] parts = line.Split(',');

            // Act
            int id;
            bool parsed = int.TryParse(parts[0], out id);
            Employee emp = new Employee(id, parts[1], parts[2], parts[3]);

            // Assert
            Assert.IsTrue(parsed);
            Assert.AreEqual(101, emp.Id);
            Assert.AreEqual("Ali Khan", emp.FullName);
            Assert.AreEqual("HR", emp.Department);
            Assert.AreEqual("Manager", emp.Role);
        }

        [TestMethod]
        public void ParseLine_InvalidId_FailsToParse()
        {
            // Arrange - line with non-numeric ID
            string line = "abc,Ali Khan,HR,Manager";
            string[] parts = line.Split(',');

            // Act
            int id;
            bool parsed = int.TryParse(parts[0], out id);

            // Assert
            Assert.IsFalse(parsed);
        }

        [TestMethod]
        public void ParseLine_TooFewFields_DetectedByLengthCheck()
        {
            // Arrange - line with only 2 fields
            string line = "101,Ali Khan";
            string[] parts = line.Split(',');

            // Act & Assert
            Assert.IsTrue(parts.Length < 4);
        }

        [TestMethod]
        public void ParseLine_EmptyLine_DetectedByWhitespaceCheck()
        {
            // Arrange
            string line = "   ";

            // Act & Assert
            Assert.IsTrue(string.IsNullOrWhiteSpace(line));
        }

        [TestMethod]
        public void SaveFormat_EmployeeToCSV_CorrectFormat()
        {
            // Arrange
            Employee emp = new Employee(101, "Ali Khan", "HR", "Manager");

            // Act - simulate the save format used in Program.cs
            string csvLine = $"{emp.Id},{emp.FullName},{emp.Department},{emp.Role}";

            // Assert
            Assert.AreEqual("101,Ali Khan,HR,Manager", csvLine);
        }

        [TestMethod]
        public void LoadMultipleLines_AllValidEmployeesInserted()
        {
            // Arrange - simulate loading multiple lines
            string[] lines = {
                "101,Ali Khan,HR,Manager",
                "102,Sara Ahmed,IT,Developer",
                "103,John Smith,Finance,Analyst"
            };
            EmployeeBST bst = new EmployeeBST();

            // Act - simulate the load logic
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                int id;
                if (int.TryParse(parts[0], out id) && parts.Length >= 4)
                {
                    if (bst.SearchById(id) == null)
                    {
                        bst.Insert(new Employee(id, parts[1], parts[2], parts[3]));
                    }
                }
            }

            // Assert
            List<Employee> all = bst.GetAllEmployees();
            Assert.AreEqual(3, all.Count);
        }
    }
}
