﻿using System;
using System.Collections.Generic;
using System.Linq;
using GMG_Portal.Business.Logic.General;
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
            return _db.Hotels.ToList();
            //foreach (var hotel in getHotelInfo)
            //{
            //    var haveFeature = true;

            //    //  var getHotelImages = _db.Hotels_Images.Where(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id).ToList();
            //    // var getHotelFeatures = _db.Hotels_Features.FirstOrDefault(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id);
            //    var getCurrency = _db.Currency_Translate.FirstOrDefault(p => p.RecordId == hotel.Currency);

            //    DateTime dtCheckin = DateTime.Parse(hotel.CheckIn).AddHours(2);
            //    DateTime dtCheckout = DateTime.Parse(hotel.CheckOut).AddHours(2);
            //    if (hotel.Hotels_Images.Any())
            //    {

            //        if (hotel.Hotels_Features == null)
            //            haveFeature = false;
            //        returnList.Add(new Hotel
            //        {
            //            Id = hotel.Id,
            //            Rate = hotel.Rate,
            //            PriceStart = hotel.PriceStart,
            //            Currency = hotel.Currency,
            //            CurrencyTitle = getCurrency.Title,
            //            IsDeleted = hotel.IsDeleted,
            //            HasImage = haveFeature,
            //            //Image = getHotelImages[0].Image,
            //            Late = hotel.Late,
            //            Long = hotel.Long,
            //            CheckIn = dtCheckin.ToString(),
            //            CheckOut = dtCheckout.ToString(),
            //        });
            //    }
            //    else
            //    {
            //        returnList.Add(new Hotel
            //        {
            //            Id = hotel.Id,
            //            Rate = hotel.Rate,
            //            IsDeleted = hotel.IsDeleted,
            //            PriceStart = hotel.PriceStart,
            //            Currency = hotel.Currency,
            //            CurrencyTitle = getCurrency.Title,
            //            HasImage = false,
            //            Late = hotel.Late,
            //            Long = hotel.Long,
            //            CheckIn = dtCheckin.ToString(),
            //            CheckOut = dtCheckout.ToString(),
            //        });
            //    }

            //}

            //return returnList;
        }

        public List<Hotel> GetAllWithCount()
        {
            var returnList = new List<Hotel>();
            return _db.Hotels.Where(p => p.IsDeleted == false).OrderByDescending(h => h.Id).ToList();

           
            //foreach (var hotel in getHotelInfo)
            //{
            //    // var getHotelImages = _db.Hotels_Images.Where(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id).ToList();
            //    //var getHotelFeatures = _db.Hotels_Features.FirstOrDefault(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id);
            //    // var getCurrency = _db.Currencies.FirstOrDefault(p => p.Id == hotel.Currency);

            //    //if (hotel.Hotels_Images.Any())
            //    //{
            //    //    if (hotel.Hotels_Features == null)
            //    //        continue;
            //    //    returnList.Add(new Hotel
            //    //    {
            //    //        Id = hotel.Id,
            //    //        Rate = hotel.Rate,
            //    //        PriceStart = hotel.PriceStart,
            //    //        Currency = hotel.Currency,
            //    //        // CurrencyTitle = getCurrency.DisplayValue,
            //    //        HasImage = true,
            //    //    });
            //    //}
            //    //if (returnList.Count >= 5)
            //    //    return returnList;
            //}
            //if (returnList.Count >= 5)
            //    return returnList;
            //return returnList;
        }

        public List<Hotel> GetAll()
        {
            var returnList = new List<Hotel>();
            return _db.Hotels.Where(p => p.IsDeleted == false).ToList();
            //foreach (var hotel in getHotelInfo)
            //{
            //    // var featuresList = new List<SystemParameters_Features>();
            //    var getCurrency = _db.Currency_Translate.FirstOrDefault(p => p.RecordId == hotel.Currency);
            //    //var getHotelFeatures = _db.Hotels_Features.Where(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id).ToList();

            //    if (!hotel.Hotels_Features.Any())
            //        continue;
            //    //foreach (var hotelFeature in getHotelFeatures)
            //    //{
            //    //    //var getFeatures = _db.SystemParameters_Features.Where(p => p.IsDeleted != true && p.Id == hotelFeature.Feature_Id).ToList();
            //    //    //foreach (var feature in getFeatures)
            //    //    //{
            //    //    //    featuresList.Add(new SystemParameters_Features
            //    //    //    {
            //    //    //        DisplayValue = feature.DisplayValue,
            //    //    //        Icon = feature.Icon
            //    //    //    });
            //    //    //}
            //    //}

            //    var getHotelImages = _db.Hotels_Images.Where(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id).ToList();

            //    if (getHotelImages.Any())
            //    {
            //        returnList.Add(new Hotel
            //        {
            //            Id = hotel.Id,
            //            Rate = hotel.Rate,
            //            PriceStart = hotel.PriceStart,
            //            Currency = hotel.Currency,
            //            CurrencyTitle = getCurrency.Title,
            //            //  FeaturesList = featuresList,
            //            Image = getHotelImages[0].Image,
            //            Bootstrap = 12 / getHotelInfo.Count
            //        });
            //    }
            //    else
            //    {

            //        returnList.Add(new Hotel
            //        {
            //            Id = hotel.Id,
            //            Rate = hotel.Rate,
            //            PriceStart = hotel.PriceStart,
            //            Currency = hotel.Currency,
            //            CurrencyTitle = getCurrency.Title,
            //            Bootstrap = 12 / getHotelInfo.Count
            //        });
            //    }
            //}

            //return returnList;
        }
        public Hotel Get(int id)
        {
            var returnList = new Hotel();

            var getHotelInfo = _db.Hotels.FirstOrDefault(p => p.Id == id);
            var getHotelFeatures = _db.Hotels_Features.Where(p => p.Hotel_Id == getHotelInfo.Id).ToList();
            var getCurrencyInfo = _db.Currency_Translate.FirstOrDefault(p => p.RecordId == getHotelInfo.Currency);

            //foreach (var hotelFeature in getHotelFeatures)
            //{
            //    //var getFeatures = _db.SystemParameters_Features.Where(p => p.Id == hotelFeature.Feature_Id).ToList();
            //    //foreach (var feature in getFeatures)
            //    //{
            //    //    featuresList.Add(new SystemParameters_Features
            //    //    {
            //    //        DisplayValue = feature.DisplayValue,
            //    //        Icon = feature.Icon
            //    //    });
            //    //}
            //}
            var getHotelImages = _db.Hotels_Images.Where(p => p.Hotel_Id == getHotelInfo.Id && p.IsDeleted == false).ToList();

            if (getHotelInfo != null)
            {
                if (getHotelImages.Any())
                {
                    returnList.Image = getHotelImages[0].Image;
                    returnList.ImageList = getHotelImages;
                }


                DateTime dtCheckin = DateTime.Parse(getHotelInfo.CheckIn);
                DateTime dtCheckout = DateTime.Parse(getHotelInfo.CheckOut);
                returnList.Id = getHotelInfo.Id;
                returnList.Rate = getHotelInfo.Rate;
                returnList.PriceStart = getHotelInfo.PriceStart;
                returnList.CurrencyTitle = getCurrencyInfo.Title;
                returnList.Late = getHotelInfo.Late;
                returnList.Long = getHotelInfo.Long;
                returnList.CheckIn = dtCheckin.ToString();
                returnList.CheckOut = dtCheckout.ToString();
                //  returnList.FeaturesList = featuresList;

                return returnList;
            }
            else return null;



        }

        public Hotel GetWithDeleted(int id)
        {
            var returnList = new Hotel();
            // var featuresList = new List<SystemParameters_Features>();

            return _db.Hotels.FirstOrDefault(p => p.Id == id);
           // var getCurrency = _db.Currency_Translate.FirstOrDefault(p => p.RecordId == getHotelInfo.Currency);

            //var getHotelFeatures = _db.Hotels_Features.Where(p => p.Hotel_Id == getHotelInfo.Id).ToList();
            //foreach (var hotelFeature in getHotelFeatures)
            //{
            //    //var getFeatures = _db.SystemParameters_Features.Where(p => p.Id == hotelFeature.Feature_Id).ToList();
            //    //foreach (var feature in getFeatures)
            //    //{
            //    //    featuresList.Add(new SystemParameters_Features
            //    //    {
            //    //        Id = feature.Id,
            //    //        DisplayValue = feature.DisplayValue,
            //    //        Icon = feature.Icon
            //    //    });
            //    //}
            //}
            //var getHotelImages = _db.Hotels_Images.Where(p => p.Hotel_Id == getHotelInfo.Id).ToList();

            //if (getHotelInfo != null)
            //{
            //    //DateTime dtCheckin = DateTime.Parse(getHotelInfo.CheckIn, new CultureInfo("ar-SA"));
            //    //DateTime dtCheckout = DateTime.Parse(getHotelInfo.CheckOut, new CultureInfo("ar-SA"));

            //    DateTime dtCheckin = DateTime.Parse(getHotelInfo.CheckIn).AddHours(2);
            //    DateTime dtCheckout = DateTime.Parse(getHotelInfo.CheckOut).AddHours(2);
            //    if (getHotelInfo.Hotels_Images.Any())
            //    {
            //        returnList.Image = getHotelImages[0].Image;
            //        returnList.ImageList = getHotelImages;
            //    }
            //    returnList.Id = getHotelInfo.Id;
            //    returnList.Rate = getHotelInfo.Rate;
            //    returnList.PriceStart = getHotelInfo.PriceStart;
            //    returnList.Currency = getHotelInfo.Currency;
            //    returnList.CurrencyTitle = getCurrency.Title;
            //    //  returnList.FeaturesList = featuresList;
            //    returnList.Late = getHotelInfo.Late;
            //    returnList.Long = getHotelInfo.Long;
            //    returnList.CheckIn = dtCheckin.ToString();
            //    returnList.CheckOut = dtCheckout.ToString();

            //    return returnList;
            //}
            //else return null;



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
            return _db.Hotels_Images.Where(x => x.Hotel_Id == hotelId && !x.IsDeleted);
        }
        public List<Hotels_Images> GetAllImages()
        {
            var returnList = new List<Hotels_Images>();
            var availableHotels = _db.Hotels.Where(x => !x.IsDeleted).ToList();
            foreach (var availableHotel in availableHotels)
            {
                //var imageList = _db.Hotels_Images.Where(x => x.IsDeleted == false && x.Hotel_Id == availableHotel.Id).ToList();
                var random = availableHotel.Hotels_Images.OrderBy(x => Guid.NewGuid()).Take(10);
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
                IsDeleted = postedhotel.IsDeleted,
                PriceStart = postedhotel.PriceStart,
                Rate = postedhotel.Rate,
                Currency = postedhotel.Currency,
                Late = postedhotel.Late,
                Long = postedhotel.Long,
                CheckIn = postedhotel.CheckIn,
                CheckOut = postedhotel.CheckOut,
                ImageList = postedhotel.ImageList,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.Hotels.Add(hotel);
            _db.SaveChanges();
            var objTrasnlate = new Hotels_Translate();
            {
                foreach (var title in postedhotel.TitleDictionary)
                {
                    objTrasnlate.Title = title.Value;
                    objTrasnlate.Descrtiption = postedhotel.DescDictionary[title.Key];
                    objTrasnlate.langId = title.Key;
                    objTrasnlate.RecordId = hotel.Id;
                    _db.Hotels_Translate.Add(objTrasnlate);
                    _db.SaveChanges();
                }
            }
            Hotel hotelObj = Get(hotel.Id);
            List<Hotels_Translate> hotelTranslate = GetTranslates(hotel.Id);
            return Save(hotelObj);
        }
        public List<Hotels_Translate> GetTranslates(int recordId)
        {
            return _db.Hotels_Translate.Where(x => x.RecordId == recordId).ToList();
        }
        public Hotel Edit(Hotel postedHotel)
        {

            var hotel = GetHotelInfoById(postedHotel.Id);
            List<Hotels_Translate> hotelTranslate = GetTranslates(postedHotel.Id);
            foreach (var title in postedHotel.TitleDictionary)
            {
                foreach (var objTranslate in hotelTranslate)
                {
                    if (title.Key == objTranslate.langId)
                    {
                        objTranslate.Title = title.Value;
                        objTranslate.Descrtiption = postedHotel.DescDictionary[title.Key];
                        _db.SaveChanges();
                    }
                }
            }

            hotel.IsDeleted = postedHotel.IsDeleted;
            hotel.ImageList = postedHotel.ImageList;
            hotel.Rate = postedHotel.Rate;
            hotel.Currency = postedHotel.Currency;
            hotel.PriceStart = postedHotel.PriceStart;
            hotel.Late = postedHotel.Late;
            hotel.Long = postedHotel.Long;
            hotel.CheckIn = postedHotel.CheckIn;
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
