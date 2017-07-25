using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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

                if (getHotelImages.Any())
                {
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
                else
                {
                    returnList.Add(new Hotel
                    {
                        Id = hotel.Id,
                        DisplayValue = hotel.DisplayValue,
                        DisplayValueDesc = hotel.DisplayValueDesc,
                        Rate = hotel.Rate,
                        PriceStart = hotel.PriceStart,
                        FeaturesList = featuresList,
                        Bootstrap = 12 / getHotelInfo.Count
                    });
                }
            }

            return returnList;
        }
        public Hotel Get(int id)
        {
            var returnList = new Hotel();
            var featuresList = new List<SystemParameters_Features>();

            var getHotelInfo = _db.Hotels.FirstOrDefault(p => p.Id == id);
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
                if (getHotelImages.Any())
                {
                    returnList.Image = getHotelImages[0].Image;
                    returnList.ImageList = getHotelImages;
                }
                returnList.Id = getHotelInfo.Id;
                returnList.DisplayValue = getHotelInfo.DisplayValue;
                returnList.DisplayValueDesc = getHotelInfo.DisplayValueDesc;
                returnList.Rate = getHotelInfo.Rate;
                returnList.PriceStart = getHotelInfo.PriceStart;
                returnList.FeaturesList = featuresList;

                return returnList;
            }
            else return null;



        }
        public Hotel GetHotelInfoById(int id)
        {

            return _db.Hotels.Find(id);

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
            var hotel = new Hotel()
            {
                DisplayValue = postedhotel.DisplayValue,
                DisplayValueDesc = postedhotel.DisplayValueDesc,
                IsDeleted = postedhotel.IsDeleted,
                Show = Parameters.Show,
                PriceStart = postedhotel.PriceStart,
                ImageList = postedhotel.ImageList,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.Hotels.Add(hotel);
            return Save(hotel);
        }
        public Hotel Edit(Hotel postedHotel)
        {
            var hotel = GetHotelInfoById(postedHotel.Id);
            hotel.DisplayValue = postedHotel.DisplayValue;
            hotel.DisplayValueDesc = postedHotel.DisplayValueDesc;
            hotel.IsDeleted = postedHotel.IsDeleted;
            hotel.ImageList = postedHotel.ImageList;
            hotel.PriceStart = postedHotel.PriceStart;
            hotel.LastModificationTime = Parameters.CurrentDateTime;
            hotel.LastModifierUserId = Parameters.UserId;

            // _db.Hotels.Attach(hotel);
            //var entry = _db.Entry(hotel);
            //entry.Property(e => e.IsDeleted).IsModified = true;
            // other changed properties
            //_db.SaveChanges();

            return Save(hotel);
        }

        public Hotel Delete(Hotel postedHotel)
        {
            Hotel hotel = Get(postedHotel.Id);
            //if (_db.Hotels.Any(p => p.Id == postedHotel.Id))
            //{
            //    //  Hotel.OperationStatus = "HasRelationship";
            //    return hotel;
            //}

            hotel.IsDeleted = true;
            hotel.CreationTime = Parameters.CurrentDateTime;
            hotel.CreatorUserId = Parameters.UserId;
            _db.SaveChanges();
            return Save(hotel);
        }

    }
}
