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
    public class Notify
    {
        GMG_Portal_DBEntities1 _db;

        public Notify()
        {
            _db = new GMG_Portal_DBEntities1();
        }

        public List<SystemParameters_Notify> GetAll()
        {
            return _db.SystemParameters_Notify.Where(p => p.IsDeleted != true).ToList();
        }
        public List<SystemParameters_Notify> GetAllWithDeleted()
        {
            return _db.SystemParameters_Notify.OrderBy(p => p.IsDeleted).ToList();
        }
        public SystemParameters_Notify Get(int id)
        {
            return _db.SystemParameters_Notify.Find(id);
        }
        public List<SystemParameters_Notify> GetNotifyByDepId(int id)
        {
            return _db.SystemParameters_Notify.Where(x => x.DepartmentId == id && x.IsDeleted == false).ToList();
        }
        private SystemParameters_Notify Save(SystemParameters_Notify notify)
        {
            try
            {
                _db.SaveChanges();
                notify.OperationStatus = "Succeded";
                return notify;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {

                }
                throw;
            }
        }
        public SystemParameters_Notify Insert(SystemParameters_Notify postedDepartments)
        {
            var notify = new SystemParameters_Notify()
            {
                DisplayValue = postedDepartments.DisplayValue,
                IsDeleted = false
            };
            _db.SystemParameters_Notify.Add(notify);
            return Save(notify);
        }
        public SystemParameters_Notify Edit(SystemParameters_Notify postedDepartment)
        {
            SystemParameters_Notify notify = Get(postedDepartment.Id);
            notify.DisplayValue = postedDepartment.DisplayValue;
            notify.IsDeleted = postedDepartment.IsDeleted;
            return Save(notify);
        }
        public SystemParameters_Notify Delete(SystemParameters_Notify postedDepartment)
        {
            SystemParameters_Notify Notify = Get(postedDepartment.Id);

            Notify.DisplayValue = Notify.DisplayValue;
            Notify.IsDeleted = true;
            return Save(Notify);
        }


    }
}
