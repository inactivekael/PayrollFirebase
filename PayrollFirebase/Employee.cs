using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollFirebase.Model
{
    public class Employee
    {
        public int EmployeeNum { get; set; }
        public string Name { get; set; }
        public double HoursWorked { get; set; }
        public string EmployeeStatus { get; set; }
        public string CivilStatus { get; set; }
        public double Gross { get; set; }
        public double Deduction { get; set; }
        public double NetIncome { get; set; }
    }
}
