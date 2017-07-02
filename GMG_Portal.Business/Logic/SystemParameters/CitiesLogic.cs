using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class CitiesLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public CitiesLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<SystemParameter_Cities> GetAllWithDeleted()
        {
            return _db.SystemParameter_Cities.OrderBy(p => p.IsDeleted).ToList();
        }
        public List<SystemParameter_Cities> GetAll()
        {
            return _db.SystemParameter_Cities.Where(p => p.IsDeleted != true).ToList();
        }
        public List<SystemParameter_Cities> GetAll(int countryId)
        {
            return _db.SystemParameter_Cities.Where(ps =>  ps.CountryID == countryId && ps.IsDeleted!=true).ToList();
        }
        public List<SystemParameter_Cities> GetAllWithDeleted(int countryId)
        {
            return _db.SystemParameter_Cities.Where(ps =>  ps.CountryID== countryId).OrderBy(p => p.IsDeleted).ToList();
        }
        public SystemParameter_Cities Get(int id)
        {
            return _db.SystemParameter_Cities.Find(id);
        }
        private SystemParameter_Cities Save(SystemParameter_Cities city)
        {
            try
            {
                _db.SaveChanges();
                city.OperationStatus = "Succeded";
                return city;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_SystemParameter_Cities_Ar"))
                    {
                        city.OperationStatus = "NameArMustBeUnique";
                        return city;
                    }
                    else if (e.InnerException.ToString().Contains("IX_SystemParameter_Cities_En"))
                    {
                        city.OperationStatus = "NameEnMustBeUnique";
                        return city;
                    }
                }
                throw;
            }
        }

        public SystemParameter_Cities Insert(SystemParameter_Cities postedSystemParameterCities)
        {
            var city = new SystemParameter_Cities()
            {
                CountryID=postedSystemParameterCities.CountryID,
                //NameAr = postedSystemParameterCities.NameAr,
                //NameEn = postedSystemParameterCities.NameEn,
                IsDeleted = false

            };
        
            _db.SystemParameter_Cities.Add(city);
            return Save(city);
        }
        public SystemParameter_Cities Edit(SystemParameter_Cities postedCity)
        {
            return null;
            //SystemParameter_Cities City = Get(postedCity.ID);
            //City.CountryID = postedCity.CountryID;
            //City. = postedCity.NameEn;
            //City.NameAr = postedCity.NameAr;
            //City.IsDeleted = postedCity.IsDeleted;
            //return Save(City);
        }
        public SystemParameter_Cities Delete(SystemParameter_Cities PostedCity)
        {
            return null;
            //SystemParameter_Cities City = Get(PostedCity.ID);
            //if (_db.CourierDetails.Where(p => p.CityID == PostedCity.ID && p.IsDeleted != true).Count() > 0
            //        || _db.Regions.Where(c => c.CityID == PostedCity.ID && c.IsDeleted != true).Count() > 0)
            //{
            //    City.OperationStatus = "HasRelationship";
            //    return City;
            //}

            //City.IsDeleted = true;
            //return Save(City);
        }

    }

    //public class SystemParameter_CitiesLogic
    //{
    //    GMG_Portal_DBEntities DB = new GMG_Portal_DBEntities();
    //    public List<SystemParameter_Cities> GetAll()
    //    {
    //        return DB.SystemParameter_Cities.Where(C => C.IsDeleted != true).ToList();
    //    }

    //    public List<SystemParameter_Cities> GetAll(int countryId)
    //    {
    //        return DB.SystemParameter_Cities.Where(cty => cty.CountryID == countryId).ToList();
    //    }

    //    public SystemParameter_Cities Get(int ID)
    //    {
    //        return DB.SystemParameter_Cities.Find(ID);
    //    }

    //    public SystemParameter_Cities Save(SystemParameter_Cities PostedCity)
    //    {
    //        var city = PostedCity.ID == 0 ? null : Get(PostedCity.ID);

    //        if (city == null)
    //        {
    //            city = new SystemParameter_Cities();
    //            DB.SystemParameter_Cities.Add(city);
    //        }

    //        city.NameAr = PostedCity.NameAr;
    //        city.NameEn = PostedCity.NameEn;
    //        city.CountryID = PostedCity.CountryID;

    //        DB.SaveChanges();
    //        return city;
    //    }

    //}
}
