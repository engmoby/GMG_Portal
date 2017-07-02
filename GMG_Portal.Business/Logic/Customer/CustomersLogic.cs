using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using GMG_Portal.Data;
using Helper;

namespace GMG_Portal.Business.Logic.Customer
{
    public class CustomersLogic
    {
        GMG_Portal_DBEntities1 DB;
        bool _insert = false;
        public CustomersLogic()
        {
            DB = new GMG_Portal_DBEntities1();
        }

        //public List<View_GetAllCustomersWithLookupsNames> GetAll()
        //{
        //    //return DB.Customers.Where(C => C.IsDeleted != true).ToList();
        //    return DB.View_GetAllCustomersWithLookupsNames.Where(p => p.IsDeleted != true).ToList();
        //}
        //public List<View_GetAllCustomersWithLookupsNames> GetAllWithDeleted()
        //{
        //    //return DB.Customers.Where(C => C.IsDeleted != true).ToList();
        //    return DB.View_GetAllCustomersWithLookupsNames.OrderBy(p => p.IsDeleted).ToList();
        //}



        private Customers Save(Customers Customer)
        {
            return null;
            //try
            //{
            //    DataTable Customers = SqlHelp.ToDataTable<Customers>(Customer,new string[]{"ContractDocuments", "MaintenanceContracts", "MaintenanceRequests",
            //        "AccountStatuses","AccountTypes","CustomerAccountStatus","CustomerInstallments","Regions","OperationStatus" });
            //    DataTable CustomerInstallments = SqlHelp.ToDataTable<CustomerInstallments>(Customer.CustomerInstallments,new string[] { "Customers" , "OperationStatus" });

            //    var param1 = new SqlParameter
            //    {
            //        ParameterName = "@Customers",
            //        SqlDbType = SqlDbType.Structured,
            //        TypeName = "[SystemParameter].[Customers]",
            //        Value = Customers
            //    };
            //    var param2 = new SqlParameter
            //    {
            //        ParameterName = "@CustomerInstallments",
            //        SqlDbType = SqlDbType.Structured,
            //        TypeName = "[SystemParameter].[CustomerInstallments]",
            //        Value = CustomerInstallments
            //    };
            //    var param3 = new SqlParameter
            //    {
            //        ParameterName = "@Insert",
            //        SqlDbType = SqlDbType.Bit,
            //        Value = _insert
            //    };

               

            //    DB.Database.ExecuteSqlCommand("EXEC	[SystemParameter].[SP_SaveCustomer] @Customers, @CustomerInstallments , @Insert", param1, param2, param3);

            //    Customer.OperationStatus = "Succeded";

            //    return Customer;
            //}
            //catch (Exception e)
            //{
            //    if (e.InnerException != null)
            //    {
            //        if (e.InnerException.ToString().Contains("IX_Customers_Ar"))
            //        {
            //            Customer.OperationStatus = "NameArMustBeUnique";
            //            return Customer;
            //        }
            //        else if (e.InnerException.ToString().Contains("IX_Customers_En"))
            //        {
            //            Customer.OperationStatus = "NameEnMustBeUnique";
            //            return Customer;
            //        }
            //    }
            //    throw;
            //}
        }

        public Customers Insert(Customers PostedCustomer)
        {
            return null;
            //_insert = true;
            //PostedCustomer.ID = Guid.NewGuid();

            //for (int i = 0; i < PostedCustomer.CustomerInstallments.Count(); i++)
            //{
            //    (PostedCustomer.CustomerInstallments as List<CustomerInstallments>)[i].CustomerID = PostedCustomer.ID;
            //    (PostedCustomer.CustomerInstallments as List<CustomerInstallments>)[i].ID = Guid.NewGuid();
            //}

            //return Save(PostedCustomer);

        }
        public Customers Edit(Customers PostedCustomer)
        {
            return null;
            //_insert = false;

            //for (int i = 0; i < PostedCustomer.CustomerInstallments.Count(); i++)
            //{
            //    if ((PostedCustomer.CustomerInstallments as List<CustomerInstallments>)[i].ID == Guid.Empty)
            //    {
            //        (PostedCustomer.CustomerInstallments as List<CustomerInstallments>)[i].CustomerID = PostedCustomer.ID;
            //        (PostedCustomer.CustomerInstallments as List<CustomerInstallments>)[i].ID = Guid.NewGuid();
            //    }
            //}
            //return Save(PostedCustomer);
        }
        public Customers Delete(Customers PostedCustomer)
        {
            return null;
            //_insert = false;

            //PostedCustomer.IsDeleted = true;
            //return Save(PostedCustomer);
        }

    }
}
