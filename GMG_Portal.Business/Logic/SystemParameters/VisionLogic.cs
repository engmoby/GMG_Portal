﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class VisionsLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public VisionsLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<Front_Vision> GetAllWithDeleted()
        {
            return _db.Front_Vision.OrderBy(p => p.IsDeleted).ToList();
        }
        public List<Front_Vision> GetAll()
        {
            return _db.Front_Vision.Where(p => p.IsDeleted != true).ToList();
        }
        public Front_Vision Get(int id)
        {
            return _db.Front_Vision.Find(id);
        }
        private Front_Vision Save(Front_Vision vision)
        {
            try
            {
                _db.SaveChanges();
                vision.OperationStatus = "Succeded";
                return vision;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        vision.OperationStatus = "NameArMustBeUnique";
                        return vision;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        vision.OperationStatus = "NameEnMustBeUnique";
                        return vision;
                    }
                }
                throw;
            }
        }
        public Front_Vision Insert(Front_Vision postedVision)
        {
            var language = new Front_Vision()
            {
                DisplayValue = postedVision.DisplayValue,
                DisplayValueDesc = postedVision.DisplayValueDesc,
                Image= postedVision.Image,
                IsDeleted = postedVision.IsDeleted,
                Show = Parameters.Show, 
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId, 
            };
            _db.Front_Vision.Add(language);
            return Save(language);
        }
        public Front_Vision Edit(Front_Vision postedLanguage)
        {
            Front_Vision language = Get(postedLanguage.Id);
            language.DisplayValue = postedLanguage.DisplayValue;
            language.DisplayValueDesc = postedLanguage.DisplayValueDesc;
            language.Image = postedLanguage.Image; 
            language.IsDeleted = postedLanguage.IsDeleted;
            language.Show = postedLanguage.Show; 
            language.LastModificationTime = Parameters.CurrentDateTime;
            language.LastModifierUserId = Parameters.UserId;
            return Save(language);
        }
        public Front_Vision Delete(Front_Vision postedLanguage)
        {
            Front_Vision language = Get(postedLanguage.Id);
            if (_db.Front_Vision.Any(p => p.Id == postedLanguage.Id && p.IsDeleted != true))
            {
                  //  language.OperationStatus = "HasRelationship";
                return language;
            }

            language.IsDeleted = true;
            language.CreationTime = Parameters.CurrentDateTime;
            language.CreatorUserId = Parameters.UserId;
            return Save(language);
        }

    }
}
