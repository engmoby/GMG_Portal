using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using AutoMapper;
using System.Data;
using System.Security.AccessControl;
using Heloper;


namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class NotifyLogic
    {
        GMG_Portal_DBEntities1 _db;

        public NotifyLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }

        public List<SystemParameters_Notify> GetAll()
        {
            return _db.SystemParameters_Notify.Where(p => p.IsDeleted != true).ToList();
        }
        public List<SystemParameters_Notify> GetAllWithDeleted()
        {

            var returnList = new List<SystemParameters_Notify>();
            var list= _db.SystemParameters_Notify.OrderBy(p => p.IsDeleted).ToList();
            foreach (var systemParametersNotify in list)
            {
                var departmentOnj= _db.SystemParameters_NotifyDepartment.FirstOrDefault(p => p.Id== systemParametersNotify.DepartmentId);

                if (departmentOnj != null)
                    returnList.Add(new SystemParameters_Notify
                    {
                        Id = systemParametersNotify.Id,
                        DisplayValue = systemParametersNotify.DisplayValue,
                        DepartmentName = departmentOnj.DisplayValue,
                        DepartmentId =  systemParametersNotify.DepartmentId,
                    });
            }
            return returnList;
        }
        public SystemParameters_Notify Get(int id)
        {

            //var obj = _db.SystemParameters_Notify.Find(id);
            //obj.DepartmentName = _db.SystemParameters_NotifyDepartment.FirstOrDefault(p => p.Id == systemParametersNotify.DepartmentId);
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
                DepartmentId = postedDepartments.DepartmentId,
                DepartmentName = postedDepartments.DepartmentName,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
                IsDeleted = false
            };
            _db.SystemParameters_Notify.Add(notify);
            return Save(notify);
        }
        public SystemParameters_Notify Edit(SystemParameters_Notify postedDepartment)
        {
            SystemParameters_Notify notify = Get(postedDepartment.Id);
            notify.DisplayValue = postedDepartment.DisplayValue;
            notify.DepartmentId = postedDepartment.DepartmentId;
        //    notify.DepartmentName = postedDepartment.DepartmentName;
            notify.LastModificationTime = Parameters.CurrentDateTime;
            notify.LastModifierUserId = Parameters.UserId;
            notify.IsDeleted = postedDepartment.IsDeleted;
            return Save(notify);
        }
        public SystemParameters_Notify Delete(SystemParameters_Notify postedDepartment)
        {
            SystemParameters_Notify Notify = Get(postedDepartment.Id);
            Notify.DepartmentName = postedDepartment.DepartmentName;
            Notify.DisplayValue = Notify.DisplayValue;
            Notify.DepartmentId = postedDepartment.DepartmentId;
            Notify.IsDeleted = true;
            return Save(Notify);
        }


    }
}
