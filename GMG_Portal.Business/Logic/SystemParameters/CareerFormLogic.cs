using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class CareerFormLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public CareerFormLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<SystemParameters_CareerForm> GetAllWithSeen()
        {
            return _db.SystemParameters_CareerForm.OrderBy(p => p.SeenBy).ToList();
        }
        public List<SystemParameters_CareerForm> GetAll()
        {
            return _db.SystemParameters_CareerForm.Where(p => p.Seen != true).ToList();
        }
        public SystemParameters_CareerForm Get(int id)
        {
            return _db.SystemParameters_CareerForm.Find(id);
        }
        private SystemParameters_CareerForm Save(SystemParameters_CareerForm careerForm)
        {
            try
            {
                _db.SaveChanges();
                careerForm.OperationStatus = "Succeded";
                return careerForm;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        careerForm.OperationStatus = "NameArMustBeUnique";
                        return careerForm;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        careerForm.OperationStatus = "NameEnMustBeUnique";
                        return careerForm;
                    }
                }
                throw;
            }
        }
        public SystemParameters_CareerForm Insert(SystemParameters_CareerForm postedCareerForm)
        {

            var careerForm = new SystemParameters_CareerForm()
            {
                FirstName = postedCareerForm.FirstName,
                LastName = postedCareerForm.LastName,
                Email = postedCareerForm.Email,
                PhoneNo = postedCareerForm.PhoneNo,
                Message = postedCareerForm.Message,
                Attach = postedCareerForm.Attach,
                CreationTime = Parameters.CurrentDateTime
            };
            _db.SystemParameters_CareerForm.Add(careerForm);
            return Save(careerForm);
        }
        public SystemParameters_CareerForm Edit(SystemParameters_CareerForm postedCareerForm)
        {
            SystemParameters_CareerForm careerForm = Get(postedCareerForm.Id);
            careerForm.Seen = postedCareerForm.Seen;
            careerForm.SeenDate = Parameters.CurrentDateTime;
            careerForm.SeenBy = Parameters.UserId;
            return Save(careerForm);
        }


    }
}
