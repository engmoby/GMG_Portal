using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using AutoMapper;
using System.Data;
using GMG_Portal.Data.Partials.SystemParameters;


namespace GMG_Portal.Business.Logic.SystemParameters
{
  public class AccountTypeLogic
    {
        GMG_Portal_DBEntities1 DB;

        public AccountTypeLogic()
        {
            DB = new GMG_Portal_DBEntities1();
        }

        public List<AccountTypes> GetAllWithDeleted()
        {
            return null;
          //  return DB.AccountTypes.OrderBy(p => p.IsDeleted).ToList();
        }

        public List<AccountTypes> GetAll()
        {
            return null;
          //  return DB.AccountTypes.Where(p=>p.IsDeleted!=true).ToList();
        }

        public AccountTypes Get(int ID)
        {
            return null;
          //  return DB.AccountTypes.Find(ID);
        }
        private AccountTypes Save(AccountTypes AccountType)
        {
            try
            {
                DB.SaveChanges();
                AccountType.OperationStatus = "Succeded";
                return AccountType;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_AccountTypes_Ar"))
                    {
                        AccountType.OperationStatus = "NameArMustBeUnique";
                        return AccountType;
                    }
                    else if (e.InnerException.ToString().Contains("IX_AccountTypes_En"))
                    {
                        AccountType.OperationStatus = "NameEnMustBeUnique";
                        return AccountType;
                    }
                }
                throw;
            }
        }

        public AccountTypes Insert(AccountTypes PostedAccountTypes)
        {
            return null;
            //var AccountType = new AccountTypes()
            //{
            //    NameAr = PostedAccountTypes.NameAr,
            //    NameEn = PostedAccountTypes.NameEn,
            //    IsDeleted = false

            //};
            //DB.AccountTypes.Add(AccountType);
            //return Save(AccountType);
        }
        public AccountTypes Edit(AccountTypes PostedAccountType)
        {
            return null;
            //AccountTypes AccountType = Get(PostedAccountType.ID);
            //AccountType.NameEn = PostedAccountType.NameEn;
            //AccountType.NameAr = PostedAccountType.NameAr;
            //AccountType.IsDeleted = PostedAccountType.IsDeleted;
            //return Save(AccountType);
        }
        public AccountTypes Delete(AccountTypes PostedAccountType)
        {
            return null;
          //  AccountTypes AccountType = Get(PostedAccountType.ID);
           
           // AccountType.IsDeleted = true;
            //return Save(AccountType);
        }


    }
}
