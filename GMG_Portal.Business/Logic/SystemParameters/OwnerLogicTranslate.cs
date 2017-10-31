using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper; 

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class OwnerLogicTranslate
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public OwnerLogicTranslate()
        {
            _db = new GMG_Portal_DBEntities1();
        }

        //Back and Front  Fetch Logic 
        public List<SystemParameters_Owners_Translate> GetAllWithDeleted(string langId)
        {
            return _db.SystemParameters_Owners_Translate.Where(p => p.langId == langId).OrderBy(p => p.IsDeleted && p.langId == langId).ToList().OrderBy(p => p.Sorder).ToList();
        }
        public List<SystemParameters_Owners_Translate> GetAll(string langId)
        {
            return _db.SystemParameters_Owners_Translate.Where(p => p.IsDeleted != true && p.langId == langId).OrderBy(p => p.Sorder).ToList();
        }


        //Crud Logic 
        public SystemParameters_Owners_Translate Get(int id, string langId)
        {
            return _db.SystemParameters_Owners_Translate.FirstOrDefault(x => x.Id == id && x.langId == langId);
        }
        private SystemParameters_Owners_Translate Save(SystemParameters_Owners_Translate obj)
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
        public SystemParameters_Owners_Translate Insert(SystemParameters_Owners_Translate postedOwner)
        {
            var maxcount = GetMaxCountofOrder();
            var obj = new SystemParameters_Owners_Translate()
            {
                DisplayValueName = postedOwner.DisplayValueName,
                DisplayValuePosition = postedOwner.DisplayValuePosition,
                DisplayValueDesc = postedOwner.DisplayValueDesc,
                Image= postedOwner.Image, 
                Show = Parameters.Show,  
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId, 
                langId  = postedOwner.langId,
                Sorder = maxcount,

            };
            _db.SystemParameters_Owners_Translate.Add(obj);
            return Save(obj);
        }
        public SystemParameters_Owners_Translate Edit(SystemParameters_Owners_Translate postedOwner)
        {
            SystemParameters_Owners_Translate obj = Get(postedOwner.Id,postedOwner.langId);
            obj.DisplayValueName = postedOwner.DisplayValueName;
            obj.DisplayValuePosition = postedOwner.DisplayValuePosition;
            obj.DisplayValueDesc = postedOwner.DisplayValueDesc;
            obj.Image = postedOwner.Image; 
            obj.IsDeleted = postedOwner.IsDeleted;
            obj.Show = postedOwner.Show; 
            obj.LastModificationTime = Parameters.CurrentDateTime;
            obj.LastModifierUserId = Parameters.UserId;
            //Update to Magically Replace the Numbers !
            var corder = GetCurrentOrder(postedOwner.Id);
            Savetheorder(postedOwner.Sorder, corder, postedOwner.Id);
            return Save(obj);
        }
        public SystemParameters_Owners_Translate Delete(SystemParameters_Owners_Translate postedOwner)
        {
            SystemParameters_Owners_Translate obj = Get(postedOwner.Id,postedOwner.langId);
         
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
        public string Savetheorder(int Sorder, int currentOrder, int Sid)
        {
            DataTable DT;
            DT = Sqlread("SELECT * FROM [dbo].[SystemParameters.Owners_Translate] WHERE Sorder=" + Sorder).Tables[0];
            if (DT.Rows.Count > 0)
            {
                Sqlinsert("UPDATE [dbo].[SystemParameters.Owners_Translate] SET Sorder=" + Sorder + " WHERE Id=" + Sid);
                var SQ = "UPDATE [dbo].[SystemParameters.Owners_Translate] SET Sorder=" + currentOrder + " WHERE Id=" +
                         DT.Rows[0].Field<int>("Id");
                Sqlinsert(SQ);
            }
            else
            {
                Sqlinsert("UPDATE [dbo].[SystemParameters.Owners_Translate] SET Sorder=" + Sorder + " WHERE Id=" + Sid);
            }




            return null;

        }
        public int GetCurrentOrder(int sId)
        {
            DataTable DT;
            DT = Sqlread("SELECT Sorder FROM [dbo].[SystemParameters.Owners_Translate] WHERE Id=" + sId).Tables[0];
            if (DT.Rows.Count > 0)
            {
                return DT.Rows[0].Field<int>("Sorder");
            }

            return 99;
        }

        public int GetMaxCountofOrder()
        {
            DataTable DT;
            DT = Sqlread("SELECT * FROM [dbo].[SystemParameters.Owners_Translate] ORDER BY Sorder DESC").Tables[0];
            if (DT.Rows.Count > 0)
            {
                return DT.Rows[0].Field<int>("Sorder") + 1;
            }

            return 99;
        }


    }
}
