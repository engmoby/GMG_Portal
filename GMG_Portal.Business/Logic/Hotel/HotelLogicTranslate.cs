﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class HotelLogicTranslate
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public HotelLogicTranslate()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<Hotels_Translate> GetAllWithDeleted(string langId)
        {
            var returnList = new List<Hotels_Translate>();
            var getHotelInfo = _db.Hotels_Translate.Where(x => x.langId == langId).ToList();
            foreach (var hotel in getHotelInfo)
            {
                var getHotelImages = _db.Hotels_Images.Where(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id).ToList();

                if (getHotelImages.Any())
                {
                    returnList.Add(new Hotels_Translate
                    {
                        Id = hotel.Id,
                        DisplayValue = hotel.DisplayValue,
                        DisplayValueDesc = hotel.DisplayValueDesc,
                        Rate = hotel.Rate,
                        PriceStart = hotel.PriceStart,
                        IsDeleted = hotel.IsDeleted,
                        HasImage = true,
                        Image = getHotelImages[0].Image,
                        Bootstrap = 12 / getHotelInfo.Count
                    });
                }
                else
                {
                    returnList.Add(new Hotels_Translate
                    {
                        Id = hotel.Id,
                        DisplayValue = hotel.DisplayValue,
                        DisplayValueDesc = hotel.DisplayValueDesc,
                        Rate = hotel.Rate,
                        IsDeleted = hotel.IsDeleted,
                        PriceStart = hotel.PriceStart,
                        HasImage = false,
                        Bootstrap = 12 / getHotelInfo.Count
                    });
                }
            }

            return returnList;
        }

        public List<Hotels_Translate> GetAllWithCount(string langId)
        {
            var returnList = new List<Hotels_Translate>();
            var getHotelInfo = _db.Hotels_Translate.Where(p => p.IsDeleted == false && p.langId == langId).OrderByDescending(h => h.Id).ToList();
            foreach (var hotel in getHotelInfo)
            {
                var getHotelImages = _db.Hotels_Images.Where(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id).ToList();

                if (getHotelImages.Any())
                {
                    returnList.Add(new Hotels_Translate
                    {
                        Id = hotel.Id,
                        DisplayValue = hotel.DisplayValue,
                        DisplayValueDesc = hotel.DisplayValueDesc,
                        Rate = hotel.Rate,
                        PriceStart = hotel.PriceStart,
                        HasImage = true,
                    });
                }
                if (returnList.Count >= 10)
                    return returnList;
            }

            return returnList;
        }

        public List<Hotels_Translate> GetAll(string langId)
        {
            var returnList = new List<Hotels_Translate>();
            var featuresList = new List<SystemParameters_Features>();
            var getHotelInfo = _db.Hotels_Translate.Where(p => p.IsDeleted == false && p.Show && p.langId == langId).ToList();
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
                    returnList.Add(new Hotels_Translate
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

                    returnList.Add(new Hotels_Translate
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
        public Hotels_Translate Get(int id, string langId)
        {
            var returnList = new Hotels_Translate();
            var featuresList = new List<SystemParameters_Features>();

            var getHotelInfo = _db.Hotels_Translate.FirstOrDefault(p => p.Id == id && p.langId == langId);
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
                returnList.FeaturesList = featuresList;

                return returnList;
            }
            else return null;



        }

        public Hotels_Translate GetWithDeleted(int id, string langId)
        {
            var returnList = new Hotels_Translate();
            var featuresList = new List<SystemParameters_Features>();

            var getHotelInfo = _db.Hotels_Translate.FirstOrDefault(p => p.Id == id && p.langId == langId);
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

                return returnList;
            }
            else return null;



        }

        public Hotels_Translate GetHotelInfoById(int id, string langId)
        {
            return _db.Hotels_Translate.FirstOrDefault(p => p.Id == id && p.langId == langId);
        }
        public Hotels_Images GetImageInfoById(int id, string langId)
        {
            return _db.Hotels_Images.Find(id);
        }
        public IQueryable<Hotels_Images> HotelImages(int hotelId)
        {
            return _db.Hotels_Images.Where(x => x.Hotel_Id == hotelId && x.Show);
        }
        private Hotels_Translate Save(Hotels_Translate hotel)
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
        public Hotels_Translate Insert(Hotels_Translate postedhotel)
        {
            var hotel = new Hotels_Translate()
            {
                DisplayValue = postedhotel.DisplayValue,
                DisplayValueDesc = postedhotel.DisplayValueDesc,
                IsDeleted = postedhotel.IsDeleted,
                Show = Parameters.Show,
                PriceStart = postedhotel.PriceStart,
                ImageList = postedhotel.ImageList,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
                langId = postedhotel.langId
            };
            _db.Hotels_Translate.Add(hotel);
            return Save(hotel);
        }
        public Hotels_Translate Edit(Hotels_Translate postedHotel )
        {
            var hotel = GetHotelInfoById(postedHotel.Id, postedHotel.langId);
            hotel.DisplayValue = postedHotel.DisplayValue;
            hotel.DisplayValueDesc = postedHotel.DisplayValueDesc;
            hotel.IsDeleted = postedHotel.IsDeleted;
            hotel.ImageList = postedHotel.ImageList;
            hotel.PriceStart = postedHotel.PriceStart;
            hotel.LastModificationTime = Parameters.CurrentDateTime;
            hotel.LastModifierUserId = Parameters.UserId;
            hotel.langId = postedHotel.langId;
            return Save(hotel);
        }
        public Hotels_Translate Delete(Hotels_Translate postedHotel)
        {
            var hotel = GetHotelInfoById(postedHotel.Id, postedHotel.langId);


            hotel.IsDeleted = true;
            hotel.CreationTime = Parameters.CurrentDateTime;
            hotel.CreatorUserId = Parameters.UserId;
            return Save(hotel);
        }

        public List<Hotels_Images> InsertHotelImages(List<Hotels_Images> postedhotel)
        {
            var hotelLogic = new HotelLogic();
            foreach (var imageObj in postedhotel)
            {
                if (hotelLogic.GetImageInfoById(imageObj.Id) != null)
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
            var hotelLogic = new HotelLogic();
            var imageObj = hotelLogic.GetImageInfoById(postedImage.Id);

            imageObj.IsDeleted = isDeleted;
            imageObj.DeletionTime = Parameters.CurrentDateTime;
            imageObj.DeleterUserId = Parameters.UserId;
            _db.SaveChanges();
            imageObj.OperationStatus = "Succeded";
            return imageObj;
        }

        public List<Hotels_Features> InsertHotelFeatures(List<Hotels_Features> postedfeature)    
        {
            var hotelLogic = new HotelLogic();
            var deleteList = hotelLogic.DeleteHotelFeatures(postedfeature[0].Hotel_Id); 
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
            postedfeature[0].OperationStatus = "Succeded";
            return postedfeature;
        }
        public Hotels_Features DeleteHotelFeatures(int? id)
        {
            var featureList = _db.Hotels_Features.Where(item => item.Id == id).ToList();
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