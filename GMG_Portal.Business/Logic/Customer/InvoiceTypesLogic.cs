using System;
using System.Collections.Generic;
using System.Linq;
using GMG_Portal.Data;

namespace GMG_Portal.Business.Logic.Customer
{
    public class InvoiceTypesLogic
    {
        GMG_Portal_DBEntities DB;

        public InvoiceTypesLogic()
        {
            DB = new GMG_Portal_DBEntities();
        }

        public List<InvoiceTypes> GetAll()
        {
            return null;
            //return DB.InvoiceTypes.Where(PS => PS.IsDeleted != true).ToList();
        }

        public InvoiceTypes Get(int ID)
        {
            return null;
          //  return DB.InvoiceTypes.Find(ID);
        }
        private InvoiceTypes Save(InvoiceTypes InvoiceType)
        {
            try
            {
                DB.SaveChanges();
                InvoiceType.OperationStatus = "Succeded";
                return InvoiceType;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_InvoiceTypes_Ar"))
                    {
                        InvoiceType.OperationStatus = "NameArMustBeUnique";
                        return InvoiceType;
                    }
                    else if (e.InnerException.ToString().Contains("IX_InvoiceTypes_En"))
                    {
                        InvoiceType.OperationStatus = "NameEnMustBeUnique";
                        return InvoiceType;
                    }
                }
                throw;
            }
        }

        public InvoiceTypes Insert(InvoiceTypes PostedInvoiceType)
        {
            return null;
            //var InvoiceType = new InvoiceTypes()
            //{
            //    NameAr = PostedInvoiceType.NameAr,
            //    NameEn = PostedInvoiceType.NameEn,
            //    IsDeleted = false

            //};
            //DB.InvoiceTypes.Add(InvoiceType);
            //return Save(InvoiceType);
        }
        public InvoiceTypes Edit(InvoiceTypes PostedInvoiceType)
        {
            return null;
            //InvoiceTypes InvoiceType = Get(PostedInvoiceType.ID);
            //InvoiceType.NameEn = PostedInvoiceType.NameEn;
            //InvoiceType.NameAr = PostedInvoiceType.NameAr;
            //return Save(InvoiceType);
        }
        public InvoiceTypes Delete(InvoiceTypes PostedInvoiceType)
        {
            return null;
            //InvoiceTypes InvoiceType = Get(PostedInvoiceType.ID);

            //InvoiceType.NameAr = InvoiceType.ID + "-" + InvoiceType.NameAr;
            //InvoiceType.NameEn = InvoiceType.ID + "-" + InvoiceType.NameEn;
            //InvoiceType.IsDeleted = true;
            //return Save(InvoiceType);
        }

    }
}
