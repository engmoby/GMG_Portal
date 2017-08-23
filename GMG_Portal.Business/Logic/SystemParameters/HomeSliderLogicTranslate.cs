using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class HomeSliderLogicTranslate
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public HomeSliderLogicTranslate()
        {
            _db = new GMG_Portal_DBEntities1();
        }

        //Back and Front  Fetch Logic 
        public List<SystemParameters_HomeSlider_Translate> GetAllWithDeleted(string langId)
        {
            return _db.SystemParameters_HomeSlider_Translate.Where(p => p.langId == langId).OrderBy(p => p.IsDeleted).ToList();
        }
        public List<SystemParameters_HomeSlider_Translate> GetAll(string langId)
        {
            return _db.SystemParameters_HomeSlider_Translate.Where(p => p.IsDeleted != true && p.langId == langId).ToList();
        }

        //Crud Logic 

        public SystemParameters_HomeSlider_Translate Get(int id, string langId)
        {
            return _db.SystemParameters_HomeSlider_Translate.FirstOrDefault(x => x.Id == id && x.langId == langId);
        }
        private SystemParameters_HomeSlider_Translate Save(SystemParameters_HomeSlider_Translate obj)
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
        public SystemParameters_HomeSlider_Translate Insert(SystemParameters_HomeSlider_Translate postedHomeSlider)
        {

            var obj = new SystemParameters_HomeSlider_Translate()
            {
                DisplayValue = postedHomeSlider.DisplayValue,
                DisplayValueDesc = postedHomeSlider.DisplayValueDesc,
                Image = postedHomeSlider.Image,
                IsDeleted = postedHomeSlider.IsDeleted,
                Show = Parameters.Show,
                Rating = postedHomeSlider.Rating,
                URL = postedHomeSlider.URL,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
                langId = postedHomeSlider.langId,
            };
            _db.SystemParameters_HomeSlider_Translate.Add(obj);
            return Save(obj);
        }
        public SystemParameters_HomeSlider_Translate Edit(SystemParameters_HomeSlider_Translate postedhomeSlider)
        {
            SystemParameters_HomeSlider_Translate obj = Get(postedhomeSlider.Id, postedhomeSlider.langId);
            obj.DisplayValue = postedhomeSlider.DisplayValue;
            obj.DisplayValueDesc = postedhomeSlider.DisplayValueDesc;
            obj.Image = postedhomeSlider.Image;
            obj.URL = postedhomeSlider.URL;
            obj.Rating = postedhomeSlider.Rating;
            obj.IsDeleted = postedhomeSlider.IsDeleted;
            obj.Show = postedhomeSlider.Show;
            obj.LastModificationTime = Parameters.CurrentDateTime;
            obj.LastModifierUserId = Parameters.UserId;
            return Save(obj);
        }
        public SystemParameters_HomeSlider_Translate Delete(SystemParameters_HomeSlider_Translate postedhomeSlider)
        {
            SystemParameters_HomeSlider_Translate obj = Get(postedhomeSlider.Id, postedhomeSlider.langId);
            //if (_db.SystemParameters_HomeSlider_Translate.Any(p => p.Id == postedhomeSlider.Id && p.IsDeleted != true))
            //{
            //      //  obj.OperationStatus = "HasRelationship";
            //    return obj;
            //}

            obj.IsDeleted = true;
            obj.CreationTime = Parameters.CurrentDateTime;
            obj.CreatorUserId = Parameters.UserId;
            return Save(obj);
        }

    }
}
