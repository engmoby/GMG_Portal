using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class NewsletterLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public NewsletterLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<SystemParameters_Newsletter> GetAllWithSeen()
        {
            return _db.SystemParameters_Newsletter.OrderByDescending(p => p.CreationTime).ToList();
        }
        public List<SystemParameters_Newsletter> GetAll()
        {
            return _db.SystemParameters_Newsletter.Where(p => p.Seen != true).ToList();
        }
        public SystemParameters_Newsletter Get(string mail)
        {
            return _db.SystemParameters_Newsletter.FirstOrDefault(x => x.Mail==mail);
        }
        private SystemParameters_Newsletter Save(SystemParameters_Newsletter newsletter)
        {
            try
            {
                _db.SaveChanges();
                newsletter.OperationStatus = "Succeded";
                return newsletter;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        newsletter.OperationStatus = "NameArMustBeUnique";
                        return newsletter;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        newsletter.OperationStatus = "NameEnMustBeUnique";
                        return newsletter;
                    }
                }
                throw;
            }
        }
        public SystemParameters_Newsletter Insert(SystemParameters_Newsletter postedNewsletter)
        {
            SystemParameters_Newsletter checkNewsletter= Get(postedNewsletter.Mail);
            if (checkNewsletter != null)
            {
                checkNewsletter.OperationStatus = "AlreadyExist";
                return checkNewsletter;
            }
            var newsletter = new SystemParameters_Newsletter()
            {
                Mail = postedNewsletter.Mail,
                CreationTime = Parameters.CurrentDateTime
            };
            _db.SystemParameters_Newsletter.Add(newsletter);
            return Save(newsletter);
        }
        public SystemParameters_Newsletter Edit(SystemParameters_Newsletter postedNewsletter)
        {
            SystemParameters_Newsletter newsletter = Get(postedNewsletter.Mail);
            newsletter.SeenDate = Parameters.CurrentDateTime;
            newsletter.SeenBy = Parameters.UserId; 
            newsletter.Seen = postedNewsletter.Seen;
            return Save(newsletter);
        }


     

    }
}
