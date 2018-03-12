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
        public List<About> GetAllWithDeleted()
        {
            return _db.Abouts.OrderBy(p => p.IsDeleted).ToList();
        }
        public About GetAll()
        {
            return _db.Abouts.FirstOrDefault(p => p.IsDeleted != true);
        }
        public About Get(int id)
        {
            return _db.Abouts.Find(id);
        }
        private About Save(About about)
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
        public List<About_Translate> GetTranslates(int recordId)
        {
            return _db.About_Translate.Where(x => x.RecordId == recordId).ToList();
        }
        public About Insert(About postedabout)
        {
            var obj = new About()
            {
                Image = postedabout.Image,
                IsDeleted = postedabout.IsDeleted,
                Url = postedabout.Url,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.Abouts.Add(obj);
            _db.SaveChanges();
            var objTrasnlate = new About_Translate();
            {
                foreach (var title in postedabout.AboutTitleDictionary)
                {
                    objTrasnlate.AboutTitle = title.Value;
                    objTrasnlate.AboutDescription = postedabout.AboutDescDictionary[title.Key];

                    objTrasnlate.VisionTitle = postedabout.VisionTitleDictionary[title.Key];
                    objTrasnlate.VisionDescription = postedabout.VisionDescDictionary[title.Key];

                    objTrasnlate.MissionTitle = postedabout.MissionTitleDictionary[title.Key];
                    objTrasnlate.MissionDescription = postedabout.MissionDescDictionary[title.Key];

                    objTrasnlate.CoreValueTitle = postedabout.CoreValueTitleDictionary[title.Key];
                    objTrasnlate.CoreValueDescription = postedabout.CoreValueDescDictionary[title.Key];

                    objTrasnlate.langId = title.Key;
                    objTrasnlate.RecordId = obj.Id;
                    _db.About_Translate.Add(objTrasnlate);
                    _db.SaveChanges();
                }
            }
            About about = Get(postedabout.Id);
            List<About_Translate> translate = GetTranslates(postedabout.Id);

            return Save(about);
        }
        public About Edit(About postedAbout)
        {
            About about = Get(postedAbout.Id);
            List<About_Translate> translate = GetTranslates(postedAbout.Id);
            foreach (var title in postedAbout.AboutTitleDictionary)
            {
                foreach (var objTrasnlate in translate)
                {
                    if (title.Key == objTrasnlate.langId)
                    {

                        objTrasnlate.AboutTitle = title.Value;
                        objTrasnlate.AboutDescription = postedAbout.AboutDescDictionary[title.Key];

                        objTrasnlate.VisionTitle = postedAbout.VisionTitleDictionary[title.Key];
                        objTrasnlate.VisionDescription = postedAbout.VisionDescDictionary[title.Key];

                        objTrasnlate.MissionTitle = postedAbout.MissionTitleDictionary[title.Key];
                        objTrasnlate.MissionDescription = postedAbout.MissionDescDictionary[title.Key];

                        objTrasnlate.CoreValueTitle = postedAbout.CoreValueTitleDictionary[title.Key];
                        objTrasnlate.CoreValueDescription = postedAbout.CoreValueDescDictionary[title.Key];


                        _db.SaveChanges();
                    }
                }
            }
            about.Image = postedAbout.Image;
            about.Url = postedAbout.Url;
            about.IsDeleted = postedAbout.IsDeleted;
            about.LastModificationTime = Parameters.CurrentDateTime;
            about.LastModifierUserId = Parameters.UserId;
            return Save(about);
        }
        public About Delete(About postedAbout)
        {
            About about = Get(postedAbout.Id);
            //if (_db.Abouts.Any(p => p.Id == postedAbout.Id && p.IsDeleted != true))
            //{
            //    //  About.OperationStatus = "HasRelationship";
            //    return about;
            //}

            about.IsDeleted = true;
            about.CreationTime = Parameters.CurrentDateTime;
            about.CreatorUserId = Parameters.UserId;
            return Save(about);
        }
        public List<About> GetAboutAll()
        {
            return _db.Abouts.Where(p => p.IsDeleted != true).ToList();
        }
    }
}
