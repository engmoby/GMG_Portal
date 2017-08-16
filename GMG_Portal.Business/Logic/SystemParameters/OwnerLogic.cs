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
        private SystemParameters_Owners Save(SystemParameters_Owners owner)
        {
            try
            {
                _db.SaveChanges();
                owner.OperationStatus = "Succeded";
                return owner;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        owner.OperationStatus = "NameArMustBeUnique";
                        return owner;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        owner.OperationStatus = "NameEnMustBeUnique";
                        return owner;
                    }
                }
                throw;
            }
        }
        public SystemParameters_Owners Insert(SystemParameters_Owners postedOwner)
        {

            var owner = new SystemParameters_Owners()
            {
                DisplayValueName = postedOwner.DisplayValueName,
                DisplayValuePosition = postedOwner.DisplayValuePosition,
                DisplayValueDesc = postedOwner.DisplayValueDesc,
                Facebook= postedOwner.Facebook, 
                Twitter= postedOwner.Twitter, 
                Image= postedOwner.Image, 
                LinkedIn= postedOwner.LinkedIn,
                Show = Parameters.Show,  
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId, 
            };
            _db.SystemParameters_Owners.Add(owner);
            return Save(owner);
        }
        public SystemParameters_Owners Edit(SystemParameters_Owners postedOwner)
        {
            SystemParameters_Owners owner = Get(postedOwner.Id);
            owner.DisplayValueName = postedOwner.DisplayValueName;
            owner.DisplayValuePosition = postedOwner.DisplayValuePosition;
            owner.DisplayValueDesc = postedOwner.DisplayValueDesc;
            owner.Facebook = postedOwner.Facebook;
            owner.Twitter = postedOwner.Twitter;
            owner.Image = postedOwner.Image;
            owner.LinkedIn = postedOwner.LinkedIn;
            owner.IsDeleted = postedOwner.IsDeleted;
            owner.Show = postedOwner.Show; 
            owner.LastModificationTime = Parameters.CurrentDateTime;
            owner.LastModifierUserId = Parameters.UserId;
            return Save(owner);
        }
        public SystemParameters_Owners Delete(SystemParameters_Owners postedOwner)
        {
            SystemParameters_Owners owner = Get(postedOwner.Id);
            //if (_db.SystemParameters_Owners.Any(p => p.Id == postedOwner.Id && p.IsDeleted != true))
            //{
            //      //  Owner.OperationStatus = "HasRelationship";
            //    return owner;
            //}

            owner.IsDeleted = true;
            owner.CreationTime = Parameters.CurrentDateTime;
            owner.CreatorUserId = Parameters.UserId;
            return Save(owner);
        }

    }
}
