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
        public List<SystemParameters_HomeSlider> GetAllWithDeleted()
        {
            return _db.SystemParameters_HomeSlider.OrderBy(p => p.IsDeleted).ToList();
        }
        public List<SystemParameters_HomeSlider> GetAll()
        {
            return _db.SystemParameters_HomeSlider.Where(p => p.IsDeleted != true).ToList();
        }
        public SystemParameters_HomeSlider Get(int id)
        {
            return _db.SystemParameters_HomeSlider.Find(id);
        }
        private SystemParameters_HomeSlider Save(SystemParameters_HomeSlider obj)
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
        public SystemParameters_HomeSlider Insert(SystemParameters_HomeSlider postedHomeSlider)
        {

            var obj = new SystemParameters_HomeSlider()
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
            _db.SystemParameters_HomeSlider.Add(obj);
            return Save(obj);
        }
        public SystemParameters_HomeSlider Edit(SystemParameters_HomeSlider postedhomeSlider)
        {
            SystemParameters_HomeSlider obj = Get(postedhomeSlider.Id);
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
        public SystemParameters_HomeSlider Delete(SystemParameters_HomeSlider postedhomeSlider)
        {
            SystemParameters_HomeSlider obj = Get(postedhomeSlider.Id);
            //if (_db.SystemParameters_HomeSlider.Any(p => p.Id == postedhomeSlider.Id && p.IsDeleted != true))
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
