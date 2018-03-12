using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class CoreValuesLogic
   
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public CoreValuesLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<CoreValue> GetAllWithDeleted()
        {
            return _db.CoreValues.OrderBy(p => p.IsDeleted).ToList();
        }
        public List<CoreValue> GetAll()
        {
            return _db.CoreValues.Where(p => p.IsDeleted != true).ToList();
        }
        public CoreValue Get(int id)
        {
            return _db.CoreValues.Find(id);
        }
        private CoreValue Save(CoreValue obj)
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
        public CoreValue Insert(CoreValue postedCoreValue)
        { 
            var obj = new CoreValue()
            { 
                IsDeleted = postedCoreValue.IsDeleted, 
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.CoreValues.Add(obj);
            _db.SaveChanges();
            var objTrasnlate = new CoreValues_Translate();
            {
                foreach (var title in postedCurrency.TitleDictionary)
                {
                    objTrasnlate.Title = title.Value;
                    objTrasnlate.Description = postedCurrency.DescDictionary[title.Key];
                    objTrasnlate.LangId = title.Key;
                    objTrasnlate.RecordId = obj.Id;
                    _db.Currency_Translate.Add(objTrasnlate);
                    _db.SaveChanges();
                }
            }
            Currency currency = Get(postedCurrency.Id);
            List<Currency_Translate> currencyTranslate = GetTranslates(postedCurrency.Id);
            return Save(currency);
        }
        public CoreValue Edit(CoreValue postedCoreValue)
        {
            CoreValue obj = Get(postedCoreValue.Id);
            obj.DisplayValue = postedCoreValue.DisplayValue;
            obj.DisplayValueDesc = postedCoreValue.DisplayValueDesc;
            obj.Icon = postedCoreValue.Icon; 
            obj.IsDeleted = postedCoreValue.IsDeleted;
            obj.Show = postedCoreValue.Show;
            obj.LastModificationTime = Parameters.CurrentDateTime;
            obj.LastModifierUserId = Parameters.UserId;
            return Save(obj);
        }
        public CoreValue Delete(CoreValue postedCoreValue)
        {
            CoreValue obj = Get(postedCoreValue.Id); 

            obj.IsDeleted = true;
            obj.CreationTime = Parameters.CurrentDateTime;
            obj.CreatorUserId = Parameters.UserId;
            return Save(obj);
        }

    }
}
