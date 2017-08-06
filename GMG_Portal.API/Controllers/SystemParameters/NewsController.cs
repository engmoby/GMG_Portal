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
                var news = newsLogic.GetAll(langId);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<News>>(news));
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
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<News>>(news));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }
        public HttpResponseMessage GetLatestNewsWithOutCurrentId(int newsId,string langId)
        {
            try
            {
                var newsLogic = new NewsLogic();
                var news = newsLogic.GetLatestNewsWithOutCurrentId(newsId,langId);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<News>>(news));
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
                newsModel.LangId = langId;
                var newsLogic = new NewsLogic();
                var news = newsLogic.GetAllByCatrgoryId(categoryId, newsModel.LangId);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<News>>(news));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public HttpResponseMessage SearchNews(string keyword)
        {
            try
            {
                var newsLogic = new NewsLogic();
                var news = newsLogic.SearchNews(keyword);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<News>>(news));
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
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<API.Models.SystemParameters.News>>(news));
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
                var newsModel = new News();
                newsModel.LangId = langId;
                var newsLogic = new NewsLogic();
                var newss = newsLogic.Get(id, newsModel.LangId);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<News>(newss));
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
                    SystemParameters_News newsObj = null;
                    SystemParameters_News_Translate newsObjByLang = null;
                    if (postedNews.Id.Equals(0))
                    {
                        if (postedNews.LangId == Parameters.DefaultLang)
                            newsObj = newsLogic.Insert(Mapper.Map<SystemParameters_News>(postedNews));
                        else
                            newsObjByLang = newsLogic.InsertByLang(Mapper.Map<SystemParameters_News_Translate>(postedNews));
                    }
                    else
                    {
                        if (postedNews.IsDeleted)
                        {
                            if (postedNews.LangId == Parameters.DefaultLang)
                                newsObj = newsLogic.Delete(Mapper.Map<SystemParameters_News>(postedNews));
                            else
                                newsObjByLang = newsLogic.DeleteByLang(Mapper.Map<SystemParameters_News_Translate>(postedNews));

                        }
                        else
                        {
                            if (postedNews.LangId == Parameters.DefaultLang)
                                newsObj = newsLogic.Edit(Mapper.Map<SystemParameters_News>(postedNews));
                            else
                                newsObjByLang = newsLogic.EditByLang(Mapper.Map<SystemParameters_News_Translate>(postedNews));

                        }
                    }
                    if (postedNews.LangId == Parameters.DefaultLang)
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<News>(newsObj));
                    else
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<News>(newsObjByLang));

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
