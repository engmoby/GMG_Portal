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
        public List<SystemParameters_Features> GetAllByTake6()
        {
            var returnList = new List<SystemParameters_Features>();
            var featureList = _db.SystemParameters_Features.Where(p => p.IsDeleted != true).ToList();
            var random = featureList.OrderBy(x => Guid.NewGuid()).Take(6);
            foreach (var systemParametersFeaturese in random)
            {
                returnList.Add(new SystemParameters_Features
                {
                    Id = systemParametersFeaturese.Id,
                    Icon = systemParametersFeaturese.Icon,
                    DisplayValue = systemParametersFeaturese.DisplayValue,
                    DisplayValueDesc = systemParametersFeaturese.DisplayValueDesc
                });
            }
            return returnList;

        }
        public List<SystemParameters_Features> GetAll()
        {
            return _db.SystemParameters_Features.Where(p => p.IsDeleted != true).ToList();
        }
        public SystemParameters_Features Get(int id)
        {
            return _db.SystemParameters_Features.Find(id);
        }
        private SystemParameters_Features Save(SystemParameters_Features homeSlider)
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
        public SystemParameters_Features Insert(SystemParameters_Features postedHomeSlider)
        {
            var language = new SystemParameters_Features()
            {
                DisplayValue = postedHomeSlider.DisplayValue,
                DisplayValueDesc = postedHomeSlider.DisplayValueDesc,
                Icon = postedHomeSlider.Icon,
                IsDeleted = postedHomeSlider.IsDeleted,
                Show = Parameters.Show,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.SystemParameters_Features.Add(language);
            return Save(language);
        }
        public SystemParameters_Features Edit(SystemParameters_Features postedLanguage)
        {
            SystemParameters_Features language = Get(postedLanguage.Id);
            language.DisplayValue = postedLanguage.DisplayValue;
            language.DisplayValueDesc = postedLanguage.DisplayValueDesc;
            language.Icon = postedLanguage.Icon;
            language.IsDeleted = postedLanguage.IsDeleted;
            language.Show = postedLanguage.Show;
            language.LastModificationTime = Parameters.CurrentDateTime;
            language.LastModifierUserId = Parameters.UserId;
            return Save(language);
        }
        public SystemParameters_Features Delete(SystemParameters_Features postedLanguage)
        {
            SystemParameters_Features language = Get(postedLanguage.Id);
            //if (_db.SystemParameters_Features.Any(p => p.Id == postedLanguage.Id && p.IsDeleted != true))
            //{
            //    //  language.OperationStatus = "HasRelationship";
            //    return language;
            //}

            language.IsDeleted = true;
            language.CreationTime = Parameters.CurrentDateTime;
            language.CreatorUserId = Parameters.UserId;
            return Save(language);
        }

    }
}
