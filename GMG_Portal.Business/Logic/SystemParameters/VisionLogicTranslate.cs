using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class VisionLogicTranslate
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public VisionLogicTranslate()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<Front_Vision_Translate> GetAllWithDeleted(string langId)
        {
            return _db.Front_Vision_Translate.OrderBy(p => p.IsDeleted && p.langId == langId).ToList();
        }
        public Front_Vision_Translate GetAll(string langId)
        {
            return _db.Front_Vision_Translate.FirstOrDefault(p => p.IsDeleted != true && p.langId == langId);
        }
        public Front_Vision_Translate Get(int id, string langId)
        {
            return _db.Front_Vision_Translate.FirstOrDefault(x => x.Id == id && x.langId == langId);
        }
        private Front_Vision_Translate Save(Front_Vision_Translate obj)
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
        public Front_Vision_Translate Insert(Front_Vision_Translate postedVision)
        {

            var obj = new Front_Vision_Translate()
            {
                DisplayValue = postedVision.DisplayValue,
                DisplayValueDesc = postedVision.DisplayValueDesc,

                IsDeleted = postedVision.IsDeleted,
                Show = Parameters.Show,

                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
                langId = postedVision.langId
            };
            _db.Front_Vision_Translate.Add(obj);
            return Save(obj);
        }
        public Front_Vision_Translate Edit(Front_Vision_Translate postedVision)
        {
            Front_Vision_Translate obj = Get(postedVision.Id, postedVision.langId);
            obj.DisplayValue = postedVision.DisplayValue;
            obj.DisplayValueDesc = postedVision.DisplayValueDesc;

            obj.IsDeleted = postedVision.IsDeleted;
            obj.Show = postedVision.Show;
            obj.LastModificationTime = Parameters.CurrentDateTime;
            obj.LastModifierUserId = Parameters.UserId;
            return Save(obj);
        }
        public Front_Vision_Translate Delete(Front_Vision_Translate postedVision)
        {
            Front_Vision_Translate obj = Get(postedVision.Id, postedVision.langId);
            if (_db.Front_Vision_Translate.Any(p => p.Id == postedVision.Id && p.IsDeleted != true))
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
