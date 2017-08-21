﻿using System.Collections.Generic;
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


        public List<HomeView> GetAll()
        {
            return _db.HomeViews.ToList();
        }
        public int CountOccurenceOfValue(List<HomeView> list, string valueToFind)
        {
            return ((from temp in list where temp.Equals(valueToFind) select temp).Count());
        }
        public List<HomeView> GetAllByLangId(string landId)
        {
            return _db.HomeViews.ToList();
        }
    }
}
