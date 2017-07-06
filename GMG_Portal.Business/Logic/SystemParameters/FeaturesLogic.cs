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
        public List<Hotels_Features> GetAllWithDeleted()
        {
            return _db.Hotels_Features.OrderBy(p => p.IsDeleted).ToList();
        }
        public List<Hotels_Features> GetAll()
        {
            return _db.Hotels_Features.Where(p => p.IsDeleted != true).ToList();
        }
        public Hotels_Features Get(int id)
        {
            return _db.Hotels_Features.Find(id);
        }
        private Hotels_Features Save(Hotels_Features homeSlider)
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
        public Hotels_Features Insert(Hotels_Features postedHomeSlider)
        {

            var language = new Hotels_Features()
            {
                DisplayValue = postedHomeSlider.DisplayValue,
                DisplayValueDesc = postedHomeSlider.DisplayValueDesc,
                Icon = postedHomeSlider.Icon,
                IsDeleted = postedHomeSlider.IsDeleted,
                Show = Parameters.Show, 
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.Hotels_Features.Add(language);
            return Save(language);
        }
        public Hotels_Features Edit(Hotels_Features postedLanguage)
        {
            Hotels_Features language = Get(postedLanguage.Id);
            language.DisplayValue = postedLanguage.DisplayValue;
            language.DisplayValueDesc = postedLanguage.DisplayValueDesc;
            language.Icon = postedLanguage.Icon; 
            language.IsDeleted = postedLanguage.IsDeleted;
            language.Show = postedLanguage.Show;
            language.LastModificationTime = Parameters.CurrentDateTime;
            language.LastModifierUserId = Parameters.UserId;
            return Save(language);
        }
        public Hotels_Features Delete(Hotels_Features postedLanguage)
        {
            Hotels_Features language = Get(postedLanguage.Id);
            if (_db.Hotels_Features.Any(p => p.Id == postedLanguage.Id && p.IsDeleted != true))
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
