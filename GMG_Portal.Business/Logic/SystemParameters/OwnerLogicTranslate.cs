using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper; 

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class OwnerLogicTranslate
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public OwnerLogicTranslate()
        {
            _db = new GMG_Portal_DBEntities1();
        }

        //Back and Front  Fetch Logic 
        public List<SystemParameters_Owners_Translate> GetAllWithDeleted(string langId)
        {
            return _db.SystemParameters_Owners_Translate.Where(p => p.langId == langId).OrderBy(p => p.IsDeleted && p.langId == langId).ToList();
        }
        public List<SystemParameters_Owners_Translate> GetAll(string langId)
        {
            return _db.SystemParameters_Owners_Translate.Where(p => p.IsDeleted != true && p.langId == langId).ToList();
        }


        //Crud Logic 
        public SystemParameters_Owners_Translate Get(int id, string langId)
        {
            return _db.SystemParameters_Owners_Translate.FirstOrDefault(x => x.Id == id && x.langId == langId);
        }
        private SystemParameters_Owners_Translate Save(SystemParameters_Owners_Translate obj)
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
        public SystemParameters_Owners_Translate Insert(SystemParameters_Owners_Translate postedOwner)
        {

            var obj = new SystemParameters_Owners_Translate()
            {
                DisplayValueName = postedOwner.DisplayValueName,
                DisplayValuePosition = postedOwner.DisplayValuePosition,
                DisplayValueDesc = postedOwner.DisplayValueDesc,
                Image= postedOwner.Image, 
                Show = Parameters.Show,  
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId, 
                langId  = postedOwner.langId,
            };
            _db.SystemParameters_Owners_Translate.Add(obj);
            return Save(obj);
        }
        public SystemParameters_Owners_Translate Edit(SystemParameters_Owners_Translate postedOwner)
        {
            SystemParameters_Owners_Translate obj = Get(postedOwner.Id,postedOwner.langId);
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
        public SystemParameters_Owners_Translate Delete(SystemParameters_Owners_Translate postedOwner)
        {
            SystemParameters_Owners_Translate obj = Get(postedOwner.Id,postedOwner.langId);
         
            obj.IsDeleted = true;
            obj.CreationTime = Parameters.CurrentDateTime;
            obj.CreatorUserId = Parameters.UserId;
            return Save(obj);
        }

    }
}
