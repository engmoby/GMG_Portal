using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<Offer> GetAllWithDeleted()
        { 
            var offerList = _db.Offers.OrderByDescending(o => o.Id).ToList();  
            return offerList;
        }
        public List<Offer> GetAll()
        {
             var returnList = new List<Offer>();
             return _db.Offers.Where(p => p.IsDeleted == false).OrderByDescending(o => o.Id).ToList();

            //foreach (var offer in offerList)
            //{
            //    var getCurrency = _db.Currency_Translate.FirstOrDefault(p => p.RecordId == offer.Currency); 
            //    // var getHotelInfo = _db.Hotels.Where(p => p.IsDeleted != true && p.Id == offer.Hotel_Id).ToList();
            //    returnList.Add(new Offer
            //    {
            //        Id = offer.Id, 
            //        Image = offer.Image,
            //        StartDate = offer.StartDate,
            //        EndDate = offer.EndDate,
            //        Price = offer.Price,
            //        Currency = offer.Currency,
            //        CurrencyTitle = getCurrency.Title,
            //    });
            //}
            //return returnList;
            //return _db.Hotles_Offers.Where(p => p.IsDeleted != true).CurrencyTitle();0
        }
        public Offer Get(int id)
        {
            var returnList = new Offer();
           
           return _db.Offers.FirstOrDefault(p => p.Id == id && p.IsDeleted == false);  
            //if (getOfferInfo != null)
            //{
            //    returnList.Id = getOfferInfo.Id; 
            //    returnList.Price = getOfferInfo.Price;
            //    returnList.StartDate = getOfferInfo.StartDate;
            //    returnList.EndDate = getOfferInfo.EndDate;
            //    returnList.Image = getOfferInfo.Image;
            //    returnList.Currency = getOfferInfo.Currency;
            //    returnList.CurrencyTitle = _db.Currency_Translate.FirstOrDefault(p => p.RecordId== getOfferInfo.Currency)?.Title;
            //    return returnList;
            //}
            //else return null;

        }
        public List<Offers_Translate> GetTranslates(int recordId)
        {
            return _db.Offers_Translate.Where(x => x.RecordId == recordId).ToList();
        }

        public Offer GetOfferInfo(int id)
        { 
           return _db.Offers.Find(id);
            
        }
        private Offer Save(Offer offer)
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
        public Offer Insert(Offer postedoffer)
        {
            var obj = new Offer()
            { 
                StartDate = postedoffer.StartDate,
                EndDate = postedoffer.EndDate,
                Price = postedoffer.Price,
                Image = postedoffer.Image,
                IsDeleted = postedoffer.IsDeleted, 
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
                Currency =  postedoffer.Currency
            };
            _db.Offers.Add(obj);
            _db.SaveChanges();
            var objTrasnlate = new Offers_Translate();
            {
                foreach (var offerTitle in postedoffer.OfferTitleDictionary)
                {
                    objTrasnlate.Title = offerTitle.Value;
                    objTrasnlate.Description= postedoffer.OfferDescDictionary[offerTitle.Key]; 
                    objTrasnlate.langId = offerTitle.Key;
                    objTrasnlate.RecordId = obj.Id;
                    _db.Offers_Translate.Add(objTrasnlate);
                    _db.SaveChanges();
                }
            }
            return Save(obj);
        }
        public Offer Edit(Offer postedOffer)
        {
            Offer offer = GetOfferInfo(postedOffer.Id);

            List<Offers_Translate> objTranslate = GetTranslates(postedOffer.Id);
            foreach (var offerTitle in postedOffer.OfferTitleDictionary)
            { 
                foreach (var offerTranslate in objTranslate)
                {
                    if (offerTitle.Key == offerTranslate.langId)
                    {
                        offerTranslate.Title = offerTitle.Value;
                        offerTranslate.Description= postedOffer.OfferDescDictionary[offerTitle.Key]; 
                        _db.SaveChanges();
                    }
                }
            }

            offer.IsDeleted = postedOffer.IsDeleted; 
            //offer.StartDate = postedOffer.StartDate;
            //offer.EndDate = postedOffer.EndDate;
            offer.Price = postedOffer.Price;
            offer.Image = postedOffer.Image;
            offer.LastModificationTime = Parameters.CurrentDateTime;
            offer.LastModifierUserId = Parameters.UserId;
            offer.Currency = postedOffer.Currency;
            return Save(offer);
        }
        public Offer Delete(Offer postedOffer)
        {
            Offer offer = GetOfferInfo(postedOffer.Id);
       

            offer.IsDeleted = true;
            offer.CreationTime = Parameters.CurrentDateTime;
            offer.CreatorUserId = Parameters.UserId;
            _db.SaveChanges();
            return Save(offer);
        }

    }
}
