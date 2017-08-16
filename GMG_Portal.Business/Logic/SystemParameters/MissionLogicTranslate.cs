using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class MissionLogicTranslate
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public MissionLogicTranslate()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<Front_Mission_Translate> GetAllWithDeleted(string langId)
        {
            return _db.Front_Mission_Translate.OrderBy(p => p.IsDeleted && p.langId == langId).ToList();
        }
        public Front_Mission_Translate GetAll(string langId)
        {
            return _db.Front_Mission_Translate.FirstOrDefault(p => p.IsDeleted != true && p.langId == langId);
        }
        public Front_Mission_Translate Get(int id, string langId)
        {
            return _db.Front_Mission_Translate.FirstOrDefault(x => x.Id == id && x.langId == langId);
        }
        private Front_Mission_Translate Save(Front_Mission_Translate obj)
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
        public Front_Mission_Translate Insert(Front_Mission_Translate postedMission)
        {

            var obj = new Front_Mission_Translate()
            {
                DisplayValue = postedMission.DisplayValue,
                DisplayValueDesc = postedMission.DisplayValueDesc,

                IsDeleted = postedMission.IsDeleted,
                Show = Parameters.Show,

                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
                langId = postedMission.langId
            };
            _db.Front_Mission_Translate.Add(obj);
            return Save(obj);
        }
        public Front_Mission_Translate Edit(Front_Mission_Translate postedMission)
        {
            Front_Mission_Translate obj = Get(postedMission.Id, postedMission.langId);
            obj.DisplayValue = postedMission.DisplayValue;
            obj.DisplayValueDesc = postedMission.DisplayValueDesc;

            obj.IsDeleted = postedMission.IsDeleted;
            obj.Show = postedMission.Show;
            obj.LastModificationTime = Parameters.CurrentDateTime;
            obj.LastModifierUserId = Parameters.UserId;
            return Save(obj);
        }
        public Front_Mission_Translate Delete(Front_Mission_Translate postedMission)
        {
            Front_Mission_Translate obj = Get(postedMission.Id, postedMission.langId);
            if (_db.Front_Mission_Translate.Any(p => p.Id == postedMission.Id && p.IsDeleted != true))
            {
                //  About.OperationStatus = "HasRelationship";
                return obj;
            }

            obj.IsDeleted = true;
            obj.CreationTime = Parameters.CurrentDateTime;
            obj.CreatorUserId = Parameters.UserId;
            return Save(obj);
        }



    }
}
