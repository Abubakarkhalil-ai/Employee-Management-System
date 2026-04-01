using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagementSystem;

namespace EmployeeManagementSystem.Tests
{
    [TestClass]
    public class EmployeeTests
    {
        [TestMethod]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange & Act
            Employee emp = new Employee(101, "Ali Khan", "HR", "Manager");

            // Assert
            Assert.AreEqual(101, emp.Id);
            Assert.AreEqual("Ali Khan", emp.FullName);
            Assert.AreEqual("HR", emp.Department);
            Assert.AreEqual("Manager", emp.Role);
        }

        [TestMethod]
        public void Constructor_InitialisesEmptyLeavesList()
        {
            // Arrange & Act
            Employee emp = new Employee(101, "Ali Khan", "HR", "Manager");

            // Assert
            Assert.IsNotNull(emp.Leaves);
            Assert.AreEqual(0, emp.Leaves.Count);
        }

        [TestMethod]
        public void ToString_ReturnsFormattedString()
        {
            // Arrange
            Employee emp = new Employee(101, "Ali Khan", "HR", "Manager");

            // Act
            string result = emp.ToString();

            // Assert
            Assert.AreEqual("ID: 101, Name: Ali Khan, Department: HR, Role: Manager", result);
        }
    }
}
