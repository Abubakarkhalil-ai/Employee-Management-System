using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagementSystem;

namespace EmployeeManagementSystem.Tests
{
    [TestClass]
    public class BSTInsertSearchTests
    {
        [TestMethod]
        public void Insert_SingleEmployee_CanBeFoundById()
        {
            // Arrange
            EmployeeBST bst = new EmployeeBST();
            Employee emp = new Employee(101, "Ali Khan", "HR", "Manager");

            // Act
            bst.Insert(emp);
            Employee result = bst.SearchById(101);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(101, result.Id);
            Assert.AreEqual("Ali Khan", result.FullName);
        }

        [TestMethod]
        public void Insert_MultipleEmployees_AllCanBeFound()
        {
            // Arrange
            EmployeeBST bst = new EmployeeBST();
            bst.Insert(new Employee(103, "John Smith", "Finance", "Analyst"));
            bst.Insert(new Employee(101, "Ali Khan", "HR", "Manager"));
            bst.Insert(new Employee(105, "Sara Ahmed", "IT", "Developer"));

            // Act & Assert
            Assert.IsNotNull(bst.SearchById(103));
            Assert.IsNotNull(bst.SearchById(101));
            Assert.IsNotNull(bst.SearchById(105));
        }

        [TestMethod]
        public void SearchById_NonExistentId_ReturnsNull()
        {
            // Arrange
            EmployeeBST bst = new EmployeeBST();
            bst.Insert(new Employee(101, "Ali Khan", "HR", "Manager"));

            // Act
            Employee result = bst.SearchById(999);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void SearchById_EmptyTree_ReturnsNull()
        {
            // Arrange
            EmployeeBST bst = new EmployeeBST();

            // Act
            Employee result = bst.SearchById(101);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Insert_DuplicateId_DoesNotOverwrite()
        {
            // Arrange
            EmployeeBST bst = new EmployeeBST();
            bst.Insert(new Employee(101, "Ali Khan", "HR", "Manager"));

            // Act - insert duplicate ID with different name
            bst.Insert(new Employee(101, "Different Name", "IT", "Developer"));
            Employee result = bst.SearchById(101);

            // Assert - original employee should still be there
            Assert.AreEqual("Ali Khan", result.FullName);
        }
    }
}
