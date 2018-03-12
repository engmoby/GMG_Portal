using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class CurrencyLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public CurrencyLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<Currency> GetAllWithDeleted()
        {
            return _db.Currencies.OrderBy(p => p.IsDeleted).ToList();
        }
        public List<Currency> GetAll()
        {
            return _db.Currencies.Where(p => p.IsDeleted != true).ToList();
        }
        public Currency Get(int id)
        {
            return _db.Currencies.Find(id);
        }
        public List<Currency_Translate> GetTranslates(int recordId)
        {
            return _db.Currency_Translate.Where(x => x.RecordId == recordId).ToList();
        }
        private Currency Save(Currency currency)
        {
            try
            {
                _db.SaveChanges();
                currency.OperationStatus = "Succeded";
                return currency;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        currency.OperationStatus = "NameArMustBeUnique";
                        return currency;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        currency.OperationStatus = "NameEnMustBeUnique";
                        return currency;
                    }
                }
                throw;
            }
        }
        public Currency Insert(Currency postedCurrency)
        {
            var obj = new Currency()
            {
                IsDeleted = postedCurrency.IsDeleted,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.Currencies.Add(obj);
            _db.SaveChanges();
            var objTrasnlate = new Currency_Translate();
            {
                foreach (var title in postedCurrency.TitleDictionary)
                {
                    objTrasnlate.Title = title.Value;
                  //  objTrasnlate.Description = postedCurrency.DescDictionary[title.Key];
                    objTrasnlate.LangId = title.Key;
                    objTrasnlate.RecordId = obj.Id;
                    _db.Currency_Translate.Add(objTrasnlate);
                    _db.SaveChanges();
                }
            }
            Currency currency = Get(obj.Id);
            List<Currency_Translate> currencyTranslate = GetTranslates(obj.Id);
            return Save(currency);
        }
        public Currency Edit(Currency postedCurrency)
        {
            Currency currency = Get(postedCurrency.Id);
            List<Currency_Translate> currencyTranslate = GetTranslates(postedCurrency.Id);
            foreach (var title in postedCurrency.TitleDictionary)
            { 
                foreach (var objTranslate in currencyTranslate)
                {
                    if (title.Key == objTranslate.LangId)
                    {
                        objTranslate.Title = title.Value;
                       // objTranslate.Description = postedCurrency.DescDictionary[title.Key]; 
                        _db.SaveChanges();
                    }
                }
            }
            currency.IsDeleted = postedCurrency.IsDeleted; 
            currency.LastModificationTime = Parameters.CurrentDateTime;
            currency.LastModifierUserId = Parameters.UserId;
            return Save(currency);
        }
        public Currency Delete(Currency postedCurrency)
        {
            Currency currency = Get(postedCurrency.Id);
            if (_db.Hotels.Any(p => p.Id == postedCurrency.Id && p.IsDeleted != true))
            {
                //  Currency.OperationStatus = "HasRelationship";
                return currency;
            }
            if (_db.Offers.Any(p => p.Id == postedCurrency.Id && p.IsDeleted != true))
            {
                //  Currency.OperationStatus = "HasRelationship";
                return currency;
            }
            currency.IsDeleted = true;
            currency.CreationTime = Parameters.CurrentDateTime;
            currency.CreatorUserId = Parameters.UserId;
            _db.SaveChanges();
            return Save(currency);
        }

    }
}
