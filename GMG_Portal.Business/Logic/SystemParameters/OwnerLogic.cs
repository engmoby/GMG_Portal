using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class OwnerLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public OwnerLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<Owner> GetAllWithDeleted()
        {
            return _db.Owners.OrderBy(p => p.IsDeleted).ThenBy(p => p.Sorder).ToList();
        }
        public List<Owner> GetAll()
        {
            return _db.Owners.Where(p => p.IsDeleted == false).OrderBy(p => p.IsDeleted).ThenBy(p => p.Sorder).ToList();
            //var returnList = new List<SystemParameters_Owners>();
            //var getOwnerList = _db.SystemParameters_Owners.Where(p => p.IsDeleted == false && p.Show == true).ToList().OrderBy(p => p.Sorder).ToList();
            //foreach (var ownerse in getOwnerList)
            //{

            //    returnList.Add(new SystemParameters_Owners
            //    {
            //        Id = ownerse.Id,
            //        DisplayValueName = ownerse.DisplayValueName,
            //        DisplayValuePosition = ownerse.DisplayValuePosition,
            //        DisplayValueDesc = ownerse.DisplayValueDesc,
            //        Image = ownerse.Image,
            //        Facebook = ownerse.Facebook,
            //        Twitter = ownerse.Twitter,
            //        LinkedIn = ownerse.LinkedIn,
            //        Bootstrap = 12 / getOwnerList.Count,
            //        Sorder = ownerse.Sorder

            //    });
            //}
            //return returnList;
            //return _db.SystemParameters_Owners.Where(p => p.IsDeleted == false && (bool) p.Show).ToList();
        }
        public Owner Get(int id)
        {
            return _db.Owners.Find(id);
        }
        public List<Owners_Translate> GetTranslates(int recordId)
        {
            return _db.Owners_Translate.Where(x => x.RecordId == recordId).ToList();
        }
        public Owner GetByOrder(int sorder)
        {
            //var getOwnerOrder = _db.SystemParameters_Owners.Where(p => p.IsDeleted == false && p.Show == true && p.Sorder == Sorder ).ToList().OrderBy(p => p.Sorder).ToList();
            return _db.Owners.FirstOrDefault(p => p.IsDeleted == false && p.Show == true && p.Sorder == sorder);
        }
        private Owner Save(Owner obj)
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
        public Owner Insert(Owner postedOwner)
        {

            // var maxcount = GetMaxCountofOrder();
            var obj = new Owner()
            {
                Facebook = postedOwner.Facebook,
                Twitter = postedOwner.Twitter,
                Image = postedOwner.Image,
                LinkedIn = postedOwner.LinkedIn,
                Show = Parameters.Show,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
                Sorder = 1,

            };
            _db.Owners.Add(obj);
            _db.SaveChanges();
            var objTrasnlate = new Owners_Translate();
            {
                foreach (var ownerName in postedOwner.OwnerNameDictionary)
                {
                    objTrasnlate.DisplayValueName = ownerName.Value;
                    objTrasnlate.DisplayValuePosition = postedOwner.OwnerPostionDictionary[ownerName.Key];
                    objTrasnlate.DisplayValueDesc = postedOwner.OwnerDescDictionary[ownerName.Key];
                    objTrasnlate.langId = ownerName.Key;
                    objTrasnlate.RecordId = obj.Id;
                    _db.Owners_Translate.Add(objTrasnlate);
                    _db.SaveChanges();
                }
            }
            Owner owner = Get(obj.Id);
            List<Owners_Translate> ownerTranslate = GetTranslates(obj.Id);
            return Save(owner);
        }
        public Owner Edit(Owner postedOwner)
        {
            Owner owner = Get(postedOwner.Id);
            List<Owners_Translate> ownerTranslate = GetTranslates(postedOwner.Id);
            foreach (var ownerName in postedOwner.OwnerNameDictionary)
            {
                //var systemParametersOwnersTranslate = ownerTranslate.FirstOrDefault(x => x.langId == ownerName.Key); 
                //var b = systemParametersOwnersTranslate != null && systemParametersOwnersTranslate.DisplayValueName == ownerName.Value;
                //_db.SaveChanges();
                foreach (var systemParametersOwnersTranslate in ownerTranslate)
                {
                    if (ownerName.Key == systemParametersOwnersTranslate.langId)
                    {
                        systemParametersOwnersTranslate.DisplayValueName = ownerName.Value;
                        systemParametersOwnersTranslate.DisplayValueDesc = postedOwner.OwnerDescDictionary[ownerName.Key];
                        systemParametersOwnersTranslate.DisplayValuePosition = postedOwner.OwnerPostionDictionary[ownerName.Key];
                        _db.SaveChanges();
                    }
                }
            }
            owner.Facebook = postedOwner.Facebook;
            owner.Twitter = postedOwner.Twitter;
            owner.Image = postedOwner.Image;
            owner.LinkedIn = postedOwner.LinkedIn;
            owner.IsDeleted = postedOwner.IsDeleted;
            owner.Show = postedOwner.Show;
            owner.LastModificationTime = Parameters.CurrentDateTime;
            owner.LastModifierUserId = Parameters.UserId;
            ////Update to Magically Replace the Numbers !
            //var corder = GetCurrentOrder(postedOwner.Id);
            //Savetheorder(postedOwner.Sorder, corder, postedOwner.Id);
            //_db.SaveChanges();
            return Save(owner);
        }
        public Owner Delete(Owner postedOwner)
        {
            Owner obj = Get(postedOwner.Id);
            //if (_db.SystemParameters_Owners.Any(p => p.Id == postedOwner.Id && p.IsDeleted != true))
            //{
            //      //  Owner.OperationStatus = "HasRelationship";
            //    return obj;
            //}

            obj.IsDeleted = true;
            obj.DeletionTime = Parameters.CurrentDateTime;
            obj.DeleterUserId = Parameters.UserId;
            _db.SaveChanges();
            return Save(obj);
        }
        public List<Owner> OrderOwner(List<Owner> postedOwner)
        {
            //List<Owner> obj = GetAll();
            //for (int i = 0; i < obj.Count; i++)
            //{
            //    obj[i].Sorder = orderInts[i];
            //}
            foreach (var owner in postedOwner)
            {
                var objOwner = Get(owner.Id);
                objOwner.Sorder = owner.Sorder;
                _db.SaveChanges();
            }
            return GetAllWithDeleted();
        }
    }
}
