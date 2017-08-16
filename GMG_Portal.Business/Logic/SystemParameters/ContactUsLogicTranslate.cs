using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class ContactUsLogicTranslate
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public ContactUsLogicTranslate()
        {
            _db = new GMG_Portal_DBEntities1();
        }

        //Front And Back Fetch Logic 
        public List<SystemParameters_ContactUs_Translate> GetAllWithDeleted(string langId)
        {
            return _db.SystemParameters_ContactUs_Translate.OrderBy(p => p.IsDeleted && p.langId == langId).ToList();
        }
        public  SystemParameters_ContactUs_Translate GetAll(string langId)
        {
            return _db.SystemParameters_ContactUs_Translate.FirstOrDefault(p => p.IsDeleted != true && p.langId == langId);
        }


        //Crud Logic
        public SystemParameters_ContactUs_Translate Get(int id, string langId)
        {
            return _db.SystemParameters_ContactUs_Translate.FirstOrDefault(x => x.Id == id && x.langId == langId);
        }
        private SystemParameters_ContactUs_Translate Save(SystemParameters_ContactUs_Translate obj)
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
        public SystemParameters_ContactUs_Translate Insert(SystemParameters_ContactUs_Translate postedContactUs)
        {

            var obj = new SystemParameters_ContactUs_Translate()
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
                langId = postedContactUs.langId,
            };
            _db.SystemParameters_ContactUs_Translate.Add(obj);
            return Save(obj);
        }
        public SystemParameters_ContactUs_Translate Edit(SystemParameters_ContactUs_Translate postedContactUs)
        {
            SystemParameters_ContactUs_Translate obj = Get(postedContactUs.Id,postedContactUs.langId);
            obj.DisplayValueAddress = postedContactUs.DisplayValueAddress;
            obj.DisplayValueDesc = postedContactUs.DisplayValueDesc;
            obj.Image = postedContactUs.Image;
            obj.Url = postedContactUs.Url;
            obj.IsDeleted = postedContactUs.IsDeleted;
            obj.Show = postedContactUs.Show;
            obj.Facebook = postedContactUs.Facebook;
            obj.Fax = postedContactUs.Fax;
            obj.Twitter = postedContactUs.Twitter;
            obj.Youtube = postedContactUs.Youtube;
            obj.Instgram = postedContactUs.Instgram;
            obj.Snapchat = postedContactUs.Snapchat;
            obj.PhoneNo1 = postedContactUs.PhoneNo1;
            obj.PhoneNo2 = postedContactUs.PhoneNo2;
            obj.PostalCode = postedContactUs.PostalCode;
            obj.Late = postedContactUs.Late;
            obj.Long = postedContactUs.Long;
            obj.Mailbox = postedContactUs.Mailbox;
            obj.WhatsApp = postedContactUs.WhatsApp;
            obj.LastModificationTime = Parameters.CurrentDateTime;
            obj.LastModifierUserId = Parameters.UserId;
            return Save(obj);
        }
        public SystemParameters_ContactUs_Translate Delete(SystemParameters_ContactUs_Translate postedContactUs)
        {
            SystemParameters_ContactUs_Translate obj = Get(postedContactUs.Id,postedContactUs.langId);
            if (_db.SystemParameters_ContactUs_Translate.Any(p => p.Id == postedContactUs.Id && p.IsDeleted != true))
            {
                //  ContactUs.OperationStatus = "HasRelationship";
                return obj;
            }

            obj.IsDeleted = true;
            obj.CreationTime = Parameters.CurrentDateTime;
            obj.CreatorUserId = Parameters.UserId;
            return Save(obj);
        }

    }
}
