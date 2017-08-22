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
        public List<SystemParameters_News> GetAllWithDeleted(string langId)
        {
            var returnValue = new List<SystemParameters_News>();
            if (langId == Parameters.DefaultLang)
            {
                var list = _db.SystemParameters_News.OrderByDescending(n => n.Id).ToList();
                foreach (var systemParametersNewse in list)
                {
                    var getCatrogryInfo = _db.SystemParameters_Category.FirstOrDefault(p => p.IsDeleted != true && p.Id == systemParametersNewse.CategoryId);
                    if (getCatrogryInfo != null)
                        returnValue.Add(new SystemParameters_News
                        {
                            Id = systemParametersNewse.Id,
                            DisplayValue = systemParametersNewse.DisplayValue,
                            DisplayValueDesc = systemParametersNewse.DisplayValueDesc,
                            CreationTime = systemParametersNewse.CreationTime,
                            CreationDay = systemParametersNewse.CreationTime.Day,
                            CreationMonth = systemParametersNewse.CreationTime.ToString("MMM"),
                            CreatorUserName = "Administrator",
                            CategoryName = getCatrogryInfo.DisplayValue,
                            CategoryId = getCatrogryInfo.Id,
                            Tags = systemParametersNewse.Tags,
                            Image = systemParametersNewse.Image,
                            IsDeleted = systemParametersNewse.IsDeleted,
                            Categories = GetAllCatrogry()
                        });
                }
            }
            else
            {
                var list = _db.SystemParameters_News_Translate.Where(p => p.langId == langId).OrderByDescending(n => n.Id).ToList();
                foreach (var systemParametersNewse in list)
                {
                    var getCatrogryInfo = _db.SystemParameters_Category_Translate.FirstOrDefault(p => p.IsDeleted != true && p.Id == systemParametersNewse.CategoryId && p.langId == langId);
                    if (getCatrogryInfo != null)
                        returnValue.Add(new SystemParameters_News
                        {
                            Id = systemParametersNewse.Id,
                            DisplayValue = systemParametersNewse.DisplayValue,
                            DisplayValueDesc = systemParametersNewse.DisplayValueDesc,
                            CreationTime = systemParametersNewse.CreationTime,
                            CreationDay = systemParametersNewse.CreationTime.Day,
                            CreationMonth = systemParametersNewse.CreationTime.ToString("MMM"),
                            CreatorUserName = "Administrator",
                            CategoryName = getCatrogryInfo.DisplayValue,
                            CategoryId = getCatrogryInfo.Id,
                            Tags = systemParametersNewse.Tags,
                            Image = systemParametersNewse.Image,
                            IsDeleted = systemParametersNewse.IsDeleted,
                            Categories = GetAllCatrogryByLang(langId)
                        });
                }
            }
            return returnValue;
        }
        public List<SystemParameters_News> GetAll(string langId)
        {
            var returnValue = new List<SystemParameters_News>();
            if (langId == Parameters.DefaultLang)
            {
                var list = _db.SystemParameters_News.Where(p => p.IsDeleted != true).OrderByDescending(n => n.Id).ToList();
                foreach (var systemParametersNewse in list)
                {
                    var getCatrogryInfo = _db.SystemParameters_Category.FirstOrDefault(p => p.IsDeleted != true && p.Id == systemParametersNewse.CategoryId);
                    if (getCatrogryInfo != null)
                        returnValue.Add(new SystemParameters_News
                        {
                            Id = systemParametersNewse.Id,
                            DisplayValue = systemParametersNewse.DisplayValue,
                            DisplayValueDesc = systemParametersNewse.DisplayValueDesc,
                            CreationTime = systemParametersNewse.CreationTime,
                            CreationDay = systemParametersNewse.CreationTime.Day,
                            CreationMonth = systemParametersNewse.CreationTime.ToString("MMM"),
                            CreatorUserName = "Administrator",
                            CategoryName = getCatrogryInfo.DisplayValue,
                            CategoryId = getCatrogryInfo.Id,
                            Tags = systemParametersNewse.Tags,
                            Image = systemParametersNewse.Image,
                            Categories = GetAllCatrogry()
                        });
                }
            }
            else
            {
                var list = _db.SystemParameters_News_Translate.Where(p => p.IsDeleted != true && p.langId == langId).OrderByDescending(n => n.Id).ToList();
                foreach (var systemParametersNewse in list)
                {
                    var getCatrogryInfo = _db.SystemParameters_Category_Translate.FirstOrDefault(p => p.IsDeleted != true && p.Id == systemParametersNewse.CategoryId && p.langId == langId);
                    if (getCatrogryInfo != null)
                        returnValue.Add(new SystemParameters_News
                        {
                            Id = systemParametersNewse.Id,
                            DisplayValue = systemParametersNewse.DisplayValue,
                            DisplayValueDesc = systemParametersNewse.DisplayValueDesc,
                            CreationTime = systemParametersNewse.CreationTime,
                            CreationDay = systemParametersNewse.CreationTime.Day,
                            //  CreationMonth = systemParametersNewse.CreationTime.Value.Month,
                            CreationMonth = systemParametersNewse.CreationTime.ToString("MMM"),
                            CreatorUserName = "Administrator",
                            CategoryName = getCatrogryInfo.DisplayValue,
                            CategoryId = getCatrogryInfo.Id,
                            Tags = systemParametersNewse.Tags,
                            Image = systemParametersNewse.Image,
                            Categories = GetAllCatrogryByLang(langId)
                        });
                }
            }
            return returnValue;
        }
        public List<SystemParameters_News> GetAllWithCount(string langId)
        {
            var returnList = new List<SystemParameters_News>();
            if (langId == Parameters.DefaultLang)
            {
                var list = _db.SystemParameters_News.Where(p => p.IsDeleted != true).OrderByDescending(n => n.Id)
                    .ToList().Take(3);
                foreach (var systemParametersNewse in list)
                {
                    var getCatrogryInfo =
                        _db.SystemParameters_Category.FirstOrDefault(
                            p => p.IsDeleted != true && p.Id == systemParametersNewse.CategoryId);
                    if (getCatrogryInfo != null)
                        returnList.Add(new SystemParameters_News
                        {
                            Id = systemParametersNewse.Id,
                            DisplayValue = systemParametersNewse.DisplayValue,
                            DisplayValueDesc = systemParametersNewse.DisplayValueDesc,
                            CreationTime = systemParametersNewse.CreationTime,
                            CreationDay = systemParametersNewse.CreationTime.Day,
                            CreationMonth = systemParametersNewse.CreationTime.ToString("MMM"),
                            Tags = systemParametersNewse.Tags,
                            Categories = GetAllCatrogry()
                        });
                }
            }
            else
            {
                var list = _db.SystemParameters_News_Translate.Where(p => p.IsDeleted != true && p.langId == langId).OrderByDescending(n => n.Id)
                    .ToList().Take(3);
                foreach (var systemParametersNewse in list)
                {
                    var getCatrogryInfo =
                        _db.SystemParameters_Category_Translate.FirstOrDefault(p => p.IsDeleted != true && p.Id == systemParametersNewse.CategoryId && p.langId == langId);
                    if (getCatrogryInfo != null)
                        returnList.Add(new SystemParameters_News
                        {
                            Id = systemParametersNewse.Id,
                            DisplayValue = systemParametersNewse.DisplayValue,
                            DisplayValueDesc = systemParametersNewse.DisplayValueDesc,
                            CreationTime = systemParametersNewse.CreationTime,
                            CreationDay = systemParametersNewse.CreationTime.Day,
                            CreationMonth = systemParametersNewse.CreationTime.ToString("MMM"),

                            Tags = systemParametersNewse.Tags,
                            Categories = GetAllCatrogryByLang(langId)
                        });
                }
            }
            return returnList;
        }
        public List<SystemParameters_News> GetLatestNewsWithOutCurrentId(int newsId, string langId)
        {
            var returnList = new List<SystemParameters_News>();
            if (langId == Parameters.DefaultLang)
            {
                var list = _db.SystemParameters_News.Where(p => p.IsDeleted != true && p.Id != newsId).OrderByDescending(n => n.Id)
                    .ToList().Take(3);
                foreach (var systemParametersNewse in list)
                {
                    var getCatrogryInfo =
                        _db.SystemParameters_Category.FirstOrDefault(
                            p => p.IsDeleted != true && p.Id == systemParametersNewse.CategoryId);
                    if (getCatrogryInfo != null)
                        returnList.Add(new SystemParameters_News
                        {
                            Id = systemParametersNewse.Id,
                            DisplayValue = systemParametersNewse.DisplayValue,
                            DisplayValueDesc = systemParametersNewse.DisplayValueDesc,
                            CreationTime = systemParametersNewse.CreationTime,
                            CreationDay = systemParametersNewse.CreationTime.Day,
                            CreationMonth = systemParametersNewse.CreationTime.ToString("MMM"),

                            Tags = systemParametersNewse.Tags,
                            Categories = GetAllCatrogry()
                        });
                }
            }
            else
            {
                var list = _db.SystemParameters_News_Translate.Where(p => p.IsDeleted != true && p.langId == langId).OrderByDescending(n => n.Id)
                    .ToList().Take(3);
                foreach (var systemParametersNewse in list)
                {
                    var getCatrogryInfo =
                        _db.SystemParameters_Category_Translate.FirstOrDefault(p => p.IsDeleted != true && p.Id == systemParametersNewse.CategoryId && p.langId == langId);
                    if (systemParametersNewse.CreationTime != null)
                        if (getCatrogryInfo != null)
                            returnList.Add(new SystemParameters_News
                            {
                                Id = systemParametersNewse.Id,
                                DisplayValue = systemParametersNewse.DisplayValue,
                                DisplayValueDesc = systemParametersNewse.DisplayValueDesc,
                                CreationTime = systemParametersNewse.CreationTime,
                                CreationDay = systemParametersNewse.CreationTime.Day,
                                CreationMonth = systemParametersNewse.CreationTime.ToString("MMM"),

                                Tags = systemParametersNewse.Tags,
                                Categories = GetAllCatrogryByLang(langId)
                            });
                }
            }
            return returnList;
        }

        public List<SystemParameters_Category> GetAllCatrogry()
        {
            return _db.SystemParameters_Category.Where(p => p.IsDeleted != true).OrderByDescending(n => n.Id).ToList();
        }

        public List<SystemParameters_Category_Translate> GetAllCatrogrybyLangId(string langId)
        {
            return _db.SystemParameters_Category_Translate.Where(p => p.IsDeleted != true && p.langId == langId).OrderByDescending(n => n.Id).ToList();
        }

        public List<SystemParameters_Category> GetAllCatrogryByLang(string langId)
        {
            var returnValue = new List<SystemParameters_Category>();
            var list = _db.SystemParameters_Category_Translate.Where(p => p.IsDeleted != true && p.langId == langId).OrderByDescending(n => n.Id).ToList();
            foreach (var systemParametersCategoryTranslate in list)
            {
                returnValue.Add(new SystemParameters_Category
                {
                    Id = systemParametersCategoryTranslate.Id,
                    DisplayValue = systemParametersCategoryTranslate.DisplayValue,
                    DisplayValueDesc = systemParametersCategoryTranslate.DisplayValueDesc
                });
            }
            return returnValue;
        }
        public List<SystemParameters_News> GetAllByCatrgoryId(int categoryId, string langId)
        {
            var returnValue = new List<SystemParameters_News>();
            if (langId == Parameters.DefaultLang)
            {
                var list = _db.SystemParameters_News.Where(p => p.IsDeleted != true && p.CategoryId == categoryId).OrderByDescending(n => n.Id)
                    .ToList();
                foreach (var systemParametersNewse in list)
                {
                    var getCatrogryInfo =
                        _db.SystemParameters_Category.FirstOrDefault(
                            p => p.IsDeleted != true && p.Id == systemParametersNewse.CategoryId);
                    if (getCatrogryInfo != null)
                        returnValue.Add(new SystemParameters_News
                        {
                            Id = systemParametersNewse.Id,
                            DisplayValue = systemParametersNewse.DisplayValue,
                            DisplayValueDesc = systemParametersNewse.DisplayValueDesc,
                            CreationTime = systemParametersNewse.CreationTime,
                            CreationDay = systemParametersNewse.CreationTime.Day,
                            CreationMonth = systemParametersNewse.CreationTime.ToString("MMM"),

                            CreatorUserName = "Administrator",
                            CategoryName = getCatrogryInfo.DisplayValue,
                            CategoryId = getCatrogryInfo.Id,
                            Tags = systemParametersNewse.Tags,
                            Image = systemParametersNewse.Image,
                            Categories = GetAllCatrogry()
                        });
                }
            }
            else
            {
                var list = _db.SystemParameters_News_Translate.Where(p => p.IsDeleted != true && p.CategoryId == categoryId && p.langId == langId).OrderByDescending(n => n.Id).ToList();
                foreach (var systemParametersNewse in list)
                {
                    var getCatrogryInfo = _db.SystemParameters_Category_Translate.FirstOrDefault(p => p.IsDeleted != true && p.Id == systemParametersNewse.CategoryId && p.langId == langId);
                    if (getCatrogryInfo != null)
                        returnValue.Add(new SystemParameters_News
                        {
                            Id = systemParametersNewse.Id,
                            DisplayValue = systemParametersNewse.DisplayValue,
                            DisplayValueDesc = systemParametersNewse.DisplayValueDesc,
                            CreationTime = systemParametersNewse.CreationTime,
                            CreationDay = systemParametersNewse.CreationTime.Day,
                            CreationMonth = systemParametersNewse.CreationTime.ToString("MMM"),

                            CreatorUserName = "Administrator",
                            CategoryName = getCatrogryInfo.DisplayValue,
                            CategoryId = getCatrogryInfo.Id,
                            Tags = systemParametersNewse.Tags,
                            Image = systemParametersNewse.Image,
                            Categories = GetAllCatrogryByLang(langId)
                        });
                }
            }

            return returnValue;
        }
        public SystemParameters_News Get(int id, string langId)
        {
            var returnValue = new SystemParameters_News();
            if (langId == Parameters.DefaultLang)
            {
                var obj = _db.SystemParameters_News.Find(id);

                var getCatrogryInfo = _db.SystemParameters_Category.FirstOrDefault(p => p.IsDeleted != true && p.Id == obj.CategoryId);
                if (getCatrogryInfo != null)

                    if (obj != null)
                    {

                        returnValue.Id = obj.Id; returnValue.DisplayValue = obj.DisplayValue;
                        returnValue.DisplayValueDesc = obj.DisplayValueDesc;
                        returnValue.CreationDay = obj.CreationTime.Day;
                        returnValue.CreationMonth = obj.CreationTime.ToString("MMM");
                        returnValue.CreationTime = obj.CreationTime;
                        returnValue.CreatorUserName = "Administrator";
                        returnValue.CategoryName = getCatrogryInfo.DisplayValue;
                        returnValue.CategoryId = getCatrogryInfo.Id;
                        returnValue.Tags = obj.Tags;
                        returnValue.Image = obj.Image;
                    }
            }
            else
            {
                var obj = _db.SystemParameters_News_Translate.FirstOrDefault(p => p.Id == id && p.langId == langId);

                var getCatrogryInfo = _db.SystemParameters_Category_Translate.FirstOrDefault(p => p.IsDeleted != true && p.Id == obj.CategoryId && p.langId == langId);
                if (getCatrogryInfo != null)

                    if (obj != null)
                    {
                        returnValue.Id = obj.Id; returnValue.DisplayValue = obj.DisplayValue;
                        returnValue.DisplayValueDesc = obj.DisplayValueDesc;
                        returnValue.CreationDay = obj.CreationTime.Day;
                        returnValue.CreationMonth = obj.CreationTime.ToString("MMM");
                        returnValue.CreationTime = obj.CreationTime;
                        returnValue.CreatorUserName = "Administrator";
                        returnValue.CategoryName = getCatrogryInfo.DisplayValue;
                        returnValue.CategoryId = getCatrogryInfo.Id;
                        returnValue.Tags = obj.Tags;
                        returnValue.Image = obj.Image;
                    }
            }

            return returnValue;

        }

        public SystemParameters_News GetNewsInfoById(int id)
        {
            var returnValue = new SystemParameters_News();

            var obj = _db.SystemParameters_News.Find(id);
            var getCatrogryInfo = _db.SystemParameters_Category.FirstOrDefault(p => p.IsDeleted != true && p.Id == obj.CategoryId);
            if (getCatrogryInfo != null)

                if (obj != null)
                {
                    returnValue.Id = obj.Id; returnValue.DisplayValue = obj.DisplayValue;
                    returnValue.DisplayValueDesc = obj.DisplayValueDesc;
                    returnValue.CreationDay = obj.CreationTime.Day;
                    returnValue.CreationMonth = obj.CreationTime.ToString("MMM");
                    returnValue.CreationTime = obj.CreationTime;
                    returnValue.CreatorUserName = "Administrator";
                    returnValue.CategoryName = getCatrogryInfo.DisplayValue;
                    returnValue.CategoryId = getCatrogryInfo.Id;
                    returnValue.Tags = obj.Tags;
                    returnValue.Image = obj.Image;
                }

            return returnValue;
        }

        public SystemParameters_News_Translate GetNewsInfoByLangById(int id, string langId)
        {
            var returnValue = new SystemParameters_News_Translate();

            var obj = _db.SystemParameters_News_Translate.FirstOrDefault(p => p.Id == id && p.langId == langId);

            var getCatrogryInfo = _db.SystemParameters_Category_Translate.FirstOrDefault(p => p.IsDeleted != true && p.Id == obj.CategoryId && p.langId == langId);
            if (getCatrogryInfo != null)

                if (obj != null)
                {
                    returnValue.Id = obj.Id; returnValue.DisplayValue = obj.DisplayValue;
                    returnValue.DisplayValueDesc = obj.DisplayValueDesc;
                    returnValue.CreationDay = obj.CreationTime.Day;
                    returnValue.CreationMonth = obj.CreationTime.ToString("MMM");
                    returnValue.CreationTime = obj.CreationTime;
                    returnValue.CreatorUserName = "Administrator";
                    returnValue.CategoryName = getCatrogryInfo.DisplayValue;
                    returnValue.CategoryId = getCatrogryInfo.Id;
                    returnValue.Tags = obj.Tags;
                    returnValue.Image = obj.Image;
                }


            return returnValue;
        }
        public List<SystemParameters_News> SearchNews(string keyword)
        {
           
                return _db.SystemParameters_News
                    .Where(x => x.DisplayValue.Contains(keyword) || x.DisplayValueDesc.Contains(keyword)).ToList();

            }

        public List<SystemParameters_News_Translate> SearchNewsTranslate(string keyword, string langId)
        {

            return _db.SystemParameters_News_Translate
                .Where(x => x.DisplayValue.Contains(keyword) || x.DisplayValueDesc.Contains(keyword) || x.langId.Equals(langId)).ToList();

        }
         
        private SystemParameters_News Save(SystemParameters_News news)
        {
            try
            {
                _db.SaveChanges();
                news.OperationStatus = "Succeded";
                return news;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        news.OperationStatus = "NameArMustBeUnique";
                        return news;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        news.OperationStatus = "NameEnMustBeUnique";
                        return news;
                    }
                }
                throw;
            }
        }
        private SystemParameters_News_Translate SaveByLang(SystemParameters_News_Translate news)
        {
            try
            {
                _db.SaveChanges();
                news.OperationStatus = "Succeded";
                return news;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        news.OperationStatus = "NameArMustBeUnique";
                        return news;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        news.OperationStatus = "NameEnMustBeUnique";
                        return news;
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
                CategoryId = postedNews.CategoryId,
                Show = Parameters.Show,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.SystemParameters_News.Add(news);
            return Save(news);
        }
        public SystemParameters_News_Translate InsertByLang(SystemParameters_News_Translate postedNews)
        {
            var news = new SystemParameters_News_Translate()
            {
                DisplayValue = postedNews.DisplayValue,
                DisplayValueDesc = postedNews.DisplayValueDesc,
                Image = postedNews.Image,
                IsDeleted = postedNews.IsDeleted,
                CategoryId = postedNews.CategoryId,
                Show = Parameters.Show,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
                langId = postedNews.langId
            };
            _db.SystemParameters_News_Translate.Add(news);
            return SaveByLang(news);
        }
        public SystemParameters_News Edit(SystemParameters_News postednews)
        {
            var news = GetById(postednews);
            news.DisplayValue = postednews.DisplayValue;
            news.DisplayValueDesc = postednews.DisplayValueDesc;
            news.Image = postednews.Image;
            news.IsDeleted = postednews.IsDeleted;
            news.Show = postednews.Show;
            news.LastModificationTime = Parameters.CurrentDateTime;
            news.LastModifierUserId = Parameters.UserId;
            return Save(news);
        }
        public SystemParameters_News_Translate EditByLang(SystemParameters_News_Translate postednews)
        {
            SystemParameters_News_Translate news = GetNewsInfoByLangById(postednews.Id, postednews.langId);
            news.DisplayValue = postednews.DisplayValue;
            news.DisplayValueDesc = postednews.DisplayValueDesc;
            news.Image = postednews.Image;
            news.IsDeleted = postednews.IsDeleted;
            news.Show = postednews.Show;
            news.LastModificationTime = Parameters.CurrentDateTime;
            news.LastModifierUserId = Parameters.UserId;
            return SaveByLang(news);
        }
        public SystemParameters_News Delete(SystemParameters_News postednews)
        {
            var news = GetById(postednews);
            news.IsDeleted = true;
            news.CreationTime = Parameters.CurrentDateTime;
            news.CreatorUserId = Parameters.UserId;
            return Save(news);
        }

        private SystemParameters_News GetById(SystemParameters_News postednews)
        {
            var obj = _db.SystemParameters_News.Find(postednews.Id);
            return obj;
        }
        private SystemParameters_News_Translate GetMutliLangById(SystemParameters_News_Translate postednews)
        {
            var obj = _db.SystemParameters_News_Translate.Find(postednews.Id);
            return obj;
        }

        public SystemParameters_News_Translate DeleteByLang(SystemParameters_News_Translate postednews)
        {
            SystemParameters_News_Translate news = GetMutliLangById(postednews);
            news.IsDeleted = true;
            news.CreationTime = Parameters.CurrentDateTime;
            news.CreatorUserId = Parameters.UserId;
            return SaveByLang(news);
        }
    }
}
