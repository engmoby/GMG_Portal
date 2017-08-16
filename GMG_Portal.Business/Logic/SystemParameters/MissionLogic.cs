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
        public  Front_Mission  GetAllWithDeleted()
        {
            return _db.Front_Mission.OrderBy(p => p.IsDeleted).FirstOrDefault();
        }
        public  Front_Mission GetAll()
        {
            return _db.Front_Mission.FirstOrDefault(p => p.IsDeleted != true);
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
        public Front_Mission Insert(Front_Mission postedMission)
        {
            var obj = new Front_Mission()
            {
                DisplayValue = postedMission.DisplayValue,
                DisplayValueDesc = postedMission.DisplayValueDesc,
                Image = postedMission.Image,
                IsDeleted = postedMission.IsDeleted,
                Show = Parameters.Show,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.Front_Mission.Add(obj);
            return Save(obj);
        }
        public Front_Mission Edit(Front_Mission postedMission)
        {
            Front_Mission obj = Get(postedMission.Id);
            obj.DisplayValue = postedMission.DisplayValue;
            obj.DisplayValueDesc = postedMission.DisplayValueDesc;
            obj.Image = postedMission.Image;
            obj.IsDeleted = postedMission.IsDeleted;
            obj.Show = postedMission.Show;
            obj.LastModificationTime = Parameters.CurrentDateTime;
            obj.LastModifierUserId = Parameters.UserId;
            return Save(obj);
        }
        public Front_Mission Delete(Front_Mission postedMission)
        {
            Front_Mission obj = Get(postedMission.Id);
            if (_db.Front_Mission.Any(p => p.Id == postedMission.Id && p.IsDeleted != true))
            {
                //  obj.OperationStatus = "HasRelationship";
                return obj;
            }

            obj.IsDeleted = true;
            obj.CreationTime = Parameters.CurrentDateTime;
            obj.CreatorUserId = Parameters.UserId;
            return Save(obj);
        }


    }
}
