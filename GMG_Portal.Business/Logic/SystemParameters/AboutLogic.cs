using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class AboutLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public AboutLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<SystemParameters_About> GetAllWithDeleted()
        {
            return _db.SystemParameters_About.OrderBy(p => p.IsDeleted).ToList();
        }
        public SystemParameters_About GetAll()
        {
            return _db.SystemParameters_About.FirstOrDefault(p => p.IsDeleted != true);
        }
        public SystemParameters_About Get(int id)
        {
            return _db.SystemParameters_About.Find(id);
        }
        private SystemParameters_About Save(SystemParameters_About about)
        {
            try
            {
                _db.SaveChanges();
                about.OperationStatus = "Succeded";
                return about;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        about.OperationStatus = "NameArMustBeUnique";
                        return about;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        about.OperationStatus = "NameEnMustBeUnique";
                        return about;
                    }
                }
                throw;
            }
        }
        public SystemParameters_About Insert(SystemParameters_About postedabout)
        {

            var About = new SystemParameters_About()
            {
                DisplayValue = postedabout.DisplayValue,
                DisplayValueDesc = postedabout.DisplayValueDesc,
                Image= postedabout.Image,
                IsDeleted = postedabout.IsDeleted,
                Show = Parameters.Show, 
                Url = postedabout.Url,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId, 
            };
            _db.SystemParameters_About.Add(About);
            return Save(About);
        }
        public SystemParameters_About Edit(SystemParameters_About postedAbout)
        {
            SystemParameters_About about = Get(postedAbout.Id);
            about.DisplayValue = postedAbout.DisplayValue;
            about.DisplayValueDesc = postedAbout.DisplayValueDesc;
            about.Image = postedAbout.Image;
            about.Url= postedAbout.Url; 
            about.IsDeleted = postedAbout.IsDeleted;
            about.Show = postedAbout.Show; 
            about.LastModificationTime = Parameters.CurrentDateTime;
            about.LastModifierUserId = Parameters.UserId;
            return Save(about);
        }
        public SystemParameters_About Delete(SystemParameters_About postedAbout)
        {
            SystemParameters_About about = Get(postedAbout.Id);
            if (_db.SystemParameters_About.Any(p => p.Id == postedAbout.Id && p.IsDeleted != true))
            {
                  //  About.OperationStatus = "HasRelationship";
                return about;
            }

            about.IsDeleted = true;
            about.CreationTime = Parameters.CurrentDateTime;
            about.CreatorUserId = Parameters.UserId;
            return Save(about);
        }
        public List<SystemParameters_About> GetAboutAll()
        {
            return _db.SystemParameters_About.Where(p => p.IsDeleted != true).ToList();
        }
    }
}
