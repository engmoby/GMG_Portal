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
        private SystemParameters_HomeSlider Save(SystemParameters_HomeSlider homeSlider)
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
        public SystemParameters_HomeSlider Insert(SystemParameters_HomeSlider postedHomeSlider)
        {

            var homeSlider = new SystemParameters_HomeSlider()
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
            _db.SystemParameters_HomeSlider.Add(homeSlider);
            return Save(homeSlider);
        }
        public SystemParameters_HomeSlider Edit(SystemParameters_HomeSlider postedhomeSlider)
        {
            SystemParameters_HomeSlider homeSlider = Get(postedhomeSlider.Id);
            homeSlider.DisplayValue = postedhomeSlider.DisplayValue;
            homeSlider.DisplayValueDesc = postedhomeSlider.DisplayValueDesc;
            homeSlider.Image = postedhomeSlider.Image;
            homeSlider.URL = postedhomeSlider.URL;
            homeSlider.Rating = postedhomeSlider.Rating;
            homeSlider.IsDeleted = postedhomeSlider.IsDeleted;
            homeSlider.Show = postedhomeSlider.Show; 
            homeSlider.LastModificationTime = Parameters.CurrentDateTime;
            homeSlider.LastModifierUserId = Parameters.UserId;
            return Save(homeSlider);
        }
        public SystemParameters_HomeSlider Delete(SystemParameters_HomeSlider postedhomeSlider)
        {
            SystemParameters_HomeSlider homeSlider = Get(postedhomeSlider.Id);
            //if (_db.SystemParameters_HomeSlider.Any(p => p.Id == postedhomeSlider.Id && p.IsDeleted != true))
            //{
            //      //  homeSlider.OperationStatus = "HasRelationship";
            //    return homeSlider;
            //}

            homeSlider.IsDeleted = true;
            homeSlider.CreationTime = Parameters.CurrentDateTime;
            homeSlider.CreatorUserId = Parameters.UserId;
            return Save(homeSlider);
        }

    }
}
