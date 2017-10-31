using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Business.Logic.General;
using GMG_Portal.Data;
using Heloper; 

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class OwnerLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public OwnerLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<SystemParameters_Owners> GetAllWithDeleted()
        {
            return _db.SystemParameters_Owners.OrderBy(p => p.IsDeleted).ThenBy(p => p.Sorder).ToList();
        }
        public List<SystemParameters_Owners> GetAll()
        {
            var returnList = new List<SystemParameters_Owners>();
            var getOwnerList = _db.SystemParameters_Owners.Where(p => p.IsDeleted == false && p.Show == true).ToList().OrderBy(p => p.Sorder).ToList();
            foreach (var ownerse in getOwnerList)
            {

                returnList.Add(new SystemParameters_Owners
                {
                    Id = ownerse.Id, 
                    DisplayValueName = ownerse.DisplayValueName,
                    DisplayValuePosition= ownerse.DisplayValuePosition,
                    DisplayValueDesc = ownerse.DisplayValueDesc, 
                    Image = ownerse.Image,
                    Facebook = ownerse.Facebook,
                    Twitter = ownerse.Twitter,
                    LinkedIn = ownerse.LinkedIn,
                    Bootstrap = 12 / getOwnerList.Count,
                    Sorder = ownerse.Sorder
                  
                });
            }
            return returnList;
            //return _db.SystemParameters_Owners.Where(p => p.IsDeleted == false && (bool) p.Show).ToList();
        }
        public SystemParameters_Owners Get(int id)
        {
            return _db.SystemParameters_Owners.Find(id);
        }

        public SystemParameters_Owners GetByOrder(int Sorder)
        {
            //var getOwnerOrder = _db.SystemParameters_Owners.Where(p => p.IsDeleted == false && p.Show == true && p.Sorder == Sorder ).ToList().OrderBy(p => p.Sorder).ToList();
            return _db.SystemParameters_Owners.FirstOrDefault(p => p.IsDeleted == false && p.Show == true && p.Sorder == Sorder);
        }
        private SystemParameters_Owners Save(SystemParameters_Owners obj)
        {
            try
            {
                _db.SaveChanges();
                obj.OperationStatus = "Succeded";
                return obj;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        obj.OperationStatus = "NameArMustBeUnique";
                        return obj;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        obj.OperationStatus = "NameEnMustBeUnique";
                        return obj;
                    }
                }
                throw;
            }
        }
        public SystemParameters_Owners Insert(SystemParameters_Owners postedOwner)
        {

            var maxcount = GetMaxCountofOrder();
            var obj = new SystemParameters_Owners()
            {
                DisplayValueName = postedOwner.DisplayValueName,
                DisplayValuePosition = postedOwner.DisplayValuePosition,
                DisplayValueDesc = postedOwner.DisplayValueDesc,
                Facebook= postedOwner.Facebook, 
                Twitter= postedOwner.Twitter, 
                Image= postedOwner.Image, 
                LinkedIn= postedOwner.LinkedIn,
                Show = Parameters.Show,  
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId, 
                Sorder = maxcount,

            };
            _db.SystemParameters_Owners.Add(obj);
            return Save(obj);
        }
        public SystemParameters_Owners Edit(SystemParameters_Owners postedOwner)
        {
            SystemParameters_Owners owner = Get(postedOwner.Id);
            owner.DisplayValueName = postedOwner.DisplayValueName;
            owner.DisplayValuePosition = postedOwner.DisplayValuePosition;
            owner.DisplayValueDesc = postedOwner.DisplayValueDesc;
            owner.Facebook = postedOwner.Facebook;
            owner.Twitter = postedOwner.Twitter;
            owner.Image = postedOwner.Image;
            owner.LinkedIn = postedOwner.LinkedIn;
            owner.IsDeleted = postedOwner.IsDeleted;
            owner.Show = postedOwner.Show; 
            owner.LastModificationTime = Parameters.CurrentDateTime;
            owner.LastModifierUserId = Parameters.UserId;
            //Update to Magically Replace the Numbers !
            var corder = GetCurrentOrder(postedOwner.Id);
            Savetheorder(postedOwner.Sorder, corder, postedOwner.Id);









            return Save(owner);
        }
        public SystemParameters_Owners Delete(SystemParameters_Owners postedOwner)
        {
            SystemParameters_Owners obj = Get(postedOwner.Id);
            //if (_db.SystemParameters_Owners.Any(p => p.Id == postedOwner.Id && p.IsDeleted != true))
            //{
            //      //  Owner.OperationStatus = "HasRelationship";
            //    return obj;
            //}

            obj.IsDeleted = true;
            obj.CreationTime = Parameters.CurrentDateTime;
            obj.CreatorUserId = Parameters.UserId;
            return Save(obj);
        }




        public DataSet Sqlread(string sqlquery)
        {
            DataSet functionReturnValue = default(DataSet);
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

             
                SqlConnection thisConnection = default(SqlConnection);
                thisConnection = new SqlConnection(connectionString);
                string sql = sqlquery;

                thisConnection.Open();
                DataSet DS = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sql, thisConnection);
                da.Fill(DS);
                functionReturnValue = DS;
                thisConnection.Close();
            }
            catch (Exception ex)
            {
                //  Lookups.LogErrorToText("Web", "Settings.vb", "FillMenus", ex.Message)
            }
            return functionReturnValue;
        }
        public string Sqlinsert(string sqlquery)
        {
            string functionReturnValue = null;
            try
            {


                var connectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

                SqlConnection thisConnection = default(SqlConnection);


                thisConnection = new SqlConnection(connectionString);
                string sql = sqlquery;

                thisConnection.Open();

                dynamic objCmd = null;
                objCmd = new System.Data.SqlClient.SqlCommand();
                var _with1 = objCmd;
                _with1.Connection = thisConnection;
                _with1.CommandType = CommandType.Text;
                _with1.CommandText = sql;
                objCmd.ExecuteNonQuery();




                functionReturnValue = "SQL Execution Successful !";
                thisConnection.Close();

            }
            catch (Exception ex)
            {
                return ex.ToString();

                //  Lookups.LogErrorToText("Web", "Settings.vb", "FillMenus", ex.Message)
                //
            }
            return functionReturnValue;
        }
        public string Savetheorder(int Sorder, int currentOrder , int Sid)
        {
            DataTable DT;
            DT = Sqlread("SELECT * FROM [dbo].[SystemParameters.Owners] WHERE Sorder=" + Sorder).Tables[0];
            if (DT.Rows.Count > 0)
            {
                Sqlinsert("UPDATE [dbo].[SystemParameters.Owners] SET Sorder=" + Sorder + " WHERE Id=" + Sid);
                var SQ = "UPDATE [dbo].[SystemParameters.Owners] SET Sorder=" + currentOrder + " WHERE Id=" +
                         DT.Rows[0].Field<int>("Id");
                Sqlinsert(SQ);
            }
            else
            {
                Sqlinsert("UPDATE [dbo].[SystemParameters.Owners] SET Sorder=" + Sorder + " WHERE Id=" + Sid);
            }




            return null;

        }
        public int GetCurrentOrder(int sId)
        {
            DataTable DT;
            DT = Sqlread("SELECT Sorder FROM [dbo].[SystemParameters.Owners] WHERE Id=" + sId).Tables[0];
            if (DT.Rows.Count > 0)
            {
                return DT.Rows[0].Field<int>("Sorder");
            }

            return 99;
        }
        public int GetMaxCountofOrder()
        {
            DataTable DT;
            DT = Sqlread("SELECT * FROM [dbo].[SystemParameters.Owners] ORDER BY Sorder DESC").Tables[0];
            if (DT.Rows.Count > 0)
            {
                return DT.Rows[0].Field<int>("Sorder") + 1;
            }

            return 99;
        }


    }
}
