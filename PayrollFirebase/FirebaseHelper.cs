
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayrollFirebase.Model;

namespace PayrollFirebase
{
    class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://payroll-xamarin-default-rtdb.firebaseio.com/");
        public async Task<List<Employee>> GetAllEmployee()
        {

            return (await firebase
              .Child("Employees")
              .OnceAsync<Employee>()).Select(item => new Employee
              {
                  EmployeeNum = item.Object.EmployeeNum,
                  Name = item.Object.Name,
                  HoursWorked = item.Object.HoursWorked,
                  EmployeeStatus = item.Object.EmployeeStatus,
                  CivilStatus = item.Object.CivilStatus,
                  Gross = item.Object.Gross,
                  Deduction = item.Object.Deduction,
                  NetIncome = item.Object.NetIncome

              }).ToList();
        }
        public async Task AddData(int employeenum, 
            string name, 
            double hoursworked, 
            string empstat, 
            string civilstatus, 
            double gross, 
            double deduction, 
            double netincome)
        {

            await firebase
              .Child("Employees")
              .PostAsync(new Employee() { EmployeeNum = employeenum, Name = name, HoursWorked = hoursworked, EmployeeStatus = empstat, CivilStatus = civilstatus, Gross = gross, Deduction = deduction, NetIncome = netincome });
        }
        public async Task<Employee> SearchData(int employeenum)
        {
            var allEmployee = await GetAllEmployee();
            await firebase
              .Child("Employees")
              .OnceAsync<Employee>();
            return allEmployee.Where(a => a.EmployeeNum == employeenum).FirstOrDefault();
        }

        public async Task UpdateData(int empnum,
            string name,
            double hourswork,
            string empstat,
            string civilstat,
            double gross,
            double deduction,
            double net)
        {
            var updatedata = (await firebase
                .Child("Employees")
                .OnceAsync<Employee>())
                .Where(a => a.Object.EmployeeNum == empnum).FirstOrDefault();

            await firebase
                .Child("Employees")
                .Child(updatedata.Key)
                .PutAsync(new Employee()
                {
                    EmployeeNum = empnum,
                    Name = name,
                    HoursWorked = hourswork,
                    EmployeeStatus = empstat,
                    CivilStatus = civilstat,
                    Gross = gross,
                    Deduction = deduction,
                    NetIncome = net
                });
        }
        public async Task DeleteData(int empnum)
        {
            var deldata = (await firebase.Child("Employees").OnceAsync<Employee>()).
                Where(a => a.Object.EmployeeNum == empnum).FirstOrDefault();

            await firebase.Child("Employees").Child(deldata.Key).DeleteAsync();
        }
    }
}
