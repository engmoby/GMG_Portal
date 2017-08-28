using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Globalization;
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
            var returnList = new List<Hotel>();
            var getHotelInfo = _db.Hotels.ToList();
            foreach (var hotel in getHotelInfo)
            {
                var haveFeature = true;

                var getHotelImages = _db.Hotels_Images.Where(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id).ToList();
                var getHotelFeatures = _db.Hotels_Features.FirstOrDefault(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id);

                if (getHotelImages.Any())
                {
                    if (getHotelFeatures == null)
                        haveFeature = false;
                    returnList.Add(new Hotel
                    {
                        Id = hotel.Id,
                        DisplayValue = hotel.DisplayValue,
                        DisplayValueDesc = hotel.DisplayValueDesc,
                        Rate = hotel.Rate,
                        PriceStart = hotel.PriceStart,
                        IsDeleted = hotel.IsDeleted,
                        HasImage = haveFeature,
                        Image = getHotelImages[0].Image,
                        Late = hotel.Late,
                        Long = hotel.Long,
                        CheckIn = hotel.CheckIn,
                        CheckOut = hotel.CheckOut,
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
                        IsDeleted = hotel.IsDeleted,
                        PriceStart = hotel.PriceStart,
                        HasImage = false,
                        Late = hotel.Late,
                        Long = hotel.Long,
                        CheckIn = hotel.CheckIn,
                        CheckOut = hotel.CheckOut
                    });
                }

            }

            return returnList;
        }

        public List<Hotel> GetAllWithCount()
        {
            var returnList = new List<Hotel>();
            var getHotelInfo = _db.Hotels.Where(p => p.IsDeleted == false).OrderByDescending(h => h.Id).ToList();
            foreach (var hotel in getHotelInfo)
            {
                var getHotelImages = _db.Hotels_Images.Where(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id).ToList();
                var getHotelFeatures = _db.Hotels_Features.FirstOrDefault(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id);

                if (getHotelImages.Any())
                {
                    if (getHotelFeatures == null)
                        continue;
                    returnList.Add(new Hotel
                    {
                        Id = hotel.Id,
                        DisplayValue = hotel.DisplayValue,
                        DisplayValueDesc = hotel.DisplayValueDesc,
                        Rate = hotel.Rate,
                        PriceStart = hotel.PriceStart,
                        HasImage = true,
                    });
                }
                if (returnList.Count >= 5)
                    return returnList;
            }

            return returnList;
        }

        public List<Hotel> GetAll()
        {
            var returnList = new List<Hotel>();
            var getHotelInfo = _db.Hotels.Where(p => p.IsDeleted == false && p.Show).ToList();
            foreach (var hotel in getHotelInfo)
            {
                var featuresList = new List<SystemParameters_Features>();

                var getHotelFeatures = _db.Hotels_Features.Where(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id).ToList();

                if (!getHotelFeatures.Any())
                    continue;
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
            var getHotelFeatures = _db.Hotels_Features.Where(p => p.Hotel_Id == getHotelInfo.Id).ToList();
            foreach (var hotelFeature in getHotelFeatures)
            {
                var getFeatures = _db.SystemParameters_Features.Where(p => p.Id == hotelFeature.Feature_Id).ToList();
                foreach (var feature in getFeatures)
                {
                    featuresList.Add(new SystemParameters_Features
                    {
                        DisplayValue = feature.DisplayValue,
                        Icon = feature.Icon
                    });
                }
            }
            var getHotelImages = _db.Hotels_Images.Where(p => p.Hotel_Id == getHotelInfo.Id && p.IsDeleted == false).ToList();

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
                returnList.Late = getHotelInfo.Late;
                returnList.Long = getHotelInfo.Long;
                returnList.CheckIn = getHotelInfo.CheckIn;
                returnList.CheckOut = getHotelInfo.CheckOut;
                returnList.FeaturesList = featuresList;

                return returnList;
            }
            else return null;



        }

        public Hotel GetWithDeleted(int id)
        {
            var returnList = new Hotel();
            var featuresList = new List<SystemParameters_Features>();

            var getHotelInfo = _db.Hotels.FirstOrDefault(p => p.Id == id);
            var getHotelFeatures = _db.Hotels_Features.Where(p => p.Hotel_Id == getHotelInfo.Id).ToList();
            foreach (var hotelFeature in getHotelFeatures)
            {
                var getFeatures = _db.SystemParameters_Features.Where(p => p.Id == hotelFeature.Feature_Id).ToList();
                foreach (var feature in getFeatures)
                {
                    featuresList.Add(new SystemParameters_Features
                    {
                        Id = feature.Id,
                        DisplayValue = feature.DisplayValue,
                        Icon = feature.Icon
                    });
                }
            }
            var getHotelImages = _db.Hotels_Images.Where(p => p.Hotel_Id == getHotelInfo.Id).ToList();

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
                returnList.Late = getHotelInfo.Late;
                returnList.Long = getHotelInfo.Long;
                returnList.CheckIn = getHotelInfo.CheckIn;
                returnList.CheckOut = getHotelInfo.CheckOut;

                return returnList;
            }
            else return null;



        }

        public Hotel GetHotelInfoById(int id)
        {
            return _db.Hotels.Find(id);
        }
        public Hotels_Images GetImageInfoById(int id)
        {
            return _db.Hotels_Images.Find(id);
        }
        public IQueryable<Hotels_Images> HotelImages(int hotelId)
        {
            return _db.Hotels_Images.Where(x => x.Hotel_Id == hotelId && x.Show);
        }
        public List<Hotels_Images> GetAllImages()
        {
            var returnList = new List<Hotels_Images>();
            var availableHotels = _db.Hotels.Where(x => x.IsDeleted == false).ToList();
            foreach (var availableHotel in availableHotels)
            {
                var imageList = _db.Hotels_Images.Where(x => x.IsDeleted == false && x.Hotel_Id == availableHotel.Id).ToList();
                var random = imageList.OrderBy(x => Guid.NewGuid()).Take(10);
                foreach (var hotelsImagese in random)
                {
                    returnList.Add(new Hotels_Images
                    {
                        Image = hotelsImagese.Image
                    });

                }
            }
            return returnList;
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
                Late = postedhotel.Late,
                Long = postedhotel.Long,
                CheckIn = postedhotel.CheckIn,
                CheckOut = postedhotel.CheckOut,
                ImageList = postedhotel.ImageList,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.Hotels.Add(hotel);
            return Save(hotel);
        }
        public Hotel Edit(Hotel postedHotel)
        {

            DateTime dateTime = DateTime.ParseExact(postedHotel.CheckIn, "HH:mm:ss",CultureInfo.InvariantCulture);
            var hotel = GetHotelInfoById(postedHotel.Id);
            hotel.DisplayValue = postedHotel.DisplayValue;
            hotel.DisplayValueDesc = postedHotel.DisplayValueDesc;
            hotel.IsDeleted = postedHotel.IsDeleted;
            hotel.ImageList = postedHotel.ImageList;
            hotel.PriceStart = postedHotel.PriceStart;
            hotel.Late = postedHotel.Late;
            hotel.Long = postedHotel.Long;
            hotel.CheckIn = dateTime.ToString();
            hotel.CheckOut = postedHotel.CheckOut;
            hotel.LastModificationTime = Parameters.CurrentDateTime;
            hotel.LastModifierUserId = Parameters.UserId;
            hotel.HasImage = postedHotel.HasImage;
            return Save(hotel);
        }
        public Hotel Delete(Hotel postedHotel)
        {
            var hotel = GetHotelInfoById(postedHotel.Id);
            //if (_db.Hotels_Images.FirstOrDefault(p => p.Hotel_Id == postedHotel.Id) != null)
            //{
            //    hotel.OperationStatus = "HasRelationship";
            //    return hotel;
            //}

            hotel.HasImage = postedHotel.HasImage;
            hotel.IsDeleted = true;
            hotel.CreationTime = Parameters.CurrentDateTime;
            hotel.CreatorUserId = Parameters.UserId;
            return Save(hotel);
        }

        public List<Hotels_Images> InsertHotelImages(List<Hotels_Images> postedhotel)
        {
            foreach (var imageObj in postedhotel)
            {
                if (GetImageInfoById(imageObj.Id) != null)
                    continue;
                var image = new Hotels_Images()
                {
                    Image = imageObj.Image,
                    IsDeleted = false,
                    Show = Parameters.Show,
                    Hotel_Id = postedhotel[0].Hotel_Id,
                    LastModificationTime = Parameters.CurrentDateTime,
                    CreationTime = Parameters.CurrentDateTime,
                    CreatorUserId = Parameters.UserId,
                };
                _db.Hotels_Images.Add(image);
                _db.SaveChanges();
            }

            postedhotel[0].OperationStatus = "Succeded";
            return postedhotel;
        }

        public Hotels_Images DeleteImage(Hotels_Images postedImage, bool isDeleted)
        {
            var imageObj = GetImageInfoById(postedImage.Id);

            imageObj.IsDeleted = isDeleted;
            imageObj.DeletionTime = Parameters.CurrentDateTime;
            imageObj.DeleterUserId = Parameters.UserId;
            _db.SaveChanges();
            imageObj.OperationStatus = "Succeded";
            return imageObj;
        }

        public List<Hotels_Features> InsertHotelFeatures(List<Hotels_Features> postedfeature)
        {
            var deleteList = DeleteHotelFeatures(postedfeature[0].Hotel_Id);
            if (postedfeature.Any())
            {
                foreach (var featureObj in postedfeature)
                {
                    var feature = new Hotels_Features()
                    {
                        IsDeleted = false,
                        Hotel_Id = postedfeature[0].Hotel_Id,
                        Feature_Id = featureObj.Feature_Id
                    };
                    _db.Hotels_Features.Add(feature);
                    _db.SaveChanges();
                }
            }
            postedfeature[0].OperationStatus = "Succeded";
            return postedfeature;
        }
        public Hotels_Features DeleteHotelFeatures(int? id)
        {
            var featureList = _db.Hotels_Features.Where(item => item.Hotel_Id == id).ToList();
            if (!featureList.Any())
                return null;

            try
            {
                foreach (var hotelsFeaturese in featureList)
                {
                    _db.Hotels_Features.Remove(hotelsFeaturese);

                }
                _db.SaveChanges();
                featureList[0].OperationStatus = "Succeded";
                return featureList[0];

            }
            catch (Exception)
            {
                featureList[0].OperationStatus = "Error";
                throw;
            }


        }
    }
}
