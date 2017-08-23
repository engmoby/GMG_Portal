using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class FeaturesLogicTranslate
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public FeaturesLogicTranslate()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<SystemParameters_Features_Translate> GetAllWithDeleted(string langId)
        {
            return _db.SystemParameters_Features_Translate.Where(p => p.langId == langId).OrderBy(p => p.IsDeleted && p.langId == langId).ToList();
        }
        public List<SystemParameters_Features_Translate> GetAll(string langId)
        {
            return _db.SystemParameters_Features_Translate.Where(p => p.IsDeleted != true && p.langId == langId).ToList();
        }
        public SystemParameters_Features_Translate Get(int id, string langId)
        {
            return _db.SystemParameters_Features_Translate.FirstOrDefault(x => x.Id == id && x.langId == langId);
        }
        public List<SystemParameters_Features_Translate> GetAllByTake6(string langId)
        {
            var returnList = new List<SystemParameters_Features_Translate>();
            var featureList = _db.SystemParameters_Features_Translate.Where(p => p.IsDeleted != true && p.langId== langId).ToList();
            var random = featureList.OrderBy(x => Guid.NewGuid()).Take(6);
            foreach (var systemParametersFeaturese in random)
            {
                returnList.Add(new SystemParameters_Features_Translate
                {
                    Id = systemParametersFeaturese.Id,
                    Icon = systemParametersFeaturese.Icon,
                    DisplayValue = systemParametersFeaturese.DisplayValue,
                    DisplayValueDesc = systemParametersFeaturese.DisplayValueDesc
                });
            }
            return returnList;

        }



        private SystemParameters_Features_Translate Save(SystemParameters_Features_Translate obj)
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
        public SystemParameters_Features_Translate Insert(SystemParameters_Features_Translate postedFeature)
        {
            var obj = new SystemParameters_Features_Translate()
            {
                DisplayValue = postedFeature.DisplayValue,
                DisplayValueDesc = postedFeature.DisplayValueDesc,
                Icon = postedFeature.Icon,
                IsDeleted = postedFeature.IsDeleted,
                Show = Parameters.Show,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
                langId = postedFeature.langId
            };
            _db.SystemParameters_Features_Translate.Add(obj);
            return Save(obj);
        }
        public SystemParameters_Features_Translate Edit(SystemParameters_Features_Translate postedFeature)
        {
            SystemParameters_Features_Translate obj = Get(postedFeature.Id,postedFeature.langId);
            obj.DisplayValue = postedFeature.DisplayValue;
            obj.DisplayValueDesc = postedFeature.DisplayValueDesc;
            obj.Icon = postedFeature.Icon;
            obj.IsDeleted = postedFeature.IsDeleted;
            obj.Show = postedFeature.Show;
            obj.LastModificationTime = Parameters.CurrentDateTime;
            obj.LastModifierUserId = Parameters.UserId;
            return Save(obj);
        }
        public SystemParameters_Features_Translate Delete(SystemParameters_Features_Translate postedFeature)
        {
            SystemParameters_Features_Translate obj = Get(postedFeature.Id,postedFeature.langId);
            //if (_db.SystemParameters_Features_Translate.Any(p => p.Id == postedFeature.Id && p.IsDeleted != true))
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
