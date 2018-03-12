using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<Feature> GetAllWithDeleted()
        {
            return _db.Features.OrderBy(p => p.IsDeleted).ToList();
        }
        public IQueryable<Feature> GetAllByTake6()
        {
            var returnList = new List<Feature>();
           return _db.Features.Where(p => p.IsDeleted != true).OrderBy(r => Guid.NewGuid()).Take(6);
            //var random = featureList.OrderBy(x => Guid.NewGuid()).Take(6);
            //foreach (var systemParametersFeaturese in random)
            //{
            //    returnList.Add(new Feature
            //    {
            //        Id = systemParametersFeaturese.Id,
            //        Icon = systemParametersFeaturese.Icon,
            //        //DisplayValue = systemParametersFeaturese.DisplayValue,
            //        //DisplayValueDesc = systemParametersFeaturese.DisplayValueDesc
            //    });
            //}
            //return returnList;

        }
        public List<Feature> GetAll()
        {
            return _db.Features.Where(p => p.IsDeleted != true).ToList();
        }
        public Feature Get(int id)
        {
            return _db.Features.Find(id);
        }
        public List<Features_Translate> GetTranslates(int recordId)
        {
            return _db.Features_Translate.Where(x => x.RecordId == recordId).ToList();
        }
        private Feature Save(Feature obj)
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
        public Feature Insert(Feature postedFeature)
        {
            var obj = new Feature()
            { 
                Icon = postedFeature.Icon,
                IsDeleted = postedFeature.IsDeleted, 
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.Features.Add(obj);
            _db.SaveChanges();
            var objTrasnlate = new Features_Translate();
            {
                foreach (var title in postedFeature.TitleDictionary)
                {
                    objTrasnlate.Title = title.Value;
                    objTrasnlate.Description = postedFeature.DescDictionary[title.Key];
                    objTrasnlate.langId = title.Key;
                    objTrasnlate.RecordId = obj.Id;
                    _db.Features_Translate.Add(objTrasnlate);
                    _db.SaveChanges();
                }
            }
            Feature feature = Get(obj.Id);
            List<Features_Translate> featureTranslate = GetTranslates(obj.Id);
            return Save(feature); 
        }
        public Feature Edit(Feature postedFeature)
        {
            Feature obj = Get(postedFeature.Id);
            List<Features_Translate> featureTranslate = GetTranslates(postedFeature.Id);
            foreach (var title in postedFeature.TitleDictionary)
            { 
                foreach (var objTranslate in featureTranslate)
                {
                    if (title.Key == objTranslate.langId)
                    {
                        objTranslate.Title = title.Value;
                        objTranslate.Description = postedFeature.DescDictionary[title.Key];
                        _db.SaveChanges();
                    }
                }
            }
            obj.Icon = postedFeature.Icon;
            obj.IsDeleted = postedFeature.IsDeleted; 
            obj.LastModificationTime = Parameters.CurrentDateTime;
            obj.LastModifierUserId = Parameters.UserId;
            return Save(obj);
        }
        public Feature Delete(Feature postedFeature)
        {
            Feature obj = Get(postedFeature.Id);
            //if (_db.SystemParameters_Features.Any(p => p.Id == postedFeature.Id && p.IsDeleted != true))
            //{
            //    //  obj.OperationStatus = "HasRelationship";
            //    return obj;
            //}

            obj.IsDeleted = true;
            obj.CreationTime = Parameters.CurrentDateTime;
            obj.CreatorUserId = Parameters.UserId;
            _db.SaveChanges();
            return Save(obj);
        }

    }
}
