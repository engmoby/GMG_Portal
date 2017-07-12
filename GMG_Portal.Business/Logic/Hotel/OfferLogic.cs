using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class OfferLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public OfferLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<Hotles_Offers> GetAllWithDeleted()
        {
            return _db.Hotles_Offers.OrderBy(p => p.IsDeleted).ToList();
        }
        public List<Hotles_Offers> GetAll()
        {
            var returnList = new List<Hotles_Offers>();
            returnList = _db.Hotles_Offers.Where(p => p.IsDeleted == false && p.Show == true).ToList();
            //foreach (var offer in getOfferInfo)
            //{
            //    var getOfferImages = _db.Offers_Offer_Images.Where(p => p.IsDeleted != true && p.Offer_Id == offer.Id).ToList();
            //    returnList.Add(new Offer
            //    {
            //        Id = offer.Id,
            //        DisplayValue = offer.DisplayValue,
            //        DisplayValueDesc = offer.DisplayValueDesc,
            //        Rate = offer.Rate,
            //        PriceStart = offer.PriceStart,
            //        Image = getOfferImages[0].Image,
            //        Bootstrap = 12 / getOfferInfo.Count
            //    });
            //}
            return returnList;
            // return _db.Offers.Where(p => p.IsDeleted != true).ToList();
        }
        public Hotles_Offers Get(int id)
        {
            var returnList = new Hotles_Offers();
             
            var getOfferInfo = _db.Hotles_Offers.FirstOrDefault(p => p.Id== id&& p.IsDeleted == false && p.Show== true);
            var getOfferhotel= _db.Hotels.FirstOrDefault(p => p.IsDeleted != true && p.Id == getOfferInfo.Hotel_Id);

            var getOfferImages = _db.Hotels_Images.Where(p => p.IsDeleted != true && p.Hotel_Id == getOfferhotel.Id).ToList();

            if (getOfferInfo != null)
            {
                returnList.Id = getOfferInfo.Id;
                returnList.DisplayValue = getOfferInfo.DisplayValue;
                returnList.DisplayValueDesc = getOfferInfo.DisplayValueDesc;
                returnList.Price = getOfferInfo.Price;
                returnList.StartDate = getOfferInfo.StartDate;
                returnList.EndDate = getOfferInfo.EndDate;
                returnList.Image = getOfferImages[0].Image; 
                //returnList.ImageList = getOfferImages[0].Image; 


                return returnList;
            }
            else return null;

        } 
        private Hotles_Offers Save(Hotles_Offers offer)
        {
            try
            {
                _db.SaveChanges();
                offer.OperationStatus = "Succeded";
                return offer;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        offer.OperationStatus = "NameArMustBeUnique";
                        return offer;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        offer.OperationStatus = "NameEnMustBeUnique";
                        return offer;
                    }
                }
                throw;
            }
        }
        public Hotles_Offers Insert(Hotles_Offers postedoffer)
        { 
            var offer = new Hotles_Offers()
            {
                DisplayValue = postedoffer.DisplayValue,
                DisplayValueDesc = postedoffer.DisplayValueDesc, 
                StartDate = postedoffer.StartDate, 
                EndDate = postedoffer.EndDate, 
                Price = postedoffer.Price,
                Image = postedoffer.Image,
                IsDeleted = postedoffer.IsDeleted,
                Show = Parameters.Show,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.Hotles_Offers.Add(offer);
            return Save(offer);
        }
        public Hotles_Offers Edit(Hotles_Offers postedOffer)
        {
            Hotles_Offers offer = Get(postedOffer.Id);
            offer.DisplayValue = postedOffer.DisplayValue;
            offer.DisplayValueDesc = postedOffer.DisplayValueDesc;
            offer.IsDeleted = postedOffer.IsDeleted;
            offer.Show = postedOffer.Show;
            offer.StartDate = postedOffer.StartDate;
            offer.EndDate = postedOffer.EndDate;
            offer.Price = postedOffer.Price;
            offer.Image = postedOffer.Image;
            offer.LastModificationTime = Parameters.CurrentDateTime;
            offer.LastModifierUserId = Parameters.UserId;
            return Save(offer);
        }
        public Hotles_Offers Delete(Hotles_Offers postedOffer)
        {
            Hotles_Offers offer = Get(postedOffer.Id);
            if (_db.Hotles_Offers.Any(p => p.Id == postedOffer.Id && p.IsDeleted != true))
            {
                //  Offer.OperationStatus = "HasRelationship";
                return offer;
            }

            offer.IsDeleted = true;
            offer.CreationTime = Parameters.CurrentDateTime;
            offer.CreatorUserId = Parameters.UserId;
            return Save(offer);
        }

    }
}
