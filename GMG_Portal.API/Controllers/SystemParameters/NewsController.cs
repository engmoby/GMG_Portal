using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GMG_Portal.API.Models.SystemParameters;
using GMG_Portal.Data;
using GMG_Portal.Business.Logic.SystemParameters;
using AutoMapper;
using Heloper;
using Helpers;

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/News")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NewsController : ApiController
    {
        public HttpResponseMessage GetAll(string langId)
        {
            try
            {
                var newsLogic = new NewsLogic();

                List<News> newsObj = null;
                newsObj = newsLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<NewsModel>>(newsObj));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }
        public HttpResponseMessage GetAllWithCount(string langId)
        {
            try
            {
                var newsLogic = new NewsLogic();
                var news = newsLogic.GetAllWithCount(langId);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<NewsModel>>(news));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }
        public HttpResponseMessage GetLatestNewsWithOutCurrentId(int newsId, string langId)
        {
            try
            {
                var newsLogic = new NewsLogic();
                var news = newsLogic.GetLatestNewsWithOutCurrentId(newsId, langId);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<NewsModel>>(news));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }
        public HttpResponseMessage GetAllByCatrgoryId(int categoryId, string langId)
        {
            try
            {
                var newsModel = new News();
                var newsLogic = new NewsLogic();
                var news = newsLogic.GetAllByCatrgoryId(categoryId, langId);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<NewsModel>>(news));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public HttpResponseMessage SearchNews(string keyword, string langId)
        {
            try
            {
                var newsLogic = new NewsLogic();
                var news = newsLogic.SearchNews(keyword);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<NewsModel>>(news));

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        public HttpResponseMessage GetAllWithDeleted(string langId)
        {
            try
            {
                var newsLogic = new NewsLogic();
                var news = newsLogic.GetAllWithDeleted(langId);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<NewsModel>>(news));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        public HttpResponseMessage GetNewsDetails(int id, string langId)
        {
            try
            {
                var newsLogic = new NewsLogic();
                var newss = newsLogic.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<NewsModel>(newss));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage Save(News postedNews)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newsLogic = new NewsLogic();
                    News newsObj = null;
                    if (postedNews.Id.Equals(0))
                    {
                        newsObj = newsLogic.Insert(Mapper.Map<News>(postedNews));
                    }
                    else
                    {
                        if (postedNews.IsDeleted)
                        {
                            newsObj = newsLogic.Delete(Mapper.Map<News>(postedNews));

                        }
                        else
                        {
                            newsObj = newsLogic.Edit(Mapper.Map<News>(postedNews));

                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<NewsModel>(newsObj));

                }
                goto ThrowBadRequest;
            }

            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            ThrowBadRequest:
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }

}
