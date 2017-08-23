using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class CoreValuesLogicTranslate

    {
        private readonly GMG_Portal_DBEntities1 _db;

        public CoreValuesLogicTranslate()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<SystemParameters_CoreValues_Translate> GetAllWithDeleted(string langId)
        {
            return _db.SystemParameters_CoreValues_Translate.Where(p => p.langId == langId).OrderBy(p => p.IsDeleted && p.langId == langId).ToList();
        }
        public List<SystemParameters_CoreValues_Translate> GetAll(string langId)
        {
            return _db.SystemParameters_CoreValues_Translate.Where(p => p.IsDeleted != true && p.langId == langId).ToList();
        }
        public SystemParameters_CoreValues_Translate Get(int id, string langId)
        {
            return _db.SystemParameters_CoreValues_Translate.FirstOrDefault(x => x.Id == id && x.langId == langId);
        }
        private SystemParameters_CoreValues_Translate Save(SystemParameters_CoreValues_Translate obj)
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
        public SystemParameters_CoreValues_Translate Insert(SystemParameters_CoreValues_Translate postedCoreValue)
        {

            var obj = new SystemParameters_CoreValues_Translate()
            {
                DisplayValue = postedCoreValue.DisplayValue,
                DisplayValueDesc = postedCoreValue.DisplayValueDesc,
              
                IsDeleted = postedCoreValue.IsDeleted,
                Show = Parameters.Show,
              
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
                langId = postedCoreValue.langId
            };
            _db.SystemParameters_CoreValues_Translate.Add(obj);
            return Save(obj);
        }
        public SystemParameters_CoreValues_Translate Edit(SystemParameters_CoreValues_Translate postedCoreValue)
        {
            SystemParameters_CoreValues_Translate obj = Get(postedCoreValue.Id, postedCoreValue.langId);
            obj.DisplayValue = postedCoreValue.DisplayValue;
            obj.DisplayValueDesc = postedCoreValue.DisplayValueDesc;
          
            obj.IsDeleted = postedCoreValue.IsDeleted;
            obj.Show = postedCoreValue.Show;
            obj.LastModificationTime = Parameters.CurrentDateTime;
            obj.LastModifierUserId = Parameters.UserId;
            return Save(obj);
        }
        public SystemParameters_CoreValues_Translate Delete(SystemParameters_CoreValues_Translate postedCoreValue)
        {
            SystemParameters_CoreValues_Translate obj = Get(postedCoreValue.Id, postedCoreValue.langId);
            if (_db.SystemParameters_CoreValues_Translate.Any(p => p.Id == postedCoreValue.Id && p.IsDeleted != true))
            {
                //  About.OperationStatus = "HasRelationship";
                return obj;
            }

            obj.IsDeleted = true;
            obj.CreationTime = Parameters.CurrentDateTime;
            obj.CreatorUserId = Parameters.UserId;
            return Save(obj);
        }
   

    }








}
