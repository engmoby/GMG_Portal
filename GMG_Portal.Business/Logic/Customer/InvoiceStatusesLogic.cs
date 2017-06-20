using System;
using System.Collections.Generic;
using System.Linq;
using GMG_Portal.Data;

namespace GMG_Portal.Business.Logic.Customer
{
    public class InvoiceStatusesLogic
    {
        GMG_Portal_DBEntities DB;

        public InvoiceStatusesLogic()
        {
            DB = new GMG_Portal_DBEntities();
        }

        public List<InvoiceStatuses> GetAll()
        {
            return null;
          //  return DB.InvoiceStatuses.Where(p => p.IsDeleted!=true).ToList();
        }

        public InvoiceStatuses Get(int ID)
        {
            return null;
           // return DB.InvoiceStatuses.Find(ID);
        }
        private InvoiceStatuses Save(InvoiceStatuses InvoiceStatus)
        {
            try
            {
                DB.SaveChanges();
                InvoiceStatus.OperationStatus = "Succeded";
                return InvoiceStatus;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_InvoiceStatuses_Ar"))
                    {
                        InvoiceStatus.OperationStatus = "NameArMustBeUnique";
                        return InvoiceStatus;
                    }
                    else if (e.InnerException.ToString().Contains("IX_InvoiceStatuses_En"))
                    {
                        InvoiceStatus.OperationStatus = "NameEnMustBeUnique";
                        return InvoiceStatus;
                    }
                }
                throw;
            }
        }

        public InvoiceStatuses Insert(InvoiceStatuses PostedInvoiceStatuses)
        {
            return null;
            //var InvoiceStatus = new InvoiceStatuses()
            //{
            //    NameAr = PostedInvoiceStatuses.NameAr,
            //    NameEn = PostedInvoiceStatuses.NameEn,
            //    IsDeleted = false

            //};
            //DB.InvoiceStatuses.Add(InvoiceStatus);
            //return Save(InvoiceStatus);
        }
        public InvoiceStatuses Edit(InvoiceStatuses PostedInvoiceStatus)
        {
            return null;
            //InvoiceStatuses InvoiceStatus = Get(PostedInvoiceStatus.ID);
            //InvoiceStatus.NameEn = PostedInvoiceStatus.NameEn;
            //InvoiceStatus.NameAr = PostedInvoiceStatus.NameAr;
            //InvoiceStatus.IsDeleted = PostedInvoiceStatus.IsDeleted;
            //return Save(InvoiceStatus);
        }
        public InvoiceStatuses Delete(InvoiceStatuses PostedInvoiceStatus)
        {
            return null;
            //InvoiceStatuses InvoiceStatus = Get(PostedInvoiceStatus.ID);

            //InvoiceStatus.IsDeleted = true;
            //return Save(InvoiceStatus);
        }
    }
}
