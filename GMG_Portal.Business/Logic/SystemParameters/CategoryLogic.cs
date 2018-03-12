using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<Category> GetAllWithDeleted()
        {
            return _db.Categories.OrderBy(p => p.IsDeleted).ToList();
        }
        public List<Category> GetAll()
        {
            return _db.Categories.Where(p => p.IsDeleted != true).ToList();
        }
        public Category Get(int id)
        {
            return _db.Categories.Find(id);
        }
        private Category Save(Category category)
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
        public Category Insert(Category postedCategory)
        {

            var obj = new Category()
            { 
                Image = postedCategory.Image,
                IsDeleted = postedCategory.IsDeleted, 
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.Categories.Add(obj);
            _db.SaveChanges();
            var objTrasnlate = new Category_Translate();
            {
                foreach (var ownerName in postedCategory.TitleDictionary)
                {
                    objTrasnlate.Title = ownerName.Value;
                  //  objTrasnlate.Description= postedCategory.DescDictionary[ownerName.Key]; 
                    objTrasnlate.langId = ownerName.Key;
                    objTrasnlate.RecordId = obj.Id;
                    _db.Category_Translate.Add(objTrasnlate);
                    _db.SaveChanges();
                }
            }
            Category owner = Get(obj.Id);
            List<Category_Translate> categoryTranslate = GetTranslates(obj.Id);
            return Save(owner);
        }

        public List<Category_Translate> GetTranslates(int recordId)
        {
            return _db.Category_Translate.Where(x => x.RecordId == recordId).ToList();
        }

        public Category Edit(Category postedCategory)
        {
            Category category = Get(postedCategory.Id);

            List<Category_Translate> cTranslate = GetTranslates(postedCategory.Id);
            foreach (var categoryName in postedCategory.TitleDictionary)
            { 
                foreach (var categoryTranslate in cTranslate)
                {
                    if (categoryName.Key == categoryTranslate.langId)
                    {
                        categoryTranslate.Title = categoryName.Value;
                       // categoryTranslate.Description = postedCategory.DescDictionary[categoryName.Key]; 
                        _db.SaveChanges();
                    }
                }
            }

            category.Image = postedCategory.Image;
            category.IsDeleted = postedCategory.IsDeleted; 
            category.LastModificationTime = Parameters.CurrentDateTime;
            category.LastModifierUserId = Parameters.UserId;
            return Save(category);
        }
        public Category Delete(Category postedCategory)
        {
            Category category = Get(postedCategory.Id);
            if (_db.News.Any(p => p.CategoryId == postedCategory.Id && p.IsDeleted != true))
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
