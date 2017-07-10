using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class NewsLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public NewsLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<SystemParameters_News> GetAllWithDeleted()
        {
            return _db.SystemParameters_News.OrderBy(p => p.IsDeleted).ToList();
        }
        public List<SystemParameters_News> GetAll()
        {
            return _db.SystemParameters_News.Where(p => p.IsDeleted != true).ToList();
        }
        public SystemParameters_News Get(int id)
        {
            return _db.SystemParameters_News.Find(id);
        }
        private SystemParameters_News Save(SystemParameters_News News)
        {
            try
            {
                _db.SaveChanges();
                News.OperationStatus = "Succeded";
                return News;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        News.OperationStatus = "NameArMustBeUnique";
                        return News;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        News.OperationStatus = "NameEnMustBeUnique";
                        return News;
                    }
                }
                throw;
            }
        }
        public SystemParameters_News Insert(SystemParameters_News postedNews)
        {

            var news = new SystemParameters_News()
            {
                DisplayValue = postedNews.DisplayValue,
                DisplayValueDesc = postedNews.DisplayValueDesc,
                Image = postedNews.Image,
                IsDeleted = postedNews.IsDeleted,
                Show = Parameters.Show,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.SystemParameters_News.Add(news);
            return Save(news);
        }
        public SystemParameters_News Edit(SystemParameters_News postednews)
        {
            SystemParameters_News news = Get(postednews.Id);
            news.DisplayValue = postednews.DisplayValue;
            news.DisplayValueDesc = postednews.DisplayValueDesc;
            news.Image = postednews.Image;
            news.IsDeleted = postednews.IsDeleted;
            news.Show = postednews.Show;
            news.LastModificationTime = Parameters.CurrentDateTime;
            news.LastModifierUserId = Parameters.UserId;
            return Save(news);
        }
        public SystemParameters_News Delete(SystemParameters_News postednews)
        {
            SystemParameters_News news = Get(postednews.Id);
            if (_db.SystemParameters_News.Any(p => p.Id == postednews.Id && p.IsDeleted != true))
            {
                //  news.OperationStatus = "HasRelationship";
                return news;
            }

            news.IsDeleted = true;
            news.CreationTime = Parameters.CurrentDateTime;
            news.CreatorUserId = Parameters.UserId;
            return Save(news);
        }

    }
}
