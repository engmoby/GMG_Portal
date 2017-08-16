using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class AboutLogicTranslate
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public AboutLogicTranslate()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<SystemParameters_About_Translate> GetAllWithDeleted(string langId)
        {
            return _db.SystemParameters_About_Translate.OrderBy(p => p.IsDeleted && p.langId == langId).ToList();
        }
        public SystemParameters_About_Translate GetAll(string langId)
        {
            return _db.SystemParameters_About_Translate.FirstOrDefault(p => p.IsDeleted != true && p.langId == langId);
        }
        public SystemParameters_About_Translate Get(int id , string langId)
        {
            return _db.SystemParameters_About_Translate.FirstOrDefault(x => x.Id == id && x.langId == langId);
        }



        private SystemParameters_About_Translate Save(SystemParameters_About_Translate obj)
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
        public SystemParameters_About_Translate Insert(SystemParameters_About_Translate postedabout)
        {

            var About = new SystemParameters_About_Translate()
            {
                DisplayValue = postedabout.DisplayValue,
                DisplayValueDesc = postedabout.DisplayValueDesc,
                Image = postedabout.Image,
                IsDeleted = postedabout.IsDeleted,
                Show = Parameters.Show,
                Url = postedabout.Url,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
                langId = postedabout.langId
            };
            _db.SystemParameters_About_Translate.Add(About);
            return Save(About);
        }
        public SystemParameters_About_Translate Edit(SystemParameters_About_Translate postedAbout)
        {
            SystemParameters_About_Translate obj = Get(postedAbout.Id ,postedAbout.langId);
            obj.DisplayValue = postedAbout.DisplayValue;
            obj.DisplayValueDesc = postedAbout.DisplayValueDesc;
            obj.Image = postedAbout.Image;
            obj.Url = postedAbout.Url;
            obj.IsDeleted = postedAbout.IsDeleted;
            obj.Show = postedAbout.Show;
            obj.LastModificationTime = Parameters.CurrentDateTime;
            obj.LastModifierUserId = Parameters.UserId;
            return Save(obj);
        }
        public SystemParameters_About_Translate Delete(SystemParameters_About_Translate postedAbout)
        {
            SystemParameters_About_Translate obj = Get(postedAbout.Id,postedAbout.langId);
            if (_db.SystemParameters_About_Translate.Any(p => p.Id == postedAbout.Id && p.IsDeleted != true))
            {
                //  About.OperationStatus = "HasRelationship";
                return obj;
            }

            obj.IsDeleted = true;
            obj.CreationTime = Parameters.CurrentDateTime;
            obj.CreatorUserId = Parameters.UserId;
            return Save(obj);
        }
        public List<SystemParameters_About_Translate> GetAboutAll()
        {
            return _db.SystemParameters_About_Translate.Where(p => p.IsDeleted != true).ToList();
        }
        
    }
}