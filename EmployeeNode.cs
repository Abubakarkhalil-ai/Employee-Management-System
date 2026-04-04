namespace EmployeeManagementSystem
{
    public class EmployeeNode
    {
        public Employee Data;
        public EmployeeNode Left;
        public EmployeeNode Right;

        public EmployeeNode(Employee employee)
        {
            Data = employee;
            Left = null;
            Right = null;
        }
    }
}
