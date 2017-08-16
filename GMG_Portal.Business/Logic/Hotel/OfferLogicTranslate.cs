using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class OfferLogicTranslate
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public OfferLogicTranslate()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<Hotles_Offers_Translate> GetAllWithDeleted(string langId)
        {
            return _db.Hotles_Offers_Translate.OrderBy(p => p.IsDeleted && p.langId == langId).ToList();
        }
        public List<Hotles_Offers_Translate> GetAll(string langId)
        {
             var returnList = new List<Hotles_Offers_Translate>();
             var offerList = _db.Hotles_Offers_Translate.Where(p => p.IsDeleted == false && p.Show == true && p.langId == langId).OrderByDescending(o => o.Id).ToList();

            foreach (var offer in offerList)
            {
               // var getHotelInfo = _db.Hotels.Where(p => p.IsDeleted != true && p.Id == offer.Hotel_Id).ToList();
                returnList.Add(new Hotles_Offers_Translate
                {
                    Id = offer.Id,
                    DisplayValue = offer.DisplayValue,
                    DisplayValueDesc = offer.DisplayValueDesc,
                    Image = offer.Image,
                    StartDate = offer.StartDate,
                    EndDate = offer.EndDate,
                    Price = offer.Price, 
                });
            }
            return offerList;
            //return _db.Hotles_Offers_Translate.Where(p => p.IsDeleted != true).ToList();
        }
        public Hotles_Offers_Translate Get(int id, string langId)
        {
            var returnList = new Hotles_Offers_Translate();

            var getOfferInfo = _db.Hotles_Offers_Translate.FirstOrDefault(p => p.Id == id && p.IsDeleted == false && p.Show == true && p.langId == langId);  
            if (getOfferInfo != null)
            {
                returnList.Id = getOfferInfo.Id;
                returnList.DisplayValue = getOfferInfo.DisplayValue;
                returnList.DisplayValueDesc = getOfferInfo.DisplayValueDesc;
                returnList.Price = getOfferInfo.Price;
                returnList.StartDate = getOfferInfo.StartDate;
                returnList.EndDate = getOfferInfo.EndDate;
                returnList.Image = getOfferInfo.Image; 


                return returnList;
            }
            else return null;

        }
        public Hotles_Offers_Translate GetOfferInfo(int id, string langId)
        { 
           return _db.Hotles_Offers_Translate.FirstOrDefault(x => x.Id == id && x.langId == langId);
            
        }
        private Hotles_Offers_Translate Save(Hotles_Offers_Translate offer)
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
        public Hotles_Offers_Translate Insert(Hotles_Offers_Translate postedoffer)
        {
            var offer = new Hotles_Offers_Translate()
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
                langId = postedoffer.langId,
            };
            _db.Hotles_Offers_Translate.Add(offer);
            return Save(offer);
        }
        public Hotles_Offers_Translate Edit(Hotles_Offers_Translate postedOffer)
        {
            Hotles_Offers_Translate offer = GetOfferInfo(postedOffer.Id,postedOffer.langId);
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
        public Hotles_Offers_Translate Delete(Hotles_Offers_Translate postedOffer)
        {
            Hotles_Offers_Translate offer = GetOfferInfo(postedOffer.Id,postedOffer.langId);
       

            offer.IsDeleted = true;
            offer.CreationTime = Parameters.CurrentDateTime;
            offer.CreatorUserId = Parameters.UserId;
            return Save(offer);
        }

    }
}
