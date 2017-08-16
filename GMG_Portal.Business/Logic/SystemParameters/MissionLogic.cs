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
        public List<Front_Mission> GetAllWithDeleted(string langId)
        {
            var returnValue = new List<Front_Mission>();
            if (langId == Parameters.DefaultLang)
                returnValue = _db.Front_Mission.OrderBy(p => p.IsDeleted).ToList();
            else
            {
                var getList = _db.Front_Mission_Translate.Where(p => p.langId == langId).ToList();
                foreach (var frontMissionTranslate in getList)
                {
                    returnValue.Add(new Front_Mission
                    {
                        Id = frontMissionTranslate.Id,
                        DisplayValue = frontMissionTranslate.DisplayValue,
                        DisplayValueDesc = frontMissionTranslate.DisplayValueDesc,
                        Image = frontMissionTranslate.Image
                    });
                }
            }

            return returnValue;

        }
        public List<Front_Mission> GetAll(string langId)
        {
            var returnValue = new List<Front_Mission>();
            if (langId == Parameters.DefaultLang)
                returnValue = _db.Front_Mission.Where(p => p.IsDeleted != true).ToList();
            else
            {
                var getList = _db.Front_Mission_Translate.Where(p => p.IsDeleted != true && p.langId == langId).ToList();
                foreach (var frontMissionTranslate in getList)
                {
                    returnValue.Add(new Front_Mission
                    {
                        Id = frontMissionTranslate.Id,
                        DisplayValue = frontMissionTranslate.DisplayValue,
                        DisplayValueDesc = frontMissionTranslate.DisplayValueDesc,
                        Image = frontMissionTranslate.Image
                    });
                }
            }

            return returnValue;
        }
        public Front_Mission Get(int id, string langId)
        {
            return _db.Front_Mission.Find(id);
        }
        public Front_Mission_Translate GetByLang(int id, string langId)
        {
            return _db.Front_Mission_Translate.FirstOrDefault(p => p.Id != id && p.langId == langId);
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
        public Front_Mission Edit(Front_Mission postedMission, string langId)
        {
            var returnValue = new Front_Mission();
            if (langId == Parameters.DefaultLang)
            {
                Front_Mission mission = Get(postedMission.Id, langId);
                mission.DisplayValue = postedMission.DisplayValue;
                mission.DisplayValueDesc = postedMission.DisplayValueDesc;
                mission.Image = postedMission.Image;
                mission.IsDeleted = postedMission.IsDeleted;
                mission.Show = postedMission.Show;
                mission.LastModificationTime = Parameters.CurrentDateTime;
                mission.LastModifierUserId = Parameters.UserId;
                return Save(mission);
            }
            else
            {
                Front_Mission_Translate mission = GetByLang(postedMission.Id, langId);
                mission.DisplayValue = postedMission.DisplayValue;
                mission.DisplayValueDesc = postedMission.DisplayValueDesc;
                mission.Image = postedMission.Image;
                mission.IsDeleted = postedMission.IsDeleted;
                mission.Show = postedMission.Show;
                mission.LastModificationTime = Parameters.CurrentDateTime;
                mission.LastModifierUserId = Parameters.UserId;
                _db.SaveChanges();

                returnValue.DisplayValue = mission.DisplayValue;
                returnValue.DisplayValueDesc = mission.DisplayValueDesc;
                returnValue.Image = mission.Image;
                returnValue.IsDeleted = mission.IsDeleted;
                returnValue.Show = mission.Show;
                returnValue.LastModificationTime = Parameters.CurrentDateTime;
                returnValue.LastModifierUserId = Parameters.UserId;
                return returnValue;
            }

        }


    }
}
