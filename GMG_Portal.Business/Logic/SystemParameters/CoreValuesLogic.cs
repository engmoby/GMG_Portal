using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class CoreValuesLogic
   
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public CoreValuesLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<SystemParameters_CoreValues> GetAllWithDeleted()
        {
            return _db.SystemParameters_CoreValues.OrderBy(p => p.IsDeleted).ToList();
        }
        public List<SystemParameters_CoreValues> GetAll()
        {
            return _db.SystemParameters_CoreValues.Where(p => p.IsDeleted != true).ToList();
        }
        public SystemParameters_CoreValues Get(int id)
        {
            return _db.SystemParameters_CoreValues.Find(id);
        }
        private SystemParameters_CoreValues Save(SystemParameters_CoreValues homeSlider)
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
        public SystemParameters_CoreValues Insert(SystemParameters_CoreValues postedHomeSlider)
        { 
            var language = new SystemParameters_CoreValues()
            {
                DisplayValue = postedHomeSlider.DisplayValue,
                DisplayValueDesc = postedHomeSlider.DisplayValueDesc,
                Icon = postedHomeSlider.Icon,
                IsDeleted = postedHomeSlider.IsDeleted,
                Show = Parameters.Show, 
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.SystemParameters_CoreValues.Add(language);
            return Save(language);
        }
        public SystemParameters_CoreValues Edit(SystemParameters_CoreValues postedLanguage)
        {
            SystemParameters_CoreValues language = Get(postedLanguage.Id);
            language.DisplayValue = postedLanguage.DisplayValue;
            language.DisplayValueDesc = postedLanguage.DisplayValueDesc;
            language.Icon = postedLanguage.Icon; 
            language.IsDeleted = postedLanguage.IsDeleted;
            language.Show = postedLanguage.Show;
            language.LastModificationTime = Parameters.CurrentDateTime;
            language.LastModifierUserId = Parameters.UserId;
            return Save(language);
        }
        public SystemParameters_CoreValues Delete(SystemParameters_CoreValues postedLanguage)
        {
            SystemParameters_CoreValues language = Get(postedLanguage.Id);
            //if (_db.SystemParameters_CoreValues.Any(p => p.Id == postedLanguage.Id && p.IsDeleted != true))
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
