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
                var haveFeature = true;
                var getHotelImages = _db.Hotels_Images_Translate.Where(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id && p.langId == langId).ToList();
                var getHotelFeatures = _db.Hotels_Features_Translate.FirstOrDefault(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id && p.langId == langId);
                var getCurrency = _db.Currencies.FirstOrDefault(p => p.Id == hotel.Currency);

                //DateTime dtCheckin = DateTime.Parse(hotel.CheckIn, new CultureInfo("ar-SA"));
                //DateTime dtCheckout = DateTime.Parse(hotel.CheckOut, new CultureInfo("ar-SA"));

                DateTime dtCheckin = DateTime.Parse(hotel.CheckIn).AddHours(2);
                DateTime dtCheckout = DateTime.Parse(hotel.CheckOut).AddHours(2);
                if (getHotelImages.Any())
                {
                    if (getHotelFeatures == null)
                        haveFeature = false;
                    returnList.Add(new Hotels_Translate
                    {
                        Id = hotel.Id,
                        DisplayValue = hotel.DisplayValue,
                        DisplayValueDesc = hotel.DisplayValueDesc,
                        Rate = hotel.Rate,
                        PriceStart = hotel.PriceStart,
                        Currency = hotel.Currency,
                        CurrencyTitle = getCurrency.DisplayValue,
                        IsDeleted = hotel.IsDeleted,
                        HasImage = haveFeature,
                        Image = getHotelImages[0].Image,
                        Late = hotel.Late,
                        Long = hotel.Long,
                        CheckIn = dtCheckin.ToString(),
                        CheckOut = dtCheckout.ToString(),
                        langId = hotel.langId
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
                        Currency = hotel.Currency,
                        CurrencyTitle = getCurrency.DisplayValue,
                        HasImage = false,
                        Late = hotel.Late,
                        Long = hotel.Long,
                        CheckIn = dtCheckin.ToString(),
                        CheckOut = dtCheckout.ToString(),
                        langId = hotel.langId
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
                var getHotelImages = _db.Hotels_Images_Translate.Where(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id && p.langId == langId).ToList();
                var getHotelFeatures = _db.Hotels_Features_Translate.FirstOrDefault(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id && p.langId == langId);

                if (getHotelImages.Any())
                {
                    if (getHotelFeatures == null)
                        continue;
                    returnList.Add(new Hotels_Translate
                    {
                        Id = hotel.Id,
                        DisplayValue = hotel.DisplayValue,
                        DisplayValueDesc = hotel.DisplayValueDesc,
                        Rate = hotel.Rate,
                        PriceStart = hotel.PriceStart,
                        Currency = hotel.Currency,
                        HasImage = true,
                        langId = hotel.langId
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
            var getHotelInfo = _db.Hotels_Translate.Where(p => p.IsDeleted == false && p.Show && p.langId == langId).ToList();
            foreach (var hotel in getHotelInfo)
            {
                var featuresList = new List<SystemParameters_Features_Translate>();
                var getCurrency = _db.Currencies.FirstOrDefault(p => p.Id == hotel.Currency);

                var getHotelFeatures = _db.Hotels_Features_Translate.Where(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id && p.langId == langId).ToList();

                if (!getHotelFeatures.Any())
                    continue;
                foreach (var hotelFeature in getHotelFeatures)
                {
                    var getFeatures = _db.SystemParameters_Features_Translate.Where(p => p.IsDeleted != true && p.Id == hotelFeature.Feature_Id && p.langId == langId).ToList();
                    foreach (var feature in getFeatures)
                    {
                        featuresList.Add(new SystemParameters_Features_Translate
                        {
                            DisplayValue = feature.DisplayValue,
                            Icon = feature.Icon,
                            langId = feature.langId
                        });
                    }
                }

                var getHotelImages = _db.Hotels_Images_Translate.Where(p => p.IsDeleted != true && p.Hotel_Id == hotel.Id && p.langId == langId).ToList();

                if (getHotelImages.Any())
                {
                    returnList.Add(new Hotels_Translate
                    {
                        Id = hotel.Id,
                        DisplayValue = hotel.DisplayValue,
                        DisplayValueDesc = hotel.DisplayValueDesc,
                        Rate = hotel.Rate,
                        PriceStart = hotel.PriceStart,
                        Currency = hotel.Currency,
                        CurrencyTitle = getCurrency.DisplayValue,
                        FeaturesList = featuresList,
                        Image = getHotelImages[0].Image,
                        Bootstrap = 12 / getHotelInfo.Count,
                        langId = hotel.langId
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
                        Currency = hotel.Currency,
                        CurrencyTitle = getCurrency.DisplayValue,
                        FeaturesList = featuresList,
                        Bootstrap = 12 / getHotelInfo.Count,
                        langId = hotel.langId
                    });
                }
            }

            return returnList;
        }
        public Hotels_Translate Get(int id, string langId)
        {
            var returnList = new Hotels_Translate();
            var featuresList = new List<SystemParameters_Features_Translate>();

            var getHotelInfo = _db.Hotels_Translate.FirstOrDefault(p => p.Id == id && p.langId == langId);
            var getCurrency = _db.Currencies.FirstOrDefault(p => p.Id == getHotelInfo.Currency);
            var getHotelFeatures = _db.Hotels_Features_Translate.Where(p => p.Hotel_Id == getHotelInfo.Id && p.langId == langId).ToList();
            foreach (var hotelFeature in getHotelFeatures)
            {
                var getFeatures = _db.SystemParameters_Features_Translate.Where(p => p.Id == hotelFeature.Feature_Id && p.langId == langId).ToList();
                foreach (var feature in getFeatures)
                {
                    featuresList.Add(new SystemParameters_Features_Translate
                    {
                        DisplayValue = feature.DisplayValue,
                        Icon = feature.Icon,
                        langId = feature.langId
                    });
                }
            }
            var getHotelImages = _db.Hotels_Images_Translate.Where(p => p.Hotel_Id == getHotelInfo.Id && p.IsDeleted == false && p.langId == langId).ToList();

            if (getHotelInfo != null)
            {
                //DateTime dtCheckin = DateTime.Parse(getHotelInfo.CheckIn, new CultureInfo("ar-SA"));
                //DateTime dtCheckout = DateTime.Parse(getHotelInfo.CheckOut, new CultureInfo("ar-SA"));

                DateTime dtCheckin = DateTime.Parse(getHotelInfo.CheckIn);
                DateTime dtCheckout = DateTime.Parse(getHotelInfo.CheckOut);
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
                returnList.CurrencyTitle = getCurrency.DisplayValue;
                returnList.FeaturesList = featuresList;
                returnList.Late = getHotelInfo.Late;
                returnList.Long = getHotelInfo.Long;

                returnList.CheckIn = dtCheckin.ToString();
                returnList.CheckOut = dtCheckout.ToString();
                returnList.langId = getHotelInfo.langId;

                return returnList;
            }
            else return null;



        }

        public Hotels_Translate GetWithDeleted(int id, string langId)
        {
            var returnList = new Hotels_Translate();
            var featuresList = new List<SystemParameters_Features_Translate>();

            var getHotelInfo = _db.Hotels_Translate.FirstOrDefault(p => p.Id == id && p.langId == langId);
            var getCurrency = _db.Currencies.FirstOrDefault(p => p.Id == getHotelInfo.Currency);
            var getHotelFeatures = _db.Hotels_Features_Translate.Where(p => p.Hotel_Id == getHotelInfo.Id && p.langId == langId).ToList();
            foreach (var hotelFeature in getHotelFeatures)
            {
                var getFeatures = _db.SystemParameters_Features_Translate.Where(p => p.Id == hotelFeature.Feature_Id && p.langId == langId).ToList();
                foreach (var feature in getFeatures)
                {
                    featuresList.Add(new SystemParameters_Features_Translate
                    {
                        Id = feature.Id,
                        DisplayValue = feature.DisplayValue,
                        Icon = feature.Icon,
                        langId = feature.langId
                    });
                }
            }
            var getHotelImages = _db.Hotels_Images_Translate.Where(p => p.Hotel_Id == getHotelInfo.Id && p.langId == langId).ToList();

            if (getHotelInfo != null)
            {
                //DateTime dtCheckin = DateTime.Parse(getHotelInfo.CheckIn, new CultureInfo("ar-SA"));
                //DateTime dtCheckout = DateTime.Parse(getHotelInfo.CheckOut, new CultureInfo("ar-SA"));

                DateTime dtCheckin = DateTime.Parse(getHotelInfo.CheckIn).AddHours(2);
                DateTime dtCheckout = DateTime.Parse(getHotelInfo.CheckOut).AddHours(2);
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
                returnList.Currency = getHotelInfo.Currency;
                returnList.CurrencyTitle = getCurrency.DisplayValue;
                returnList.FeaturesList = featuresList;
                returnList.CheckIn = dtCheckin.ToString();
                returnList.CheckOut = dtCheckout.ToString();
                returnList.langId = getHotelInfo.langId;
                return returnList;
            }
            else return null;



        }
        public List<Hotels_Images_Translate> GetAllImages(string langId)
        {
            var returnList = new List<Hotels_Images_Translate>();
            var availableHotels = _db.Hotels_Translate.Where(x => x.IsDeleted == false && x.langId == langId).ToList();
            foreach (var availableHotel in availableHotels)
            {
                var imageList = _db.Hotels_Images_Translate.Where(x => x.IsDeleted == false && x.Hotel_Id == availableHotel.Id).ToList();
                var random = imageList.OrderBy(x => Guid.NewGuid()).Take(10);
                foreach (var hotelsImagese in random)
                {
                    returnList.Add(new Hotels_Images_Translate
                    {
                        Image = hotelsImagese.Image
                    });

                }
            }
            return returnList;
        }
        public Hotels_Translate GetHotelInfoById(int id, string langId)
        {
            return _db.Hotels_Translate.FirstOrDefault(p => p.Id == id && p.langId == langId);
        }
        public Hotels_Images_Translate GetImageInfoById(int id, string langId)
        {
            return _db.Hotels_Images_Translate.Find(id);
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
                Currency = postedhotel.Currency,
                Rate = postedhotel.Rate,
                Late = postedhotel.Late,
                Long = postedhotel.Long,
                CheckIn = postedhotel.CheckIn,
                CheckOut = postedhotel.CheckOut,
                ImageList = postedhotel.ImageList,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
                langId = postedhotel.langId
            };
            _db.Hotels_Translate.Add(hotel);
            return Save(hotel);
        }
        public Hotels_Translate Edit(Hotels_Translate postedHotel)
        {
            var hotel = GetHotelInfoById(postedHotel.Id, postedHotel.langId);
            hotel.DisplayValue = postedHotel.DisplayValue;
            hotel.DisplayValueDesc = postedHotel.DisplayValueDesc;
            hotel.IsDeleted = postedHotel.IsDeleted;
            hotel.ImageList = postedHotel.ImageList;
            hotel.PriceStart = postedHotel.PriceStart;
            hotel.Currency = postedHotel.Currency;
            hotel.Rate = postedHotel.Rate;
            hotel.Late = postedHotel.Late;
            hotel.Long = postedHotel.Long;
            hotel.CheckIn = postedHotel.CheckIn;
            hotel.CheckOut = postedHotel.CheckOut;
            hotel.LastModificationTime = Parameters.CurrentDateTime;
            hotel.LastModifierUserId = Parameters.UserId;
            hotel.langId = postedHotel.langId;
            hotel.HasImage = postedHotel.HasImage;
            return Save(hotel);
        }
        public Hotels_Translate Delete(Hotels_Translate postedHotel)
        {
            var hotel = GetHotelInfoById(postedHotel.Id, postedHotel.langId);

            hotel.HasImage = postedHotel.HasImage;

            hotel.IsDeleted = true;
            hotel.CreationTime = Parameters.CurrentDateTime;
            hotel.CreatorUserId = Parameters.UserId;
            return Save(hotel);
        }

        public List<Hotels_Images_Translate> InsertHotelImages(List<Hotels_Images_Translate> postedhotel)
        {
            foreach (var imageObj in postedhotel)
            {
                if (GetImageInfoById(imageObj.Id, postedhotel[0].langId) != null)
                    continue;
                var image = new Hotels_Images_Translate()
                {
                    Image = imageObj.Image,
                    IsDeleted = false,
                    Show = Parameters.Show,
                    Hotel_Id = postedhotel[0].Hotel_Id,
                    LastModificationTime = Parameters.CurrentDateTime,
                    CreationTime = Parameters.CurrentDateTime,
                    CreatorUserId = Parameters.UserId,
                    langId = postedhotel[0].langId
                };
                _db.Hotels_Images_Translate.Add(image);
                _db.SaveChanges();
            }

            postedhotel[0].OperationStatus = "Succeded";
            return postedhotel;
        }

        public Hotels_Images_Translate DeleteImage(Hotels_Images_Translate postedImage, bool isDeleted)
        {
            var imageObj = GetImageInfoById(postedImage.Id, postedImage.langId);

            imageObj.IsDeleted = isDeleted;
            imageObj.DeletionTime = Parameters.CurrentDateTime;
            imageObj.DeleterUserId = Parameters.UserId;
            _db.SaveChanges();
            imageObj.OperationStatus = "Succeded";
            return imageObj;
        }

        public List<Hotels_Features_Translate> InsertHotelFeatures(List<Hotels_Features_Translate> postedfeature)
        {
            var deleteList = DeleteHotelFeatures(postedfeature[0].Hotel_Id, postedfeature[0].langId);
            foreach (var featureObj in postedfeature)
            {
                var feature = new Hotels_Features_Translate()
                {
                    IsDeleted = false,
                    Hotel_Id = postedfeature[0].Hotel_Id,
                    Feature_Id = featureObj.Feature_Id,
                    langId = postedfeature[0].langId,
                };
                _db.Hotels_Features_Translate.Add(feature);
                _db.SaveChanges();
            }
            postedfeature[0].OperationStatus = "Succeded";
            return postedfeature;
        }
        public Hotels_Features_Translate DeleteHotelFeatures(int? id, string langId)
        {
            var featureList = _db.Hotels_Features_Translate.Where(item => item.Id == id && item.langId == langId).ToList();
            if (!featureList.Any())
                return null;
            try
            {
                foreach (var hotelsFeaturese in featureList)
                {
                    _db.Hotels_Features_Translate.Remove(hotelsFeaturese);
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
