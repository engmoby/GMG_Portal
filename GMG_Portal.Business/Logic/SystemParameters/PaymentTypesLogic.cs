using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
 

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class PaymentTypesLogic
    {
        GMG_Portal_DBEntities1 DB;

        public PaymentTypesLogic()
        {
            DB = new GMG_Portal_DBEntities1();
        }

        public List<PaymentTypes> GetAllWithDeleted()
        {
            return null;
           // return DB.PaymentTypes.OrderBy(p => p.IsDeleted).ToList();
        }

        public List<PaymentTypes> GetAll()
        {
            return null;
          //  return DB.PaymentTypes.Where(p => p.IsDeleted != true).ToList();
        }

        public PaymentTypes Get(int ID)
        {
            return null;
            //return DB.PaymentTypes.Find(ID);
        }
        private PaymentTypes Save(PaymentTypes PaymentType)
        {
            try
            {
                DB.SaveChanges();
                PaymentType.OperationStatus = "Succeded";
                return PaymentType;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_PaymentTypes_Ar"))
                    {
                        PaymentType.OperationStatus = "NameArMustBeUnique";
                        return PaymentType;
                    }
                    else if (e.InnerException.ToString().Contains("IX_PaymentTypes_En"))
                    {
                        PaymentType.OperationStatus = "NameEnMustBeUnique";
                        return PaymentType;
                    }
                }
                throw;
            }
        }

        public PaymentTypes Insert(PaymentTypes PostedPaymentTypes)
        {
            return null;
            //var PaymentType = new PaymentTypes()
            //{
            //    NameAr = PostedPaymentTypes.NameAr,
            //    NameEn = PostedPaymentTypes.NameEn,
            //    IsDeleted = false

            //};
            //DB.PaymentTypes.Add(PaymentType);
            //return Save(PaymentType);
        }
        public PaymentTypes Edit(PaymentTypes PostedPaymentType)
        {
            return null;
            //PaymentTypes PaymentType = Get(PostedPaymentType.ID);
            //PaymentType.NameEn = PostedPaymentType.NameEn;
            //PaymentType.NameAr = PostedPaymentType.NameAr;
            //PaymentType.IsDeleted = PostedPaymentType.IsDeleted;
            //return Save(PaymentType);
        }
        public PaymentTypes Delete(PaymentTypes PostedPaymentType)
        {
            return null;
            //PaymentTypes PaymentType = Get(PostedPaymentType.ID);
            //if (DB.CustomerPoints.Where(p => p.PaymentTypeID == PostedPaymentType.ID && p.IsDeleted!=true).Count() > 0)
            //{
            //    PaymentType.OperationStatus = "HasRelationship";
            //    return PaymentType;
            //}

            //PaymentType.IsDeleted = true;
            //return Save(PaymentType);
        }
    }
}
