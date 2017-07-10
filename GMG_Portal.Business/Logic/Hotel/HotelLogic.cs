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
            var getHotelInfo = _db.Hotels.Where(p => p.IsDeleted == false && p.Show).ToList();
            foreach (var hotel in getHotelInfo)
            {
                var getHotelImages = _db.Hotels_Hotel_Images.Where(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id).ToList();
                returnList.Add(new Hotel
                {
                    Id = hotel.Id,
                    DisplayValue = hotel.DisplayValue,
                    DisplayValueDesc = hotel.DisplayValueDesc,
                    Rate = hotel.Rate,
                    PriceStart = hotel.PriceStart,
                    Image = getHotelImages[0].Image
                });
            }
            return returnList;
           // return _db.Hotels.Where(p => p.IsDeleted != true).ToList();
        }
        public Hotel Get(int id)
        {
            return _db.Hotels.Find(id);
        }
        public IQueryable<Hotels_Hotel_Images> HotelImages(int hotelId)
        {
            return _db.Hotels_Hotel_Images.Where(x => x.Hotel_Id == hotelId);
        }
        private Hotel Save(Hotel homeSlider)
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
        public Hotel Insert(Hotel postedHomeSlider)
        {

            var Hotel = new Hotel()
            {
                DisplayValue = postedHomeSlider.DisplayValue,
                DisplayValueDesc = postedHomeSlider.DisplayValueDesc,

                IsDeleted = postedHomeSlider.IsDeleted,
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
