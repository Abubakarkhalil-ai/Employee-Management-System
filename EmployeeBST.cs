using System;
using System.Collections.Generic;

namespace EmployeeManagementSystem
{
    /// <summary>
    /// Binary Search Tree (BST) to store and manage Employee objects.
    /// Employees are ordered by their ID for efficient searching.
    /// Time Complexity:
    ///   Insert: O(log n) average, O(n) worst case
    ///   Search by ID: O(log n) average, O(n) worst case
    ///   Search by Name: O(n) - must visit all nodes
    ///   Display All: O(n) - in-order traversal visits every node
    /// </summary>
    public class EmployeeBST
    {
        // Root node of the binary search tree
        private EmployeeNode root;

        /// <summary>
        /// Constructor initialises an empty BST.
        /// </summary>
        public EmployeeBST()
        {
            root = null;
        }

        /// <summary>
        /// Public method to insert a new employee into the BST.
        /// </summary>
        public void Insert(Employee employee)
        {
            root = InsertRecursive(root, employee);
        }

        /// <summary>
        /// Recursively finds the correct position and inserts the employee.
        /// If the ID already exists, it prints an error message.
        /// </summary>
        private EmployeeNode InsertRecursive(EmployeeNode node, Employee employee)
        {
            // Base case: found an empty spot, create new node
            if (node == null)
                return new EmployeeNode(employee);

            // Traverse left if new ID is smaller
            if (employee.Id < node.Data.Id)
                node.Left = InsertRecursive(node.Left, employee);
            // Traverse right if new ID is larger
            else if (employee.Id > node.Data.Id)
                node.Right = InsertRecursive(node.Right, employee);
            // Duplicate ID found
            else
                Console.WriteLine("Employee ID already exists.");

            return node;
        }

        /// <summary>
        /// Searches for an employee by their unique ID.
        /// Returns the Employee object if found, null otherwise.
        /// </summary>
        public Employee SearchById(int id)
        {
            EmployeeNode result = SearchNodeById(root, id);
            return result != null ? result.Data : null;
        }

        /// <summary>
        /// Recursively searches the BST for a node with the given ID.
        /// Uses BST property: left children have smaller IDs, right children have larger IDs.
        /// </summary>
        private EmployeeNode SearchNodeById(EmployeeNode node, int id)
        {
            // Base case: node is null or ID matches
            if (node == null || node.Data.Id == id)
                return node;

            // Search left subtree if target ID is smaller
            if (id < node.Data.Id)
                return SearchNodeById(node.Left, id);

            // Search right subtree if target ID is larger
            return SearchNodeById(node.Right, id);
        }

        /// <summary>
        /// Searches for employees whose name contains the given search string.
        /// Returns a list of all matching employees (case-insensitive).
        /// This requires O(n) time as we must check every node.
        /// </summary>
        public List<Employee> SearchByName(string name)
        {
            List<Employee> matches = new List<Employee>();
            SearchByNameRecursive(root, name.ToLower(), matches);
            return matches;
        }

        /// <summary>
        /// Recursively traverses all nodes and adds matches to the list.
        /// Uses in-order traversal to check every employee.
        /// </summary>
        private void SearchByNameRecursive(EmployeeNode node, string searchName, List<Employee> matches)
        {
            if (node == null)
                return;

            SearchByNameRecursive(node.Left, searchName, matches);

            if (node.Data.FullName.ToLower().Contains(searchName))
                matches.Add(node.Data);

            SearchByNameRecursive(node.Right, searchName, matches);
        }

        /// <summary>
        /// Displays all employees in sorted order by ID.
        /// Also shows leave records for each employee if any exist.
        /// </summary>
        public void DisplayAll()
        {
            if (root == null)
            {
                Console.WriteLine("No employees found.");
                return;
            }

            InOrderTraversal(root);
        }

        /// <summary>
        /// In-order traversal: visits left subtree, then current node, then right subtree.
        /// This produces output sorted by employee ID.
        /// </summary>
        private void InOrderTraversal(EmployeeNode node)
        {
            if (node == null)
                return;

            InOrderTraversal(node.Left);
            Console.WriteLine(node.Data);

            // Display leave records if the employee has any
            if (node.Data.Leaves.Count > 0)
            {
                Console.WriteLine("  Leave Records:");
                foreach (var leave in node.Data.Leaves)
                {
                    Console.WriteLine("   - " + leave);
                }
            }

            InOrderTraversal(node.Right);
        }

        /// <summary>
        /// Adds a leave record to the employee with the given ID.
        /// Returns true if successful, false if employee was not found.
        /// </summary>
        public bool AddLeaveToEmployee(int employeeId, LeaveRecord leave)
        {
            Employee employee = SearchById(employeeId);
            if (employee == null)
                return false;

            employee.Leaves.Add(leave);
            return true;
        }

        /// <summary>
        /// Returns a list of all employees in the BST, sorted by ID.
        /// Used for saving data to file.
        /// </summary>
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            CollectEmployees(root, employees);
            return employees;
        }

        /// <summary>
        /// Helper method that collects all employees via in-order traversal.
        /// </summary>
        private void CollectEmployees(EmployeeNode node, List<Employee> employees)
        {
            if (node == null)
                return;

            CollectEmployees(node.Left, employees);
            employees.Add(node.Data);
            CollectEmployees(node.Right, employees);
        }
    }
}
