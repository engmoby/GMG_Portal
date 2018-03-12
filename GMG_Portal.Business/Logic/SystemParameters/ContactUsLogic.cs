using System;
using System.Collections.Generic;
using System.Linq;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class ContactUsLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public ContactUsLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<ContactU> GetAllWithDeleted()
        {
            return _db.ContactUs.OrderBy(p => p.IsDeleted).ToList();
        }
        public ContactU GetAll()
        {
            return _db.ContactUs.FirstOrDefault(p => p.IsDeleted != true);
        }
        public ContactU Get(int id)
        {
            return _db.ContactUs.Find(id);
        }
        private ContactU Save(ContactU obj)
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
        public List<ContactUs_Translate> GetTranslates(int recordId)
        {
            return _db.ContactUs_Translate.Where(x => x.RecordId == recordId).ToList();
        }
        public ContactU Edit(ContactU postedContactUs)
        {
            ContactU contactUs = Get(postedContactUs.Id);
            List<ContactUs_Translate> currencyTranslate = GetTranslates(postedContactUs.Id);
            foreach (var desc in postedContactUs.DescDictionary)
            { 
                foreach (var objTranslate in currencyTranslate)
                {
                    if (desc.Key == objTranslate.langId)
                    {
                        objTranslate.Description = desc.Value; 
                        _db.SaveChanges();
                    }
                }
            } 
            contactUs.Image = postedContactUs.Image;
            contactUs.Url = postedContactUs.Url;
            contactUs.IsDeleted = postedContactUs.IsDeleted; 
            contactUs.Facebook = postedContactUs.Facebook;
            contactUs.Fax = postedContactUs.Fax;
            contactUs.Twitter = postedContactUs.Twitter;
            contactUs.Youtube = postedContactUs.Youtube;
            contactUs.Instgram = postedContactUs.Instgram;
            contactUs.Snapchat = postedContactUs.Snapchat;
            contactUs.PhoneNo1 = postedContactUs.PhoneNo1;
            contactUs.PhoneNo2 = postedContactUs.PhoneNo2;
            contactUs.PostalCode = postedContactUs.PostalCode;
            contactUs.Late = postedContactUs.Late;
            contactUs.Long = postedContactUs.Long;
            contactUs.Mailbox = postedContactUs.Mailbox;
            contactUs.WhatsApp = postedContactUs.WhatsApp;
            contactUs.MailNo1 = postedContactUs.MailNo1;
            contactUs.MailNo2 = postedContactUs.MailNo2;
            contactUs.LastModificationTime = Parameters.CurrentDateTime;
            contactUs.LastModifierUserId = Parameters.UserId;
            return Save(contactUs);
        }

    }
}
