using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class CoreValuesLogic
   
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public CoreValuesLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<SystemParameters_CoreValues> GetAllWithDeleted()
        {
            return _db.SystemParameters_CoreValues.OrderBy(p => p.IsDeleted).ToList();
        }
        public List<SystemParameters_CoreValues> GetAll()
        {
            return _db.SystemParameters_CoreValues.Where(p => p.IsDeleted != true).ToList();
        }
        public SystemParameters_CoreValues Get(int id)
        {
            return _db.SystemParameters_CoreValues.Find(id);
        }
        private SystemParameters_CoreValues Save(SystemParameters_CoreValues obj)
        {
            try
            {
                _db.SaveChanges();
                obj.OperationStatus = "Succeded";
                return obj;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        obj.OperationStatus = "NameArMustBeUnique";
                        return obj;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        obj.OperationStatus = "NameEnMustBeUnique";
                        return obj;
                    }
                }
                throw;
            }
        }
        public SystemParameters_CoreValues Insert(SystemParameters_CoreValues postedCoreValue)
        { 
            var obj = new SystemParameters_CoreValues()
            {
                DisplayValue = postedCoreValue.DisplayValue,
                DisplayValueDesc = postedCoreValue.DisplayValueDesc,
                Icon = postedCoreValue.Icon,
                IsDeleted = postedCoreValue.IsDeleted,
                Show = Parameters.Show, 
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.SystemParameters_CoreValues.Add(obj);
            return Save(obj);
        }
        public SystemParameters_CoreValues Edit(SystemParameters_CoreValues postedCoreValue)
        {
            SystemParameters_CoreValues obj = Get(postedCoreValue.Id);
            obj.DisplayValue = postedCoreValue.DisplayValue;
            obj.DisplayValueDesc = postedCoreValue.DisplayValueDesc;
            obj.Icon = postedCoreValue.Icon; 
            obj.IsDeleted = postedCoreValue.IsDeleted;
            obj.Show = postedCoreValue.Show;
            obj.LastModificationTime = Parameters.CurrentDateTime;
            obj.LastModifierUserId = Parameters.UserId;
            return Save(obj);
        }
        public SystemParameters_CoreValues Delete(SystemParameters_CoreValues postedCoreValue)
        {
            SystemParameters_CoreValues obj = Get(postedCoreValue.Id);
            //if (_db.SystemParameters_CoreValues.Any(p => p.Id == postedCoreValue.Id && p.IsDeleted != true))
            //{
            //    //  obj.OperationStatus = "HasRelationship";
            //    return obj;
            //}

            obj.IsDeleted = true;
            obj.CreationTime = Parameters.CurrentDateTime;
            obj.CreatorUserId = Parameters.UserId;
            return Save(obj);
        }

    }
}
