using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class CareerLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public CareerLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<SystemParameters_Careers> GetAllWithDeleted()
        {
            var returnList = new List<SystemParameters_Careers>(); 
            var getCareersList = _db.SystemParameters_Careers.ToList();
            foreach (var caeerCareerse in getCareersList)
            {

                var getCareerForms = _db.SystemParameters_CareerForm.Where(p => p.CareerId== caeerCareerse.Id).ToList();
           
                returnList.Add(new SystemParameters_Careers
                {
                    Id = caeerCareerse.Id,
                    DisplayValue = caeerCareerse.DisplayValue,
                    DisplayValueDesc = caeerCareerse.DisplayValueDesc, 
                    Experience = caeerCareerse.Experience, 
                    EducationLevel = caeerCareerse.EducationLevel, 
                    DisplayValueRequirements = caeerCareerse.DisplayValueRequirements, 
                    SalaryAverage = caeerCareerse.SalaryAverage, 
                    CareerLevel = caeerCareerse.CareerLevel, 
                    Vacancies = caeerCareerse.Vacancies,  
                    JobType = caeerCareerse.JobType,
                    IsDeleted = caeerCareerse.IsDeleted,
                    ApplyCount = getCareerForms.Count
                });
            }

            return returnList;
        }
        public List<SystemParameters_Careers> GetAll()
        {
            return _db.SystemParameters_Careers.Where(p => p.IsDeleted != true).ToList();
        }
        public SystemParameters_Careers Get(int id)
        {
            return _db.SystemParameters_Careers.Find(id);
        }
        private SystemParameters_Careers Save(SystemParameters_Careers obj)
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
        public SystemParameters_Careers Insert(SystemParameters_Careers postedCareer)
        { 
            var obj = new SystemParameters_Careers
            {
                DisplayValue = postedCareer.DisplayValue,
                DisplayValueDesc = postedCareer.DisplayValueDesc,
                DisplayValueRequirements= postedCareer.DisplayValueRequirements,
                CareerLevel= postedCareer.CareerLevel,
                JobType = postedCareer.JobType,
                EducationLevel = postedCareer.EducationLevel,
                Vacancies = postedCareer.Vacancies,
                Experience = postedCareer.Experience,
                Image = postedCareer.Image,
                IsDeleted = postedCareer.IsDeleted,
                Show = Parameters.Show, 
                SalaryAverage = postedCareer.SalaryAverage,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId, 
            };
            _db.SystemParameters_Careers.Add(obj);
            return Save(obj);
        }
        public SystemParameters_Careers Edit(SystemParameters_Careers postedCareer)
        {
            var obj = Get(postedCareer.Id);
            obj.DisplayValue = postedCareer.DisplayValue;
            obj.DisplayValueDesc = postedCareer.DisplayValueDesc;
            obj.DisplayValueRequirements = postedCareer.DisplayValueRequirements;
            obj.CareerLevel = postedCareer.CareerLevel;
            obj.JobType = postedCareer.JobType;
            obj.EducationLevel = postedCareer.EducationLevel;
            obj.Vacancies = postedCareer.Vacancies;
            obj.Experience = postedCareer.Experience;
            obj.Image = postedCareer.Image;
            obj.SalaryAverage= postedCareer.SalaryAverage; 
            obj.IsDeleted = postedCareer.IsDeleted;
           // obj.Show = postedCareer.Show; 
            obj.LastModificationTime = Parameters.CurrentDateTime;
            obj.LastModifierUserId = Parameters.UserId;
            return Save(obj);
        }
        public SystemParameters_Careers Delete(SystemParameters_Careers postedCareer)
        {
            SystemParameters_Careers obj = Get(postedCareer.Id);
            if (_db.SystemParameters_CareerForm.Any(p => p.CareerId == postedCareer.Id))
            {
                obj.OperationStatus = "HasRelationship";
                return obj;
            }

            obj.IsDeleted = true;
            obj.CreationTime = Parameters.CurrentDateTime;
            obj.CreatorUserId = Parameters.UserId;
            return Save(obj);
        }

    }
}
