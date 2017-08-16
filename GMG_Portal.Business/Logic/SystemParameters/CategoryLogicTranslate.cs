using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class CategoryLogicTranslate
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public CategoryLogicTranslate()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<SystemParameters_Category_Translate> GetAllWithDeleted(string langId)
        {
            return _db.SystemParameters_Category_Translate.OrderBy(p => p.IsDeleted && p.langId == langId).ToList();
        }
        public List<SystemParameters_Category_Translate> GetAll(string langId)
        {
            return _db.SystemParameters_Category_Translate.Where(p => p.IsDeleted != true && p.langId == langId).ToList();
        }
        public SystemParameters_Category_Translate Get(int id, string langId)
        {
            return _db.SystemParameters_Category_Translate.FirstOrDefault(x => x.Id == id && x.langId == langId);
        }
        private SystemParameters_Category_Translate Save(SystemParameters_Category_Translate obj)
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
        public SystemParameters_Category_Translate Insert(SystemParameters_Category_Translate postedCategory)
        {

            var obj = new SystemParameters_Category_Translate()
            {
                DisplayValue = postedCategory.DisplayValue,
                DisplayValueDesc = postedCategory.DisplayValueDesc,
                Image = postedCategory.Image,
                IsDeleted = postedCategory.IsDeleted,
                Show = Parameters.Show,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
                langId = postedCategory.langId,
            };
            _db.SystemParameters_Category_Translate.Add(obj);
            return Save(obj);
        }
        public SystemParameters_Category_Translate Edit(SystemParameters_Category_Translate postedCategory)
        {
            SystemParameters_Category_Translate obj = Get(postedCategory.Id,postedCategory.langId);
            obj.DisplayValue = postedCategory.DisplayValue;
            obj.DisplayValueDesc = postedCategory.DisplayValueDesc;
            obj.Image = postedCategory.Image;
            obj.IsDeleted = postedCategory.IsDeleted;
            obj.Show = postedCategory.Show;
            obj.LastModificationTime = Parameters.CurrentDateTime;
            obj.LastModifierUserId = Parameters.UserId;
            return Save(obj);
        }
        public SystemParameters_Category_Translate Delete(SystemParameters_Category_Translate postedCategory)
        {
            SystemParameters_Category_Translate obj = Get(postedCategory.Id, postedCategory.langId);
            if (_db.SystemParameters_News.Any(p => p.CategoryId == postedCategory.Id && p.IsDeleted != true))
            {
                obj.OperationStatus = "HasRelationship";
                return obj;
            }

            obj.IsDeleted = true;
            obj.CreationTime = Parameters.CurrentDateTime;
            obj.CreatorUserId = Parameters.UserId;
            return Save(obj);
        }

    }
}
