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
        GMG_Portal_DBEntities DB;

        public DepartmentLogic()
        {
            DB = new GMG_Portal_DBEntities();
        }

        public List<Departments> GetAll()
        {
            return null;

            //return DB.Departments.Where(p => p.IsDeleted!=true).ToList();

        }
        public List<Departments> GetAllWithDeleted()
        {

            return null;
            //return DB.Departments.OrderBy(p => p.IsDeleted).ToList();

        }

        public Departments Get(int ID)
        {
            return null;
           // return DB.Departments.Find(ID);
        }
        private Departments Save(Departments Department)
        {
            try
            {
                DB.SaveChanges();
                Department.OperationStatus = "Succeded";
                return Department;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Departments_Ar"))
                    {
                        Department.OperationStatus = "NameArMustBeUnique";
                        return Department;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Departments_En"))
                    {
                        Department.OperationStatus = "NameEnMustBeUnique";
                        return Department;
                    }
                }
                throw;
            }
        }

        public Departments Insert(Departments PostedDepartments)
        {
            return null;
            //var Department = new Departments()
            //{
            //    NameAr = PostedDepartments.NameAr,
            //    NameEn = PostedDepartments.NameEn,
            //    IsDeleted = false

            //};
            //DB.Departments.Add(Department);
            //return Save(Department);
        }
        public Departments Edit(Departments PostedDepartment)
        {
            return null;
            //Departments Department = Get(PostedDepartment.ID);
            //Department.NameEn = PostedDepartment.NameEn;
            //Department.NameAr = PostedDepartment.NameAr;
            //Department.IsDeleted = PostedDepartment.IsDeleted;

            //return Save(Department);
        }
        public Departments Delete(Departments PostedDepartment)
        {
            return null;
            //Departments Department = Get(PostedDepartment.ID);
           
            ////Department.NameAr = Department.ID + "-" + Department.NameAr;
            ////Department.NameEn = Department.ID + "-" + Department.NameEn;
            //Department.IsDeleted = true;
            //return Save(Department);
        }


    }
}
