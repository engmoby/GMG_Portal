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
        public Currency GetAll()
        {
            return _db.Currencies.FirstOrDefault(p => p.IsDeleted != true);
        }
        public Currency Get(int id)
        {
            return _db.Currencies.Find(id);
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

            var currency = new Currency()
            {
                DisplayValue = postedCurrency.DisplayValue,
                DisplayValueDesc = postedCurrency.DisplayValueDesc,
                IsDeleted = postedCurrency.IsDeleted,
                Show = Parameters.Show,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.Currencies.Add(currency);
            return Save(currency);
        }
        public Currency Edit(Currency postedCurrency)
        {
            Currency currency = Get(postedCurrency.Id);
            currency.DisplayValue = postedCurrency.DisplayValue;
            currency.DisplayValueDesc = postedCurrency.DisplayValueDesc;
            currency.IsDeleted = postedCurrency.IsDeleted;
            currency.Show = postedCurrency.Show;
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
            if (_db.Hotles_Offers.Any(p => p.Id == postedCurrency.Id && p.IsDeleted != true))
            {
                //  Currency.OperationStatus = "HasRelationship";
                return currency;
            }
            currency.IsDeleted = true;
            currency.CreationTime = Parameters.CurrentDateTime;
            currency.CreatorUserId = Parameters.UserId;
            return Save(currency);
        }
        
    }
}
