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
        public List<SystemParameters_ContactUs> GetAll()
        {
            return _db.SystemParameters_ContactUs.Where(p => p.IsDeleted != true).ToList();
        }
        public SystemParameters_ContactUs Get(int id)
        {
            return _db.SystemParameters_ContactUs.Find(id);
        }
        private SystemParameters_ContactUs Save(SystemParameters_ContactUs contactUs)
        {
            try
            {
                _db.SaveChanges();
                contactUs.OperationStatus = "Succeded";
                return contactUs;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        contactUs.OperationStatus = "NameArMustBeUnique";
                        return contactUs;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        contactUs.OperationStatus = "NameEnMustBeUnique";
                        return contactUs;
                    }
                }
                throw;
            }
        }
        public SystemParameters_ContactUs Insert(SystemParameters_ContactUs postedContactUs)
        {

            var contactUs = new SystemParameters_ContactUs()
            {
                DisplayValueAddress = postedContactUs.DisplayValueAddress,
                DisplayValueDesc = postedContactUs.DisplayValueDesc,
                Image = postedContactUs.Image,
                Show = Parameters.Show,
                Url = postedContactUs.Url,
                Facebook = postedContactUs.Facebook,
                Fax = postedContactUs.Fax,
                Twitter = postedContactUs.Twitter,
                Youtube = postedContactUs.Youtube,
                Instgram = postedContactUs.Instgram,
                Snapchat = postedContactUs.Snapchat,
                PhoneNo1 = postedContactUs.PhoneNo1,
                PhoneNo2 = postedContactUs.PhoneNo2,
                PostalCode = postedContactUs.PostalCode,
                Late = postedContactUs.Late,
                Long = postedContactUs.Long,
                Mailbox = postedContactUs.Mailbox,
                WhatsApp = postedContactUs.WhatsApp,
                MailNo1 = postedContactUs.MailNo1,
                MailNo2 = postedContactUs.MailNo2,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.SystemParameters_ContactUs.Add(contactUs);
            return Save(contactUs);
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
            contactUs.LastModificationTime = Parameters.CurrentDateTime;
            contactUs.LastModifierUserId = Parameters.UserId;
            return Save(contactUs);
        }
        public SystemParameters_ContactUs Delete(SystemParameters_ContactUs postedContactUs)
        {
            SystemParameters_ContactUs contactUs = Get(postedContactUs.Id);
            if (_db.SystemParameters_ContactUs.Any(p => p.Id == postedContactUs.Id && p.IsDeleted != true))
            {
                //  ContactUs.OperationStatus = "HasRelationship";
                return contactUs;
            }

            contactUs.IsDeleted = true;
            contactUs.CreationTime = Parameters.CurrentDateTime;
            contactUs.CreatorUserId = Parameters.UserId;
            return Save(contactUs);
        }

    }
}
