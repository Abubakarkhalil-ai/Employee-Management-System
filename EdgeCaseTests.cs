using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagementSystem;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Tests
{
    [TestClass]
    public class EdgeCaseTests
    {
        [TestMethod]
        public void BST_InsertSingleEmployee_GetAllReturnsOne()
        {
            // Arrange
            EmployeeBST bst = new EmployeeBST();
            bst.Insert(new Employee(101, "Ali Khan", "HR", "Manager"));

            // Act
            List<Employee> all = bst.GetAllEmployees();

            // Assert
            Assert.AreEqual(1, all.Count);
        }

        [TestMethod]
        public void BST_InsertAscendingIds_AllCanBeFound()
        {
            // Arrange - worst case for BST (becomes a linked list)
            EmployeeBST bst = new EmployeeBST();
            bst.Insert(new Employee(1, "Employee One", "Dept", "Role"));
            bst.Insert(new Employee(2, "Employee Two", "Dept", "Role"));
            bst.Insert(new Employee(3, "Employee Three", "Dept", "Role"));
            bst.Insert(new Employee(4, "Employee Four", "Dept", "Role"));
            bst.Insert(new Employee(5, "Employee Five", "Dept", "Role"));

            // Act & Assert - all should still be findable
            Assert.IsNotNull(bst.SearchById(1));
            Assert.IsNotNull(bst.SearchById(3));
            Assert.IsNotNull(bst.SearchById(5));
        }

        [TestMethod]
        public void BST_InsertDescendingIds_AllCanBeFound()
        {
            // Arrange - another worst case for BST
            EmployeeBST bst = new EmployeeBST();
            bst.Insert(new Employee(5, "Employee Five", "Dept", "Role"));
            bst.Insert(new Employee(4, "Employee Four", "Dept", "Role"));
            bst.Insert(new Employee(3, "Employee Three", "Dept", "Role"));
            bst.Insert(new Employee(2, "Employee Two", "Dept", "Role"));
            bst.Insert(new Employee(1, "Employee One", "Dept", "Role"));

            // Act & Assert
            Assert.IsNotNull(bst.SearchById(1));
            Assert.IsNotNull(bst.SearchById(5));
            List<Employee> all = bst.GetAllEmployees();
            Assert.AreEqual(5, all.Count);
        }

        [TestMethod]
        public void BST_SearchByName_EmptyString_ReturnsAll()
        {
            // Arrange
            EmployeeBST bst = new EmployeeBST();
            bst.Insert(new Employee(101, "Ali Khan", "HR", "Manager"));
            bst.Insert(new Employee(102, "Sara Ahmed", "IT", "Developer"));

            // Act - empty string should match all names via Contains
            List<Employee> results = bst.SearchByName("");

            // Assert
            Assert.AreEqual(2, results.Count);
        }

        [TestMethod]
        public void Employee_LargeId_HandledCorrectly()
        {
            // Arrange
            EmployeeBST bst = new EmployeeBST();
            bst.Insert(new Employee(999999, "Large ID Employee", "IT", "Engineer"));

            // Act
            Employee result = bst.SearchById(999999);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Large ID Employee", result.FullName);
        }

        [TestMethod]
        public void Employee_SpecialCharactersInName_HandledCorrectly()
        {
            // Arrange
            EmployeeBST bst = new EmployeeBST();
            bst.Insert(new Employee(101, "O'Brien-Smith", "HR", "Manager"));

            // Act
            List<Employee> results = bst.SearchByName("O'Brien");

            // Assert
            Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void AddLeave_ToEmptyTree_ReturnsFalse()
        {
            // Arrange
            EmployeeBST bst = new EmployeeBST();
            LeaveRecord leave = new LeaveRecord("Annual Leave", "2026-01-01", "2026-01-05", "Holiday");

            // Act
            bool result = bst.AddLeaveToEmployee(101, leave);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetAllEmployees_ReturnsSortedByIdAscending()
        {
            // Arrange - insert in random order
            EmployeeBST bst = new EmployeeBST();
            bst.Insert(new Employee(105, "E", "Dept", "Role"));
            bst.Insert(new Employee(101, "A", "Dept", "Role"));
            bst.Insert(new Employee(103, "C", "Dept", "Role"));
            bst.Insert(new Employee(102, "B", "Dept", "Role"));
            bst.Insert(new Employee(104, "D", "Dept", "Role"));

            // Act
            List<Employee> all = bst.GetAllEmployees();

            // Assert - in-order traversal should give sorted order
            Assert.AreEqual(101, all[0].Id);
            Assert.AreEqual(102, all[1].Id);
            Assert.AreEqual(103, all[2].Id);
            Assert.AreEqual(104, all[3].Id);
            Assert.AreEqual(105, all[4].Id);
        }
    }
}
