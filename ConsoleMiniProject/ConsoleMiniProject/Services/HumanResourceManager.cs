﻿using System;
using System.Collections.Generic;
using System.Text;
using ConsoleMiniProject.Interfaces;
using ConsoleMiniProject.Models;
using System.Linq;
using ConsoleMiniProject.Enums;

namespace ConsoleMiniProject.Services
{
    class HumanResourceManager : IHumanResourceManager
    {
        
        private Department[] _departments;
        public Department[] Departments => _departments;
        public HumanResourceManager()
        {
            _departments = new Department[0];
        }

        #region Department
        public void AddDepartment(string name, double salaryLimit, int workerLimit)
        {
            Department department = new Department(name , salaryLimit , workerLimit);
            Array.Resize(ref _departments, _departments.Length + 1);
            _departments[_departments.Length - 1] = department;
               Console.WriteLine("\n\n***************************************\n\nDepartment successfully added!\n");
        }
        public void EditDepartments(string oldDepartmentname, string newDepartmentName)
        {
            Department department = Departments.First(department => oldDepartmentname == department.Name);
            if (department != null)
                department.Name = newDepartmentName;
            else
                Console.WriteLine("Not Such Department Found!");
        }
        public Department[] GetDepartments(Department[] departments)
        {
            return departments;
        }
        public void InfoDepartment()
        {
            foreach (var item in Departments)
            {
                for (int i = 0; i < Departments.Length; i++)
                {
                    if (item.Employees == null)
                        i++;
                    else
                    {
                        Console.WriteLine("++++++++++++++++++++++++++++++++++++++" +
                            $"\nDepartment Name: {item.Name}" +
                            "\n++++++++++++++++++++++++++++++++++++++" +
                            $"\nNumber of Workers: {item.Employees.Length}" +
                            "\n++++++++++++++++++++++++++++++++++++++" +
                            $"\nAverage Salary: {item.CalcSalaryAverage()}" +
                            "\n======================================\n");
                        break;
                    }
                }
            }
        }
        #endregion
        #region Employee
        public void ShowEmployeeOfDepartment(Employee[] employees)
        {
                Console.Write("Enter the Department Name: ");
                string departmentName = Console.ReadLine();

                foreach (var item in Departments)
                {
                    if (item.Name == departmentName)
                    {
                        for (int i = 0; i < item.Employees.Length; i++)
                        {
                            foreach (var item1 in item.Employees)
                            {
                                if (item1.FullName != null)
                                {
                                    Console.WriteLine("++++++++++++++++++++++++++++++++++++++" +
                                $"\nEmployee ID: {item1.Id}" +
                                "\n++++++++++++++++++++++++++++++++++++++" +
                                $"\nFull Name of Employee: {item1.FullName}" +
                                "\n++++++++++++++++++++++++++++++++++++++" +
                                $"\nPosition of Employee: {item1.Positions}" +
                                "\n++++++++++++++++++++++++++++++++++++++" +
                                $"\nSalary of Employee: {item1.Salary}" +
                                "\n======================================\n");
                                    break;
                                }
                                else
                                    i++;
                            }
                        }
                    }
                }
        }
        public void ShowEmployees(Employee[] employees)
        {
            foreach (var item in Departments)
            {
                foreach (var item1 in item.Employees)
                {
                    if (item1 != null)
                    {
                        Console.WriteLine("++++++++++++++++++++++++++++++++++++++" +
                $"\nEmployee Id: {item1.Id}" +
                "\n++++++++++++++++++++++++++++++++++++++" +
                $"\nFull Name of Employee: {item1.FullName}" +
                "\n++++++++++++++++++++++++++++++++++++++" +
                $"\nDepartment Name: {item1.DepartmentName}" +
                "\n++++++++++++++++++++++++++++++++++++++" +
                $"\nSalary of Employee: {item1.Salary}\n\n");
                    }

                }
            }
        }
        public void AddEmployee(string fullName, Positions positions, double salary, int index)
        {
            Department department = _departments[index];
            if (department.WorkerLimit > department.Employees.Length)
            {
                if (department.SalaryLimit > salary)
                {
                    Employee employee = new Employee(fullName, salary, department, positions);
                    //AddEmployee(fullName, positions, salary, index);
                    department.AddEmployee(employee);
                    Console.WriteLine("\n\n***************************************\n\nEmployee successfully added!\n");
                    return;
                }
                else
                {
                    Console.WriteLine("You have exceeded the salary limit. Please try again: ");
                    return;
                }
            }
            else
            {
                Console.WriteLine("You have exceeded the worker limit. Please try again: ");
                return;
            }
        }
        public void DeleteEmployee(string dName, string idEmp, string eName)
        {
            Department department = null;
            foreach (Department item in _departments)
            {
                if (item.Name.ToLower() == dName.ToLower())
                {
                    department = item;
                    break;
                }
            }
            Employee employee = null;

            if (department != null)
            {
                foreach (Employee item in department.Employees)
                {

                    if (item.FullName.ToUpper() == idEmp.ToUpper())
                    {
                        employee = item;
                        break;
                    }
                }
            }
            else
                Console.WriteLine("Employee is not exist!");
            if (employee != null)
            {
                foreach (var item in department.Employees)
                {
                    if (item.Id.ToUpper() == idEmp.ToUpper())
                    {
                        int index = Array.IndexOf(department.Employees, employee);
                        Array.Clear(department.Employees, index, 1);
                        Console.WriteLine("\n\n***************************************\n\nEmployee Successfully Deleted!\n");
                    }
                }
            }
            else
                Console.WriteLine("Employee is not exist aaaa!");
        }
        public void EditEmployee( string id, double salary, Positions positions)
        {
            //foreach (var item in _departments)
            //{
            //    foreach (var item1 in item.Employees)
            //    {
            //        if (item1.Id == id)
            //        {
            //            Console.WriteLine("Employee Detected: ");
            //            Console.WriteLine($"{item1.FullName} {item1.Salary} {item1.Positions}");
            //        }
            //    }
            //}
        }
        #endregion
    }

}
