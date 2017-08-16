using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class FeaturesLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public FeaturesLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<SystemParameters_Features> GetAllWithDeleted()
        {
            return _db.SystemParameters_Features.OrderBy(p => p.IsDeleted).ToList();
        }
        public List<SystemParameters_Features> GetAll()
        {
            return _db.SystemParameters_Features.Where(p => p.IsDeleted != true).ToList();
        }
        public SystemParameters_Features Get(int id)
        {
            return _db.SystemParameters_Features.Find(id);
        }


        private SystemParameters_Features Save(SystemParameters_Features obj)
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
        public SystemParameters_Features Insert(SystemParameters_Features postedFeature)
        { 
            var obj = new SystemParameters_Features()
            {
                DisplayValue = postedFeature.DisplayValue,
                DisplayValueDesc = postedFeature.DisplayValueDesc,
                Icon = postedFeature.Icon,
                IsDeleted = postedFeature.IsDeleted,
                Show = Parameters.Show, 
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.SystemParameters_Features.Add(obj);
            return Save(obj);
        }
        public SystemParameters_Features Edit(SystemParameters_Features postedFeature)
        {
            SystemParameters_Features obj = Get(postedFeature.Id);
            obj.DisplayValue = postedFeature.DisplayValue;
            obj.DisplayValueDesc = postedFeature.DisplayValueDesc;
            obj.Icon = postedFeature.Icon; 
            obj.IsDeleted = postedFeature.IsDeleted;
            obj.Show = postedFeature.Show;
            obj.LastModificationTime = Parameters.CurrentDateTime;
            obj.LastModifierUserId = Parameters.UserId;
            return Save(obj);
        }
        public SystemParameters_Features Delete(SystemParameters_Features postedFeature)
        {
            SystemParameters_Features obj = Get(postedFeature.Id);
            //if (_db.SystemParameters_Features.Any(p => p.Id == postedFeature.Id && p.IsDeleted != true))
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
