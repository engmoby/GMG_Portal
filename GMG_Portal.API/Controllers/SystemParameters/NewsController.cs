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
using Helpers;

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/News")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NewsController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var newsLogic = new NewsLogic();
                var news = newsLogic.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<News>>(news));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }
        public HttpResponseMessage GetAllByCatrgoryId(int categoryId)
        {
            try
            {
                var newsLogic = new NewsLogic();
                var news = newsLogic.GetAllByCatrgoryId(categoryId);
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

        public HttpResponseMessage GetAllWithDeleted()
        {
            try
            {
                var newsLogic = new NewsLogic();
                var news = newsLogic.GetAllWithDeleted();
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<API.Models.SystemParameters.News>>(news));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        public HttpResponseMessage GetNewsDetails(int id)
        {
            try
            {
                var newsLogic = new NewsLogic();
                var newss = newsLogic.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<News>(newss));
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
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
                    SystemParameters_News NewsObj = null;
                    if (postedNews.Id.Equals(0))
                    {
                        NewsObj = newsLogic.Insert(Mapper.Map<SystemParameters_News>(postedNews));
                    }
                    else
                    {
                        if (postedNews.IsDeleted)
                        {
                            NewsObj = newsLogic.Delete(Mapper.Map<SystemParameters_News>(postedNews));
                        }
                        else
                        {
                            NewsObj = newsLogic.Edit(Mapper.Map<SystemParameters_News>(postedNews));
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<News>(NewsObj));
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
