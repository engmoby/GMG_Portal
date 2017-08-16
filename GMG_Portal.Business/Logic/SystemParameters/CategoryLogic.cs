using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class CategoryLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public CategoryLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<SystemParameters_Category> GetAllWithDeleted()
        {
            return _db.SystemParameters_Category.OrderBy(p => p.IsDeleted).ToList();
        }
        public List<SystemParameters_Category> GetAll()
        {
            return _db.SystemParameters_Category.Where(p => p.IsDeleted != true).ToList();
        }
        public SystemParameters_Category Get(int id)
        {
            return _db.SystemParameters_Category.Find(id);
        }
        private SystemParameters_Category Save(SystemParameters_Category category)
        {
            try
            {
                _db.SaveChanges();
                category.OperationStatus = "Succeded";
                return category;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        category.OperationStatus = "NameArMustBeUnique";
                        return category;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        category.OperationStatus = "NameEnMustBeUnique";
                        return category;
                    }
                }
                throw;
            }
        }
        public SystemParameters_Category Insert(SystemParameters_Category postedCategory)
        {

            var category = new SystemParameters_Category()
            {
                DisplayValue = postedCategory.DisplayValue,
                DisplayValueDesc = postedCategory.DisplayValueDesc,
                Image = postedCategory.Image,
                IsDeleted = postedCategory.IsDeleted,
                Show = Parameters.Show,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.SystemParameters_Category.Add(category);
            return Save(category);
        }
        public SystemParameters_Category Edit(SystemParameters_Category postedCategory)
        {
            SystemParameters_Category category = Get(postedCategory.Id);
            category.DisplayValue = postedCategory.DisplayValue;
            category.DisplayValueDesc = postedCategory.DisplayValueDesc;
            category.Image = postedCategory.Image;
            category.IsDeleted = postedCategory.IsDeleted;
            category.Show = postedCategory.Show;
            category.LastModificationTime = Parameters.CurrentDateTime;
            category.LastModifierUserId = Parameters.UserId;
            return Save(category);
        }
        public SystemParameters_Category Delete(SystemParameters_Category postedCategory)
        {
            SystemParameters_Category category = Get(postedCategory.Id);
            if (_db.SystemParameters_News.Any(p => p.CategoryId == postedCategory.Id && p.IsDeleted != true))
            {
                category.OperationStatus = "HasRelationship";
                return category;
            }

            category.IsDeleted = true;
            category.CreationTime = Parameters.CurrentDateTime;
            category.CreatorUserId = Parameters.UserId;
            return Save(category);
        }

    }
}
