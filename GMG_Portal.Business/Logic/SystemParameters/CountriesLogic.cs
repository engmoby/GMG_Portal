using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class CountriesLogic
    {
        private readonly GMG_Portal_DBEntities _db;

        public CountriesLogic()
        {
            _db = new GMG_Portal_DBEntities();
        }

        public List<Countries> GetAllWithDeleted()
        {
            return null;
            //return DB.SystemParameter_Countries.OrderBy(p => p.IsDeleted).ToList();
        }

        public List<Countries> GetAll()
        {
            return null;
            //  return DB.Countries.Where(p => p.IsDeleted != true).ToList();
        }
        public Countries Get(int ID)
        {
            return null;
            // return DB.Countries.Find(ID);
        }
        private Countries Save(Countries Country)
        {
            try
            {
                _db.SaveChanges();
                Country.OperationStatus = "Succeded";
                return Country;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        Country.OperationStatus = "NameArMustBeUnique";
                        return Country;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        Country.OperationStatus = "NameEnMustBeUnique";
                        return Country;
                    }
                }
                throw;
            }
        }

        public Countries Insert(Countries PostedCountry)
        {
            return null;
            //var Country = new Countries()
            //{
            //    IsCurrent = false,
            //    NameAr = PostedCountry.NameAr,
            //    NameEn = PostedCountry.NameEn,
            //    IsDeleted = false

            //};
            //DB.Countries.Add(Country);
            //return Save(Country);
        }
        public Countries Edit(Countries PostedCountry)
        {
            return null;
            //Countries Country = Get(PostedCountry.ID);
            //Country.NameEn = PostedCountry.NameEn;
            //Country.NameAr = PostedCountry.NameAr;
            //Country.IsDeleted = PostedCountry.IsDeleted;
            //return Save(Country);
        }
        public Countries Delete(Countries PostedCountry)
        {
            return null;
            //Countries Country = Get(PostedCountry.ID);
            //if (DB.Cities.Where(p => p.CountryID == PostedCountry.ID && p.IsDeleted != true).Count() > 0)
            //{
            //    Country.OperationStatus = "HasRelationship";
            //    return Country;
            //}

            //Country.IsDeleted = true;
            //return Save(Country);
        }

    }
}
