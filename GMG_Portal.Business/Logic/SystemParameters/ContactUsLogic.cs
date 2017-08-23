using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public List<SystemParameters_ContactUs> GetAllWithDeleted()
        {
            return _db.SystemParameters_ContactUs.OrderBy(p => p.IsDeleted).ToList();
        }
        public  SystemParameters_ContactUs GetAll()
        {
            return _db.SystemParameters_ContactUs.FirstOrDefault(p => p.IsDeleted != true);
        }
        public SystemParameters_ContactUs Get(int id)
        {
            return _db.SystemParameters_ContactUs.Find(id);
        }
        private SystemParameters_ContactUs Save(SystemParameters_ContactUs obj)
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
        public SystemParameters_ContactUs Edit(SystemParameters_ContactUs postedContactUs)
        {
            SystemParameters_ContactUs contactUs = Get(postedContactUs.Id);
            contactUs.DisplayValueAddress = postedContactUs.DisplayValueAddress;
            contactUs.DisplayValueDesc = postedContactUs.DisplayValueDesc;
            contactUs.Image = postedContactUs.Image;
            contactUs.Url = postedContactUs.Url;
            contactUs.IsDeleted = postedContactUs.IsDeleted;
            contactUs.Show = postedContactUs.Show;
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
            contactUs.MailNo2= postedContactUs.MailNo2;
            contactUs.LastModificationTime = Parameters.CurrentDateTime;
            contactUs.LastModifierUserId = Parameters.UserId;
            return Save(contactUs);
        } 

    }
}
