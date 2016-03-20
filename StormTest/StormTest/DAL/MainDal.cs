using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using StormTest.Schema;
using TestingContextCore.PublicMembers;

namespace StormTest.DAL
{
    using Storm;
    using StormTest.StormEntities;
    using company = StormTest.Schema.company;
    using department = StormTest.Schema.department;

    public class MainDal
    {
        private readonly Storage storage;

        public MainDal(Storage storage)
        {
            this.storage = storage;
        }

        private StormTestDB CreateContext()
        {
            return storage.Get<StormTestDB>() ?? new StormTestDB();
        }

        public void InsertCompanies(List<company> dalCompanies)
        {
            var db = CreateContext();
            dalCompanies.ForEach(x => InsertCompany(db, x));
        }

        private void InsertCompany(StormTestDB db, company company)
        {
            return;
            var id = Convert.ToInt32(db.InsertWithIdentity(company));
            foreach (var department in company.fkdepartment1)
            {
                department.company_id = id;
                InsertDepartment(db, department);
            }
        }

        private void InsertDepartment(StormTestDB db, department department)
        {
            var id = Convert.ToInt32(db.InsertWithIdentity(department));
            foreach (var employee in department.fkemployee1)
            {
                employee.department_id = id;
                InsertEmployee(db, employee);
            }
        }

        private void InsertEmployee(StormTestDB db, employee employee)
        {
            var id = Convert.ToInt32(db.InsertWithIdentity(employee));
            if (employee.fkpayment1 == null)
            {
                return;
            }

            foreach (var payment in employee.fkpayment1)
            {
                payment.employee_id = id;
                db.Insert(payment);
            }
        }

        public company LoadCompany(string name)
        {
            using (var stormdb = new StormDb())
            {
                var query = stormdb.companies.Where(x => x.name == name);
                var companies = stormdb.Get(query);
                var a = query.ToList();
                var departments = companies[0].Departments;
            }


            var db = CreateContext();
            var company = db.companies.First(x => x.name == name);
            company.fkdepartment1 = LoadDepartments(db, company);
            return company;
        }

        private IEnumerable<department> LoadDepartments(StormTestDB db, company company)
        {
            var deps = db.departments.Where(x => x.company_id == company.company_id).ToList();
            deps.ForEach(x => x.fkemployee1 = LoadEmployees(db, x));
            return deps;
        }

        private IEnumerable<employee> LoadEmployees(StormTestDB db, department department)
        {
            var emps = db.employees.Where(x => x.department_id == department.department_id).ToList();
            emps.ForEach(x => x.fkpayment1 = LoadPayments(db, x));
            return emps;
        }

        private IEnumerable<payment> LoadPayments(StormTestDB db, employee employee)
        {
            return db.payments.Where(x => x.employee_id == employee.employee_id);
        }


        public void UpdateCompany(company company)
        {
            // todo1
            var existing = LoadCompany(company.name);
        }
    }
}
