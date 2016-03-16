using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using StormTest.Entities;
using TechTalk.SpecFlow;
using TestingContextCore.PublicMembers;

namespace StormTest.Definitions
{
    [Binding]
    public class Mutations
    {
        private readonly Storage storage;

        public Mutations(Storage storage)
        {
            this.storage = storage;
        }

        [When(@"I serialize company '(.*)' and deserialize result to company '(.*)'")]
        public void WhenISerializeCompanyAndDeserializeResultToCompany(string key1, string key2)
        {
            var company = storage.Get<Company>(key1);
            var json = JsonConvert.SerializeObject(company);
            var company2 = JsonConvert.DeserializeObject<Company>(json);
            storage.Set(company2, key2);
        }


        [When(@"I remove '(.*)' department from company '(.*)'")]
        public void WhenIRemoveDepartmentFromCompany(string departmentName, string companyKey)
        {
            var company = storage.Get<Company>(companyKey);
            var department = company.Departments.First(x => x.Name == departmentName);
            company.Departments.Remove(department);
        }

        [When(@"I find department '(.*)' in company '(.*)'")]
        public void WhenIFindDepartmentInCompany(string departmentName, string companyKey)
        {
            var company = storage.Get<Company>(companyKey);
            var department = company.Departments.First(x => x.Name == departmentName);
            storage.Set(department);
        }

        [When(@"I add employee '(.*)' '(.*)' to department")]
        public void WhenIAddEmployeeToDepartment(string key, string name)
        {
            var department = storage.Get<Department>();
            var employee = new Employee { Name = name };
            department.Employees.Add(employee);
            storage.Set(employee, key);
        }

        [When(@"I add payment '(.*)' '(.*)' to employee '(.*)'")]
        public void WhenIAddPaymentToEmployee(DateTime date, decimal amount, string key)
        {
            var employee = storage.Get<Employee>(key);
            employee.Payments = employee.Payments ?? new List<Payment>();
            var payment = new Payment { Amount = amount, EffectiveDate = date };
            employee.Payments.Add(payment);
        }

        [When(@"I find employee '(.*)' '(.*)' in department")]
        public void WhenIFindEmployeeInDepartment(string key, string name)
        {
            var department = storage.Get<Department>();
            var employee = department.Employees.First(x => x.Name == name);
            storage.Set(employee, key);
        }

        [When(@"I remove payment by date '(.*)' from employee '(.*)'")]
        public void WhenIRemovePaymentByDateFromEmployee(DateTime date, string key)
        {
            var employee = storage.Get<Employee>(key);
            var payment = employee.Payments.First(x => x.EffectiveDate == date);
            employee.Payments.Remove(payment);
        }

        [When(@"I remove employee '(.*)' from department")]
        public void WhenIRemoveEmployeeFromDepartment(string name)
        {
            var department = storage.Get<Department>();
            var employee = department.Employees.First(x => x.Name == name);
            department.Employees.Remove(employee);
        }
    }
}
