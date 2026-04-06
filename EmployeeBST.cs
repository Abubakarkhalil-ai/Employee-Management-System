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

        public Employee SearchById(int id)
        {
            EmployeeNode result = SearchNodeById(root, id);
            return result != null ? result.Data : null;
        }

        private EmployeeNode SearchNodeById(EmployeeNode node, int id)
        {
            if (node == null || node.Data.Id == id)
                return node;

            if (id < node.Data.Id)
                return SearchNodeById(node.Left, id);

            return SearchNodeById(node.Right, id);
        }

        public List<Employee> SearchByName(string name)
        {
            List<Employee> matches = new List<Employee>();
            SearchByNameRecursive(root, name.ToLower(), matches);
            return matches;
        }

        private void SearchByNameRecursive(EmployeeNode node, string searchName, List<Employee> matches)
        {
            if (node == null)
                return;

            SearchByNameRecursive(node.Left, searchName, matches);

            if (node.Data.FullName.ToLower().Contains(searchName))
                matches.Add(node.Data);

            SearchByNameRecursive(node.Right, searchName, matches);
        }
    }
}
