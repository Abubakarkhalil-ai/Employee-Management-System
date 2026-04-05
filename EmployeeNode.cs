namespace EmployeeManagementSystem
{
    /// <summary>
    /// Represents a node in the Binary Search Tree.
    /// Each node holds an Employee object and references to left and right child nodes.
    /// </summary>
    public class EmployeeNode
    {
        // The employee data stored in this node
        public Employee Data;

        // Reference to the left child node (employees with smaller IDs)
        public EmployeeNode Left;

        // Reference to the right child node (employees with larger IDs)
        public EmployeeNode Right;

        /// <summary>
        /// Constructor to create a new node with the given employee.
        /// Left and right children are initialised to null.
        /// </summary>
        public EmployeeNode(Employee employee)
        {
            Data = employee;
            Left = null;
            Right = null;
        }
    }
}
