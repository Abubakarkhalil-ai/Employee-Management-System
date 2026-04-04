using System;
using System.Collections.Generic;

namespace EmployeeManagementSystem
{
    public class EmployeeBST
    {
        private EmployeeNode root;

        public EmployeeBST()
        {
            root = null;
        }

        public void Insert(Employee employee)
        {
            root = InsertRecursive(root, employee);
        }

        private EmployeeNode InsertRecursive(EmployeeNode node, Employee employee)
        {
            if (node == null)
                return new EmployeeNode(employee);

            if (employee.Id < node.Data.Id)
                node.Left = InsertRecursive(node.Left, employee);
            else if (employee.Id > node.Data.Id)
                node.Right = InsertRecursive(node.Right, employee);
            else
                Console.WriteLine("Employee ID already exists.");

            return node;
        }
    }
}
