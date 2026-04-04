using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagementSystem;
using System.Collections.Generic;

namespace EmployeeManagementSystem.Tests
{
    [TestClass]
    public class BSTSearchByNameTests
    {
        private EmployeeBST bst;

        [TestInitialize]
        public void Setup()
        {
            // Create a BST with test data before each test
            bst = new EmployeeBST();
            bst.Insert(new Employee(101, "Ali Khan", "HR", "Manager"));
            bst.Insert(new Employee(102, "Sara Ahmed", "IT", "Developer"));
            bst.Insert(new Employee(103, "John Smith", "Finance", "Analyst"));
            bst.Insert(new Employee(104, "Ali Raza", "Sales", "Coordinator"));
        }

        [TestMethod]
        public void SearchByName_ExactMatch_ReturnsEmployee()
        {
            // Act
            List<Employee> results = bst.SearchByName("John Smith");

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(103, results[0].Id);
        }

        [TestMethod]
        public void SearchByName_PartialMatch_ReturnsMultiple()
        {
            // Act - "Ali" should match "Ali Khan" and "Ali Raza"
            List<Employee> results = bst.SearchByName("Ali");

            // Assert
            Assert.AreEqual(2, results.Count);
        }

        [TestMethod]
        public void SearchByName_CaseInsensitive_ReturnsMatch()
        {
            // Act
            List<Employee> results = bst.SearchByName("ali");

            // Assert
            Assert.AreEqual(2, results.Count);
        }

        [TestMethod]
        public void SearchByName_NoMatch_ReturnsEmptyList()
        {
            // Act
            List<Employee> results = bst.SearchByName("Nonexistent");

            // Assert
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void SearchByName_EmptyTree_ReturnsEmptyList()
        {
            // Arrange
            EmployeeBST emptyBst = new EmployeeBST();

            // Act
            List<Employee> results = emptyBst.SearchByName("Ali");

            // Assert
            Assert.AreEqual(0, results.Count);
        }
    }
}
