using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagementSystem;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Tests
{
    [TestClass]
    public class BSTLeaveAndGetAllTests
    {
        private EmployeeBST bst;

        [TestInitialize]
        public void Setup()
        {
            bst = new EmployeeBST();
            bst.Insert(new Employee(101, "Ali Khan", "HR", "Manager"));
            bst.Insert(new Employee(102, "Sara Ahmed", "IT", "Developer"));
            bst.Insert(new Employee(103, "John Smith", "Finance", "Analyst"));
        }

        [TestMethod]
        public void AddLeaveToEmployee_ValidId_ReturnsTrue()
        {
            // Arrange
            LeaveRecord leave = new LeaveRecord("Annual Leave", "2026-03-10", "2026-03-15", "Family trip");

            // Act
            bool result = bst.AddLeaveToEmployee(101, leave);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AddLeaveToEmployee_InvalidId_ReturnsFalse()
        {
            // Arrange
            LeaveRecord leave = new LeaveRecord("Sick Leave", "2026-03-05", "2026-03-06", "Flu");

            // Act
            bool result = bst.AddLeaveToEmployee(999, leave);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddLeaveToEmployee_LeaveAppearsInEmployeeRecord()
        {
            // Arrange
            LeaveRecord leave = new LeaveRecord("Annual Leave", "2026-03-10", "2026-03-15", "Family trip");

            // Act
            bst.AddLeaveToEmployee(101, leave);
            Employee emp = bst.SearchById(101);

            // Assert
            Assert.AreEqual(1, emp.Leaves.Count);
            Assert.AreEqual("Annual Leave", emp.Leaves[0].LeaveType);
        }

        [TestMethod]
        public void AddMultipleLeaves_AllStoredCorrectly()
        {
            // Arrange & Act
            bst.AddLeaveToEmployee(101, new LeaveRecord("Annual Leave", "2026-03-10", "2026-03-15", "Trip"));
            bst.AddLeaveToEmployee(101, new LeaveRecord("Sick Leave", "2026-04-01", "2026-04-02", "Cold"));

            Employee emp = bst.SearchById(101);

            // Assert
            Assert.AreEqual(2, emp.Leaves.Count);
        }

        [TestMethod]
        public void GetAllEmployees_ReturnsSortedList()
        {
            // Act
            List<Employee> all = bst.GetAllEmployees();

            // Assert - should be sorted by ID due to in-order traversal
            Assert.AreEqual(3, all.Count);
            Assert.AreEqual(101, all[0].Id);
            Assert.AreEqual(102, all[1].Id);
            Assert.AreEqual(103, all[2].Id);
        }

        [TestMethod]
        public void GetAllEmployees_EmptyTree_ReturnsEmptyList()
        {
            // Arrange
            EmployeeBST emptyBst = new EmployeeBST();

            // Act
            List<Employee> all = emptyBst.GetAllEmployees();

            // Assert
            Assert.AreEqual(0, all.Count);
        }
    }
}
