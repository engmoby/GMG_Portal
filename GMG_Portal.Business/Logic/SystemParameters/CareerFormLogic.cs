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
        public IEnumerable<SystemParameters_CareerForm> GetAllWithSeenHomePage()
        {
            var returnList = new List<SystemParameters_CareerForm>();
            var getCareerFormList = _db.SystemParameters_CareerForm.ToList().Take(5);
            foreach (var caeerCareerse in getCareerFormList)
            {
                var getCareerForms = _db.SystemParameters_Careers.FirstOrDefault(p => p.Id == caeerCareerse.CareerId);

                if (getCareerForms != null)
                    returnList.Add(new SystemParameters_CareerForm
                    {
                        Id = caeerCareerse.Id,
                        CareerId = caeerCareerse.CareerId,
                        CareerTitle = getCareerForms.DisplayValue,
                        FirstName = caeerCareerse.FirstName,
                        LastName = caeerCareerse.LastName,
                        Email = caeerCareerse.Email,
                        PhoneNo = caeerCareerse.PhoneNo,
                        Message = caeerCareerse.Message,
                        Attach = caeerCareerse.Attach,
                        Seen = caeerCareerse.Seen,
                        SeenBy = caeerCareerse.SeenBy,
                        SeenDate = caeerCareerse.SeenDate,
                        CreationTime = caeerCareerse.CreationTime
                    });
            }

            return returnList;
        }
        public List<SystemParameters_CareerForm> GetAllWithSeen()
        { 
            var returnList = new List<SystemParameters_CareerForm>();
            var getCareerFormList = _db.SystemParameters_CareerForm.ToList();
            foreach (var caeerCareerse in getCareerFormList)
            {
                var getCareerForms = _db.SystemParameters_Careers.FirstOrDefault(p => p.Id == caeerCareerse.CareerId);

                if (getCareerForms != null)
                    returnList.Add(new SystemParameters_CareerForm
                    {
                        Id = caeerCareerse.Id,
                        CareerId = caeerCareerse.CareerId,
                        CareerTitle = getCareerForms.DisplayValue,
                        FirstName = caeerCareerse.FirstName,
                        LastName = caeerCareerse.LastName,
                        Email = caeerCareerse.Email,
                        PhoneNo = caeerCareerse.PhoneNo,
                        Message = caeerCareerse.Message,
                        Attach = caeerCareerse.Attach,
                        Seen = caeerCareerse.Seen,
                        SeenBy = caeerCareerse.SeenBy,
                        SeenDate = caeerCareerse.SeenDate,
                        CreationTime = caeerCareerse.CreationTime
                    });
            }

            return returnList; 
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
                CareerId = postedCareerForm.CareerId,
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
