using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class HotelLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public HotelLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<Hotel> GetAllWithDeleted()
        {
            return _db.Hotels.OrderBy(p => p.IsDeleted).ToList();
        }
        public List<Hotel> GetAll()
        {
            var returnList = new List<Hotel>();
            var featuresList = new List<SystemParameters_Features>();
            var getHotelInfo = _db.Hotels.Where(p => p.IsDeleted == false && p.Show).ToList();
            foreach (var hotel in getHotelInfo)
            { 

                var getHotelFeatures = _db.Hotels_Features.Where(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id).ToList();
                foreach (var hotelFeature in getHotelFeatures)
                {
                    var getFeatures = _db.SystemParameters_Features.Where(p => p.IsDeleted != true && p.Id == hotelFeature.Feature_Id).ToList();
                    foreach (var feature in getFeatures)
                    {
                        featuresList.Add(new SystemParameters_Features
                        {
                            DisplayValue = feature.DisplayValue,
                            Icon = feature.Icon
                        });
                    }
                }

                var getHotelImages = _db.Hotels_Images.Where(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id).ToList();
                returnList.Add(new Hotel
                {
                    Id = hotel.Id,
                    DisplayValue = hotel.DisplayValue,
                    DisplayValueDesc = hotel.DisplayValueDesc,
                    Rate = hotel.Rate,
                    PriceStart = hotel.PriceStart,
                    FeaturesList = featuresList,
                    Image = getHotelImages[0].Image,
                    Bootstrap = 12 / getHotelInfo.Count
                });
            }

            return returnList;
        }
        public Hotel Get(int id)
        {
            var returnList = new Hotel();
            var featuresList = new List<SystemParameters_Features>();

            var getHotelInfo = _db.Hotels.FirstOrDefault(p => p.Id == id && p.IsDeleted == false && p.Show);
            var getHotelFeatures = _db.Hotels_Features.Where(p => p.IsDeleted != true && p.Hotel_Id == getHotelInfo.Id).ToList();
            foreach (var hotelFeature in getHotelFeatures)
            {
                var getFeatures = _db.SystemParameters_Features.Where(p => p.IsDeleted != true && p.Id == hotelFeature.Feature_Id).ToList();
                foreach (var feature in getFeatures)
                {
                    featuresList.Add(new SystemParameters_Features
                    {
                        DisplayValue = feature.DisplayValue,
                        Icon = feature.Icon
                    });
                }
            }
            var getHotelImages = _db.Hotels_Images.Where(p => p.IsDeleted != true && p.Hotel_Id == getHotelInfo.Id).ToList();

            if (getHotelInfo != null)
            {
                returnList.Id = getHotelInfo.Id;
                returnList.DisplayValue = getHotelInfo.DisplayValue;
                returnList.DisplayValueDesc = getHotelInfo.DisplayValueDesc;
                returnList.Rate = getHotelInfo.Rate;
                returnList.PriceStart = getHotelInfo.PriceStart;
                returnList.Image = getHotelImages[0].Image;
                returnList.ImageList = getHotelImages;
                returnList.FeaturesList = featuresList;
                return returnList;
            }
            else return null;



        }
        public IQueryable<Hotels_Images> HotelImages(int hotelId)
        {
            return _db.Hotels_Images.Where(x => x.Hotel_Id == hotelId && x.Show);
        }
        public List<Hotels_Images> GetAllImages()
        {
            return _db.Hotels_Images.Where(x => x.Show).ToList();
        }
        private Hotel Save(Hotel hotel)
        {
            try
            {
                _db.SaveChanges();
                hotel.OperationStatus = "Succeded";
                return hotel;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        hotel.OperationStatus = "NameArMustBeUnique";
                        return hotel;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        hotel.OperationStatus = "NameEnMustBeUnique";
                        return hotel;
                    }
                }
                throw;
            }
        }
        public Hotel Insert(Hotel postedhotel)
        {

            var Hotel = new Hotel()
            {
                DisplayValue = postedhotel.DisplayValue,
                DisplayValueDesc = postedhotel.DisplayValueDesc,

                IsDeleted = postedhotel.IsDeleted,
                Show = Parameters.Show,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.Hotels.Add(Hotel);
            return Save(Hotel);
        }
        public Hotel Edit(Hotel postedHotel)
        {
            Hotel Hotel = Get(postedHotel.Id);
            Hotel.DisplayValue = postedHotel.DisplayValue;
            Hotel.DisplayValueDesc = postedHotel.DisplayValueDesc;
            Hotel.IsDeleted = postedHotel.IsDeleted;
            Hotel.Show = postedHotel.Show;
            Hotel.LastModificationTime = Parameters.CurrentDateTime;
            Hotel.LastModifierUserId = Parameters.UserId;
            return Save(Hotel);
        }
        public Hotel Delete(Hotel postedHotel)
        {
            Hotel hotel = Get(postedHotel.Id);
            if (_db.Hotels.Any(p => p.Id == postedHotel.Id && p.IsDeleted != true))
            {
                //  Hotel.OperationStatus = "HasRelationship";
                return hotel;
            }

            hotel.IsDeleted = true;
            hotel.CreationTime = Parameters.CurrentDateTime;
            hotel.CreatorUserId = Parameters.UserId;
            return Save(hotel);
        }

    }
}
