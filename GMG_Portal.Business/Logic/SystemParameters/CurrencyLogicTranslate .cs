using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class CurrencyLogicTranslate
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public CurrencyLogicTranslate()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<Currency_Translate> GetAllWithDeleted(string langId)
        {
            return _db.Currency_Translate.Where(p => p.LangId == langId).OrderBy(p => p.IsDeleted && p.LangId == langId).ToList();
        }
        public Currency_Translate GetAll(string langId)
        {
            return _db.Currency_Translate.FirstOrDefault(p => p.IsDeleted != true && p.LangId == langId);
        }
        public Currency_Translate Get(int id , string langId)
        {
            return _db.Currency_Translate.FirstOrDefault(x => x.Id == id && x.LangId == langId);
        }



        private Currency_Translate Save(Currency_Translate obj)
        {
            try
            {
                _db.SaveChanges();
                obj.OperationStatus = "Succeded";
                return obj;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        obj.OperationStatus = "NameArMustBeUnique";
                        return obj;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        obj.OperationStatus = "NameEnMustBeUnique";
                        return obj;
                    }
                }
                throw;
            }
        }
        public Currency_Translate Insert(Currency_Translate postedCurrency)
        { 
            var currency = new Currency_Translate()
            {
                DisplayValue = postedCurrency.DisplayValue,
                DisplayValueDesc = postedCurrency.DisplayValueDesc, 
                IsDeleted = postedCurrency.IsDeleted,
                Show = Parameters.Show,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
                LangId = postedCurrency.LangId
            };
            _db.Currency_Translate.Add(currency);
            return Save(currency);
        }
        public Currency_Translate Edit(Currency_Translate postedCurrency)
        {
            Currency_Translate obj = Get(postedCurrency.Id ,postedCurrency.LangId);
            obj.DisplayValue = postedCurrency.DisplayValue;
            obj.DisplayValueDesc = postedCurrency.DisplayValueDesc; 
            obj.IsDeleted = postedCurrency.IsDeleted;
            obj.Show = postedCurrency.Show;
            obj.LastModificationTime = Parameters.CurrentDateTime;
            obj.LastModifierUserId = Parameters.UserId;
            return Save(obj);
        }
        public Currency_Translate Delete(Currency_Translate postedCurrency)
        {
            Currency_Translate obj = Get(postedCurrency.Id,postedCurrency.LangId);
            if (_db.Hotels_Translate.Any(p => p.Id == postedCurrency.Id && p.IsDeleted != true))
            {
                postedCurrency.OperationStatus = "HasRelationship";
                return obj;
            }
            if (_db.Hotels_Translate.Any(p => p.Id == postedCurrency.Id && p.IsDeleted != true))
            {
                postedCurrency.OperationStatus = "HasRelationship";
                return obj;
            }
            obj.IsDeleted = true;
            obj.CreationTime = Parameters.CurrentDateTime;
            obj.CreatorUserId = Parameters.UserId;
            return Save(obj);
        }
        public List<Currency_Translate> GetCurrencyAll()
        {
            return _db.Currency_Translate.Where(p => p.IsDeleted != true).ToList();
        }
        
    }
}