using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class HomeSlidersLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public HomeSlidersLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<SystemParamater_HomeSlider> GetAllWithDeleted()
        {
            return _db.SystemParamater_HomeSlider.OrderBy(p => p.IsDeleted).ToList();
        }
        public List<SystemParamater_HomeSlider> GetAll()
        {
            return _db.SystemParamater_HomeSlider.Where(p => p.IsDeleted != true).ToList();
        }
        public SystemParamater_HomeSlider Get(int id)
        {
            return _db.SystemParamater_HomeSlider.Find(id);
        }
        private SystemParamater_HomeSlider Save(SystemParamater_HomeSlider homeSlider)
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
        public SystemParamater_HomeSlider Insert(SystemParamater_HomeSlider postedHomeSlider)
        {
            var language = new SystemParamater_HomeSlider()
            {
                DisplayValue = postedHomeSlider.DisplayValue,
                DisplayValueDesc = postedHomeSlider.DisplayValueDesc,
                Image= postedHomeSlider.Image,
                IsDeleted = postedHomeSlider.IsDeleted,
                Show = Parameters.Show,
                Rating = postedHomeSlider.Rating,
                URL = postedHomeSlider.URL,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId, 
            };
            _db.SystemParamater_HomeSlider.Add(language);
            return Save(language);
        }
        public SystemParamater_HomeSlider Edit(SystemParamater_HomeSlider postedLanguage)
        {
            SystemParamater_HomeSlider language = Get(postedLanguage.Id);
            language.DisplayValue = postedLanguage.DisplayValue;
            language.DisplayValueDesc = postedLanguage.DisplayValueDesc;
            language.Image = postedLanguage.Image;
            language.URL = postedLanguage.URL;
            language.Rating = postedLanguage.Rating;
            language.IsDeleted = postedLanguage.IsDeleted;
            language.Show = postedLanguage.Show; 
            language.LastModificationTime = Parameters.CurrentDateTime;
            language.LastModifierUserId = Parameters.UserId;
            return Save(language);
        }
        public SystemParamater_HomeSlider Delete(SystemParamater_HomeSlider postedLanguage)
        {
            SystemParamater_HomeSlider language = Get(postedLanguage.Id);
            if (_db.SystemParamater_HomeSlider.Any(p => p.Id == postedLanguage.Id && p.IsDeleted != true))
            {
                language.OperationStatus = "HasRelationship";
                return language;
            }

            language.IsDeleted = true;
            language.CreationTime = Parameters.CurrentDateTime;
            language.CreatorUserId = Parameters.UserId;
            return Save(language);
        }

    }
}
