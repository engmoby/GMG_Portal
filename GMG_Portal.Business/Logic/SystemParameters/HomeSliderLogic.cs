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
        public List<HomeSlider> GetAllWithDeleted()
        {
            return _db.HomeSliders.OrderBy(p => p.IsDeleted).ToList();
        }
        public List<HomeSlider> GetAll()
        {
            return _db.HomeSliders.Where(p => p.IsDeleted != true).ToList();
        }
        public HomeSlider Get(int id)
        {
            return _db.HomeSliders.Find(id);
        }
        public List<HomeSlider_Translate> GetTranslates(int recordId)
        {
            return _db.HomeSlider_Translate.Where(x => x.RecordId == recordId).ToList();
        }
        private HomeSlider Save(HomeSlider obj)
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
        public HomeSlider Insert(HomeSlider postedHomeSlider)
        {

            var obj = new HomeSlider()
            {
                Image = postedHomeSlider.Image,
                IsDeleted = postedHomeSlider.IsDeleted,
                Rating = postedHomeSlider.Rating,
                URL = postedHomeSlider.URL,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.HomeSliders.Add(obj);
            _db.SaveChanges();
            var objTrasnlate = new HomeSlider_Translate();
            {
                foreach (var title in postedHomeSlider.TitleDictionary)
                {
                    objTrasnlate.Title = title.Value;
                    objTrasnlate.Description = postedHomeSlider.DescDictionary[title.Key];
                    objTrasnlate.langId = title.Key;
                    objTrasnlate.RecordId = obj.Id;
                    _db.HomeSlider_Translate.Add(objTrasnlate);
                    _db.SaveChanges();
                }
            }
            HomeSlider objReturn = Get(obj.Id);
            List<HomeSlider_Translate> currencyTranslate = GetTranslates(obj.Id);
            return Save(objReturn);
        }
        public HomeSlider Edit(HomeSlider postedhomeSlider)
        {
            HomeSlider obj = Get(postedhomeSlider.Id);
            List<HomeSlider_Translate> currencyTranslate = GetTranslates(postedhomeSlider.Id);
            foreach (var title in postedhomeSlider.TitleDictionary)
            {
                foreach (var objTranslate in currencyTranslate)
                {
                    if (title.Key == objTranslate.langId)
                    {
                        objTranslate.Title = title.Value;
                        objTranslate.Description = postedhomeSlider.DescDictionary[title.Key];
                        _db.SaveChanges();
                    }
                }
            }

            obj.Image = postedhomeSlider.Image;
            obj.URL = postedhomeSlider.URL;
            obj.Rating = postedhomeSlider.Rating;
            obj.IsDeleted = postedhomeSlider.IsDeleted;
            obj.LastModificationTime = Parameters.CurrentDateTime;
            obj.LastModifierUserId = Parameters.UserId;
            return Save(obj);
        }
        public HomeSlider Delete(HomeSlider postedhomeSlider)
        {
            HomeSlider obj = Get(postedhomeSlider.Id);
            //if (_db.SystemParameters_HomeSlider.Any(p => p.Id == postedhomeSlider.Id && p.IsDeleted != true))
            //{
            //      //  obj.OperationStatus = "HasRelationship";
            //    return obj;
            //}

            obj.IsDeleted = true;
            obj.CreationTime = Parameters.CurrentDateTime;
            obj.CreatorUserId = Parameters.UserId;
            _db.SaveChanges();
            return Save(obj);
        }

    }
}
