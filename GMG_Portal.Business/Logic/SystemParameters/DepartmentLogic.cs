using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using AutoMapper;
using System.Data;


namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class DepartmentLogic
    {
        GMG_Portal_DBEntities1 _db;

        public DepartmentLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }

        public List<SystemParameters_NotifyDepartment> GetAll()
        {
            return _db.SystemParameters_NotifyDepartment.Where(p => p.IsDeleted != true).ToList();
        }
        public List<SystemParameters_NotifyDepartment> GetAllWithDeleted()
        {
            return _db.SystemParameters_NotifyDepartment.OrderBy(p => p.IsDeleted).ToList();
        }

        public SystemParameters_NotifyDepartment Get(int id)
        {
            return _db.SystemParameters_NotifyDepartment.Find(id);
        }
        public SystemParameters_NotifyDepartment GetDepartmentByName(string department)
        {
            return _db.SystemParameters_NotifyDepartment.FirstOrDefault(x => x.DisplayValue == department );
        }
        
        private SystemParameters_NotifyDepartment Save(SystemParameters_NotifyDepartment department)
        {
            try
            {
                _db.SaveChanges();
                department.OperationStatus = "Succeded";
                return department;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {

                }
                throw;
            }
        }

        public SystemParameters_NotifyDepartment Insert(SystemParameters_NotifyDepartment postedDepartments)
        {
            var department = new SystemParameters_NotifyDepartment()
            {
                DisplayValue = postedDepartments.DisplayValue,
                IsDeleted = false

            };
            _db.SystemParameters_NotifyDepartment.Add(department);
            return Save(department);
        }
        public SystemParameters_NotifyDepartment Edit(SystemParameters_NotifyDepartment postedDepartment)
        {
            SystemParameters_NotifyDepartment department = Get(postedDepartment.Id);
            department.DisplayValue = postedDepartment.DisplayValue;
            department.IsDeleted = postedDepartment.IsDeleted;
            return Save(department);
        }
        public SystemParameters_NotifyDepartment Delete(SystemParameters_NotifyDepartment postedDepartment)
        {
            SystemParameters_NotifyDepartment department = Get(postedDepartment.Id);

            department.DisplayValue = department.DisplayValue;
            department.IsDeleted = true;
            return Save(department);
        }


    }
}
