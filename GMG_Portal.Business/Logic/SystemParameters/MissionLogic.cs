using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class MissionsLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public MissionsLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<Front_Mission> GetAllWithDeleted()
        {
            return _db.Front_Mission.OrderBy(p => p.IsDeleted).ToList();
        }
        public List<Front_Mission> GetAll()
        {
            return _db.Front_Mission.Where(p => p.IsDeleted != true).ToList();
        }
        public Front_Mission Get(int id)
        {
            return _db.Front_Mission.Find(id);
        }
        private Front_Mission Save(Front_Mission mission)
        {
            try
            {
                _db.SaveChanges();
                mission.OperationStatus = "Succeded";
                return mission;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        mission.OperationStatus = "NameArMustBeUnique";
                        return mission;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        mission.OperationStatus = "NameEnMustBeUnique";
                        return mission;
                    }
                }
                throw;
            }
        }
        public Front_Mission Insert(Front_Mission postedmission)
        {
            var language = new Front_Mission()
            {
                DisplayValue = postedmission.DisplayValue,
                DisplayValueDesc = postedmission.DisplayValueDesc,
                Image= postedmission.Image,
                IsDeleted = postedmission.IsDeleted,
                Show = Parameters.Show, 
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId, 
            };
            _db.Front_Mission.Add(language);
            return Save(language);
        }
        public Front_Mission Edit(Front_Mission postedLanguage)
        {
            Front_Mission language = Get(postedLanguage.Id);
            language.DisplayValue = postedLanguage.DisplayValue;
            language.DisplayValueDesc = postedLanguage.DisplayValueDesc;
            language.Image = postedLanguage.Image; 
            language.IsDeleted = postedLanguage.IsDeleted;
            language.Show = postedLanguage.Show; 
            language.LastModificationTime = Parameters.CurrentDateTime;
            language.LastModifierUserId = Parameters.UserId;
            return Save(language);
        }
        public Front_Mission Delete(Front_Mission postedLanguage)
        {
            Front_Mission language = Get(postedLanguage.Id);
            if (_db.Front_Mission.Any(p => p.Id == postedLanguage.Id && p.IsDeleted != true))
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
