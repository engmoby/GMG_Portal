using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class AboutLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public AboutLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<SystemParameter_About> GetAllWithDeleted()
        {
            return _db.SystemParameter_About.OrderBy(p => p.IsDeleted).ToList();
        }
        public List<SystemParameter_About> GetAll()
        {
            return _db.SystemParameter_About.Where(p => p.IsDeleted != true).ToList();
        }
        public SystemParameter_About Get(int id)
        {
            return _db.SystemParameter_About.Find(id);
        }
        private SystemParameter_About Save(SystemParameter_About homeSlider)
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
        public SystemParameter_About Insert(SystemParameter_About postedHomeSlider)
        {

            var language = new SystemParameter_About()
            {
                DisplayValue = postedHomeSlider.DisplayValue,
                DisplayValueDesc = postedHomeSlider.DisplayValueDesc,
                Image= postedHomeSlider.Image,
                IsDeleted = postedHomeSlider.IsDeleted,
                Show = Parameters.Show, 
                Url = postedHomeSlider.Url,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId, 
            };
            _db.SystemParameter_About.Add(language);
            return Save(language);
        }
        public SystemParameter_About Edit(SystemParameter_About postedLanguage)
        {
            SystemParameter_About language = Get(postedLanguage.Id);
            language.DisplayValue = postedLanguage.DisplayValue;
            language.DisplayValueDesc = postedLanguage.DisplayValueDesc;
            language.Image = postedLanguage.Image;
            language.Url= postedLanguage.Url; 
            language.IsDeleted = postedLanguage.IsDeleted;
            language.Show = postedLanguage.Show; 
            language.LastModificationTime = Parameters.CurrentDateTime;
            language.LastModifierUserId = Parameters.UserId;
            return Save(language);
        }
        public SystemParameter_About Delete(SystemParameter_About postedLanguage)
        {
            SystemParameter_About language = Get(postedLanguage.Id);
            if (_db.SystemParameter_About.Any(p => p.Id == postedLanguage.Id && p.IsDeleted != true))
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
