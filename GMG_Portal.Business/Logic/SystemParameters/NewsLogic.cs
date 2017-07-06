using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class NewsLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public NewsLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<SystemParameter_News> GetAllWithDeleted()
        {
            return _db.SystemParameter_News.OrderBy(p => p.IsDeleted).ToList();
        }
        public List<SystemParameter_News> GetAll()
        {
            return _db.SystemParameter_News.Where(p => p.IsDeleted != true).ToList();
        }
        public SystemParameter_News Get(int id)
        {
            return _db.SystemParameter_News.Find(id);
        }
        private SystemParameter_News Save(SystemParameter_News homeSlider)
        {
            try
            {
                _db.SaveChanges();
                homeSlider.OperationStatus = "Succeded";
                return homeSlider;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        homeSlider.OperationStatus = "NameArMustBeUnique";
                        return homeSlider;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        homeSlider.OperationStatus = "NameEnMustBeUnique";
                        return homeSlider;
                    }
                }
                throw;
            }
        }
        public SystemParameter_News Insert(SystemParameter_News postedHomeSlider)
        {

            var language = new SystemParameter_News()
            {
                DisplayValue = postedHomeSlider.DisplayValue,
                DisplayValueDesc = postedHomeSlider.DisplayValueDesc,
                Image= postedHomeSlider.Image,
                IsDeleted = postedHomeSlider.IsDeleted,
                Show = Parameters.Show, 
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId, 
            };
            _db.SystemParameter_News.Add(language);
            return Save(language);
        }
        public SystemParameter_News Edit(SystemParameter_News postedLanguage)
        {
            SystemParameter_News language = Get(postedLanguage.Id);
            language.DisplayValue = postedLanguage.DisplayValue;
            language.DisplayValueDesc = postedLanguage.DisplayValueDesc;
            language.Image = postedLanguage.Image; 
            language.IsDeleted = postedLanguage.IsDeleted;
            language.Show = postedLanguage.Show; 
            language.LastModificationTime = Parameters.CurrentDateTime;
            language.LastModifierUserId = Parameters.UserId;
            return Save(language);
        }
        public SystemParameter_News Delete(SystemParameter_News postedLanguage)
        {
            SystemParameter_News language = Get(postedLanguage.Id);
            if (_db.SystemParameter_News.Any(p => p.Id == postedLanguage.Id && p.IsDeleted != true))
            {
                  //  language.OperationStatus = "HasRelationship";
                return language;
            }

            language.IsDeleted = true;
            language.CreationTime = Parameters.CurrentDateTime;
            language.CreatorUserId = Parameters.UserId;
            return Save(language);
        }

    }
}
