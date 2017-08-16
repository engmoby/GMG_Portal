using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public List<SystemParameters_Owners> GetAllWithDeleted()
        {
            return _db.SystemParameters_Owners.OrderBy(p => p.IsDeleted).ToList();
        }
        public List<SystemParameters_Owners> GetAll()
        {
            var returnList = new List<SystemParameters_Owners>();
            var getOwnerList = _db.SystemParameters_Owners.Where(p => p.IsDeleted == false && p.Show == true).ToList();
            foreach (var ownerse in getOwnerList)
            {

                returnList.Add(new SystemParameters_Owners
                {
                    Id = ownerse.Id, 
                    DisplayValueName = ownerse.DisplayValueName,
                    DisplayValuePosition= ownerse.DisplayValuePosition,
                    DisplayValueDesc = ownerse.DisplayValueDesc, 
                    Image = ownerse.Image,
                    Facebook = ownerse.Facebook,
                    Twitter = ownerse.Twitter,
                    LinkedIn = ownerse.LinkedIn,
                    Bootstrap = 12 / getOwnerList.Count
                });
            }
            return returnList;
            //return _db.SystemParameters_Owners.Where(p => p.IsDeleted == false && (bool) p.Show).ToList();
        }
        public SystemParameters_Owners Get(int id)
        {
            return _db.SystemParameters_Owners.Find(id);
        }
        private SystemParameters_Owners Save(SystemParameters_Owners obj)
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
        public SystemParameters_Owners Insert(SystemParameters_Owners postedOwner)
        {

            var obj = new SystemParameters_Owners()
            {
                DisplayValueName = postedOwner.DisplayValueName,
                DisplayValuePosition = postedOwner.DisplayValuePosition,
                DisplayValueDesc = postedOwner.DisplayValueDesc,
                Image= postedOwner.Image, 
                Show = Parameters.Show,  
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId, 
            };
            _db.SystemParameters_Owners.Add(obj);
            return Save(obj);
        }
        public SystemParameters_Owners Edit(SystemParameters_Owners postedOwner)
        {
            SystemParameters_Owners obj = Get(postedOwner.Id);
            obj.DisplayValueName = postedOwner.DisplayValueName;
            obj.DisplayValuePosition = postedOwner.DisplayValuePosition;
            obj.DisplayValueDesc = postedOwner.DisplayValueDesc;
            obj.Image = postedOwner.Image; 
            obj.IsDeleted = postedOwner.IsDeleted;
            obj.Show = postedOwner.Show; 
            obj.LastModificationTime = Parameters.CurrentDateTime;
            obj.LastModifierUserId = Parameters.UserId;
            return Save(obj);
        }
        public SystemParameters_Owners Delete(SystemParameters_Owners postedOwner)
        {
            SystemParameters_Owners obj = Get(postedOwner.Id);
            //if (_db.SystemParameters_Owners.Any(p => p.Id == postedOwner.Id && p.IsDeleted != true))
            //{
            //      //  Owner.OperationStatus = "HasRelationship";
            //    return obj;
            //}

            obj.IsDeleted = true;
            obj.CreationTime = Parameters.CurrentDateTime;
            obj.CreatorUserId = Parameters.UserId;
            return Save(obj);
        }

    }
}
