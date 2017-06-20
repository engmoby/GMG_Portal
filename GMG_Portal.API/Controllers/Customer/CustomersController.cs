using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GMG_Portal.Data;
using GMG_Portal.Business.Logic.SystemParameters;
using GMG_Portal.API.Models.SystemParameters;
using Helpers;
using AutoMapper;
using System.Web;
using System.IO;
using GMG_Portal.Business.Logic.Customer;

namespace GMG_Portal.API.Controllers.SystemParameters
{
    [RoutePrefix("SystemParameters/Customers")]
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomersController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            try
            {
                var CustomersLogic = new CustomersLogic();
              //  var Customers = CustomersLogic.GetAll();

                return null;
               // return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Models.SystemParameters.Customer>>(Customers));
            }

            catch (Exception e)
            {
                Log.LogError(e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }
        public HttpResponseMessage GetAllWithDeleted()
        {
            try
            {
                var CustomersLogic = new CustomersLogic();
                //var Customers = CustomersLogic.GetAllWithDeleted();

                return null;

                //return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<List<Models.SystemParameters.Customer>>(Customers));
            }

            catch (Exception e)
            {
                Log.LogError(e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        public HttpResponseMessage GetContractImages(Guid CustomerID)
        {
            try
            {
                string ContractInformationsFolderPath = HttpContext.Current.Server.MapPath(@"~/Images/Customer Contract Informations Images/" + CustomerID);
                var ContractsImages = Image.GetFolderImages(ContractInformationsFolderPath);
                return Request.CreateResponse(HttpStatusCode.OK, ContractsImages);


            }

            catch (Exception e)
            {
                Log.LogError(e);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }


        [HttpPost]
        public HttpResponseMessage Save(GMG_Portal.API.Models.SystemParameters.Customer PostedCustomer)
        {
            return null;
            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        var CustomersLogic = new CustomersLogic();
            //        Data.Customers Customer = null;
            //        if (PostedCustomer.ID.Equals(Guid.Empty))
            //        {
            //            PostedCustomer.CustomerAccountStatusID = 2; //FOR INACTIVE
            //            PostedCustomer.AccountStatusNameAr = "غير نشط";
            //            PostedCustomer.AccountStatusNameEn = "Inactive";
            //            Customer = CustomersLogic.Insert(Mapper.Map<GMG_Portal.Data.Customers>(PostedCustomer));

            //            //create folder contains product images at folder products images
            //            string ContractInformationsFolderPath = HttpContext.Current.Server.MapPath(@"~/Images/Customer Contract Informations Images/" + Customer.ID);
            //            if (!Directory.Exists(ContractInformationsFolderPath))
            //            {
            //                Directory.CreateDirectory(ContractInformationsFolderPath);
            //            }
            //            string filePath;
            //            foreach (var image in PostedCustomer.ContractImages)
            //            {
            //                filePath = ContractInformationsFolderPath + "\\" + Guid.NewGuid() + ".jpeg";
            //                Image.WriteImg(filePath, image);
            //            }
            //        }
            //        else
            //        {
            //            if (PostedCustomer.IsDeleted == true)
            //            {
            //                Customer = CustomersLogic.Delete(Mapper.Map<GMG_Portal.Data.Customers>(PostedCustomer));
            //            }
            //            else
            //            {
            //                Customer = CustomersLogic.Edit(Mapper.Map<GMG_Portal.Data.Customers>(PostedCustomer));

            //            }
            //        }

            //        Models.SystemParameters.Customer ReturnedCustomer = Mapper.Map<GMG_Portal.API.Models.SystemParameters.Customer>(Customer);
            //        ReturnedCustomer.AccountTypeNameAr = PostedCustomer.AccountTypeNameAr;
            //        ReturnedCustomer.AccountTypeNameEn = PostedCustomer.AccountTypeNameEn;
            //        ReturnedCustomer.PaymentTypeNameAr = PostedCustomer.PaymentTypeNameAr;
            //        ReturnedCustomer.PaymentTypeNameEn = PostedCustomer.PaymentTypeNameEn;
            //        ReturnedCustomer.CountryNameAr = PostedCustomer.CountryNameAr;
            //        ReturnedCustomer.CountryID = PostedCustomer.CountryID;
            //        ReturnedCustomer.CountryNameEn = PostedCustomer.CountryNameEn;
            //        ReturnedCustomer.CityNameAr = PostedCustomer.CityNameAr;
            //        ReturnedCustomer.CityNameEn = PostedCustomer.CityNameEn;
            //        ReturnedCustomer.RegionNameAr = PostedCustomer.RegionNameAr;
            //        ReturnedCustomer.RegionNameEn = PostedCustomer.RegionNameEn;
            //        ReturnedCustomer.AccountStatusNameEn = PostedCustomer.AccountStatusNameEn;
            //        ReturnedCustomer.AccountStatusNameAr = PostedCustomer.AccountStatusNameAr;
            //        ReturnedCustomer.ContractImages = PostedCustomer.ContractImages;
            //        //var CustomerInstallments= new CustomerInstallmentLogic().GetAll(ReturnedCustomer.ID);
            //        //ReturnedCustomer.CustomerInstallments = Mapper.Map<List<Models.SystemParameters.CustomerInstallment>>(CustomerInstallments); 
            //        return Request.CreateResponse(HttpStatusCode.OK, ReturnedCustomer);
            //    }
            //    goto ThrowBadRequest;

            //}

            //catch (Exception e)
            //{
            //    Log.LogError(e);
            //    return Request.CreateResponse(HttpStatusCode.InternalServerError);
            //}

            //ThrowBadRequest:
            //return Request.CreateResponse(HttpStatusCode.BadRequest);

        }
    }
}
