﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using GMG_Portal.Data;

namespace GMG_Portal.Business.Logic.General
{
    public class GeneralLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public GeneralLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }


        //public List<HomeView> GetAll()
        //{
        //    return _db.HomeViews.ToList();
        //}
    
        //public List<HomeView> GetAllByLangId(string landId)
        //{
        //    return _db.HomeViews.ToList();
        //}
        public DataSet Sqlread(string sqlquery)
        {
            DataSet functionReturnValue = default(DataSet);
            try
            {
               
                //string conStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString.ToString();
                string conStr = "Data Source = tcp:gmg.database.windows.net,1433; Initial Catalog = GMG_PORTAL_STAG; User Id = gmg_admin@gmg; Password = gCv3XfIkABJWl2hK;";


                SqlConnection thisConnection = default(SqlConnection);
                thisConnection = new SqlConnection(conStr);
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


    }
}
