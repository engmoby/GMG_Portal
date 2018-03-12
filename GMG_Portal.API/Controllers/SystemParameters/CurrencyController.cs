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
    [RoutePrefix("SystemParameters/Currency")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CurrencyController : ApiController
    {
        public HttpResponseMessage GetAll(string langId)
        {
            try
            {
                var currencyLogic = new CurrencyLogic();
                //var currencyLogicTranslate = new CurrencyLogicTranslate();
                //if (langId == Parameters.DefaultLang)
                //{
                    var obj = currencyLogic.GetAll();
                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<CurrencyModel>>(obj));
                //}
                //else

                //{
                //    var objByLang = currencyLogicTranslate.GetAll(langId);
                //    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<CurrencyVm>>(objByLang));
                //}
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }
        public HttpResponseMessage GetAllWithDeleted(string langId)
        {
            try
            {
                var currencyLogic = new CurrencyLogic();
                //var currencyLogicTranslate = new CurrencyLogicTranslate();

                //if (langId == Parameters.DefaultLang)
                //{
                    var obj = currencyLogic.GetAllWithDeleted();

                    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<CurrencyModel>>(obj));

                //}
                //else

                //{
                //    var objByLang = currencyLogicTranslate.GetAllWithDeleted(langId);

                //    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<CurrencyVm>>(objByLang));

                //}

            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                return Request.CreateResponse(ex);
            }
        }
        [HttpPost]
        public HttpResponseMessage Save(CurrencyModel postedCurrencys)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var currencyLogic = new CurrencyLogic();
                  //var currencyLogicTranslate = new CurrencyLogicTranslate();

                    Currency obj = null;
                  //  Currency_Translate objByLang = null;

                    if (postedCurrencys.Id.Equals(0))
                    {
                       // if (postedCurrencys.LangId == Parameters.DefaultLang)
                            obj = currencyLogic.Insert(Mapper.Map<Currency>(postedCurrencys));
                        //else
                        //    objByLang = currencyLogicTranslate.Insert(Mapper.Map<Currency_Translate>(postedCurrencys));

                    }
                    else
                    {
                        if (postedCurrencys.IsDeleted)
                        {
                          //  if (postedCurrencys.LangId == Parameters.DefaultLang)
                                obj = currencyLogic.Delete(Mapper.Map<Currency>(postedCurrencys));
                            //else
                            //    objByLang = currencyLogicTranslate.Delete(Mapper.Map<Currency_Translate>(postedCurrencys));
                        }
                        else
                        {
                          //  if (postedCurrencys.LangId == Parameters.DefaultLang)
                                obj = currencyLogic.Edit(Mapper.Map<Currency>(postedCurrencys));
                            //else
                            //    objByLang = currencyLogicTranslate.Edit(Mapper.Map<Currency_Translate>(postedCurrencys));
                        }
                    }
                    //if (postedCurrencys.LangId == Parameters.DefaultLang)
                        return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<CurrencyModel>(obj));
                    //else
                    //    return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<Currency_Translate>(objByLang));
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
