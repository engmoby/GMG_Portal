using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class ContactFormLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public ContactFormLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<SystemParameters_ContactForm> GetAllWithSeen()
        {
            return _db.SystemParameters_ContactForm.OrderBy(p => p.SeenBy).ToList();
        }
        public List<SystemParameters_ContactForm> GetAll()
        {
            return _db.SystemParameters_ContactForm.Where(p => p.Seen != true).ToList();
        }
        public SystemParameters_ContactForm Get(int id)
        {
            return _db.SystemParameters_ContactForm.Find(id);
        }
        private SystemParameters_ContactForm Save(SystemParameters_ContactForm contactForm)
        {
            try
            {
                _db.SaveChanges();
                contactForm.OperationStatus = "Succeded";
                return contactForm;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        contactForm.OperationStatus = "NameArMustBeUnique";
                        return contactForm;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        contactForm.OperationStatus = "NameEnMustBeUnique";
                        return contactForm;
                    }
                }
                throw;
            }
        }
        public SystemParameters_ContactForm Insert(SystemParameters_ContactForm postedContactForm)
        {

            var contactForm = new SystemParameters_ContactForm()
            {
                FirstName = postedContactForm.FirstName,
                LastName = postedContactForm.LastName,
                Email = postedContactForm.Email,
                PhoneNo = postedContactForm.PhoneNo,
                Message = postedContactForm.Message,
                CreationTime = Parameters.CurrentDateTime
            };
            _db.SystemParameters_ContactForm.Add(contactForm);
            return Save(contactForm);
        }
        public SystemParameters_ContactForm Edit(SystemParameters_ContactForm postedContactForm)
        {
            SystemParameters_ContactForm contactForm = Get(postedContactForm.Id);
            contactForm.Seen = postedContactForm.Seen;
            contactForm.SeenDate = Parameters.CurrentDateTime;
            contactForm.SeenBy = Parameters.UserId;
            return Save(contactForm);
        }


    }
}
