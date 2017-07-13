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
            var returnList = new List<SystemParameters_News>();

            var list = _db.SystemParameters_News.Where(p => p.IsDeleted != true).ToList();
            foreach (var systemParametersNewse in list)
            {
                var getCatrogryInfo = _db.SystemParameters_Category.FirstOrDefault(p => p.IsDeleted != true && p.Id == systemParametersNewse.CategoryId);
                if (systemParametersNewse.CreationTime != null)
                    if (getCatrogryInfo != null)
                        returnList.Add(new SystemParameters_News
                        {
                            Id = systemParametersNewse.Id,
                            DisplayValue = systemParametersNewse.DisplayValue,
                            DisplayValueDesc = systemParametersNewse.DisplayValueDesc,

                            CreationTime = systemParametersNewse.CreationTime,
                            CreationDay = systemParametersNewse.CreationTime.Value.Day,
                            CreationMonth = systemParametersNewse.CreationTime.Value.Month,
                            CreatorUserName = "Administrator",
                            CategoryName = getCatrogryInfo.DisplayValue,
                            Tags = systemParametersNewse.Tags,
                            Image = systemParametersNewse.Image,
                            Categories = GetAllCatrogry()
                        });
            }

            return returnList;
        }

        private List<SystemParameters_Category> GetAllCatrogry()
        {
            var getAllCatrogry = _db.SystemParameters_Category.Where(p => p.IsDeleted != true).ToList();
            return getAllCatrogry;
        }

        public List<SystemParameters_News> GetAllByCatrgoryId(int categoryId)
        {
            var returnList = new List<SystemParameters_News>();

            var list = _db.SystemParameters_News.Where(p => p.IsDeleted != true && p.CategoryId == categoryId).ToList();
            foreach (var systemParametersNewse in list)
            {
                var getCatrogryInfo = _db.SystemParameters_Category.FirstOrDefault(p => p.IsDeleted != true && p.Id == systemParametersNewse.CategoryId);
                if (getCatrogryInfo != null)
                    if (systemParametersNewse.CreationTime != null)
                        returnList.Add(new SystemParameters_News
                        {
                            Id = systemParametersNewse.Id,
                            DisplayValue = systemParametersNewse.DisplayValue,
                            DisplayValueDesc = systemParametersNewse.DisplayValueDesc,
                            CreationTime = systemParametersNewse.CreationTime,
                            CreationDay = systemParametersNewse.CreationTime.Value.Day,
                            CreationMonth = systemParametersNewse.CreationTime.Value.Month,
                            CreatorUserName = "Administrator",
                            CategoryName = getCatrogryInfo.DisplayValue,
                            CategoryId = getCatrogryInfo.Id,
                            Tags = systemParametersNewse.Tags,
                            Image = systemParametersNewse.Image,
                            Categories = GetAllCatrogry()
                        });
            }

            return returnList;
        }
        public SystemParameters_News Get(int id)
        {
            var returnObj = new SystemParameters_News();

            var obj = _db.SystemParameters_News.Find(id);

            var getCatrogryInfo = _db.SystemParameters_Category.FirstOrDefault(p => p.IsDeleted != true && p.Id == obj.CategoryId);
            if (getCatrogryInfo != null)

                if (obj != null)
                {
                    returnObj.Id = obj.Id; returnObj.DisplayValue = obj.DisplayValue;
                    returnObj.DisplayValueDesc = obj.DisplayValueDesc;
                    if (obj.CreationTime != null) returnObj.CreationDay = obj.CreationTime.Value.Day;
                    if (obj.CreationTime != null) returnObj.CreationMonth = obj.CreationTime.Value.Month;
                    returnObj.CreationTime = obj.CreationTime;
                    returnObj.CreatorUserName = "Administrator";
                    returnObj.CategoryName = getCatrogryInfo.DisplayValue;
                    returnObj.CategoryId = getCatrogryInfo.Id;
                    returnObj.Tags = obj.Tags;
                    returnObj.Image = obj.Image;
                }


            return returnObj;

        }
        public List<SystemParameters_News> SearchNews(string keyword)
        {
            return _db.SystemParameters_News.Where(x => x.DisplayValue.Contains(keyword) || x.DisplayValueDesc.Contains(keyword)).ToList();
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
