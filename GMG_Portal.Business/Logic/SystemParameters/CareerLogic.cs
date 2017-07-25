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
        private SystemParameters_Careers Save(SystemParameters_Careers career)
        {
            try
            {
                _db.SaveChanges();
                career.OperationStatus = "Succeded";
                return career;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        career.OperationStatus = "NameArMustBeUnique";
                        return career;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        career.OperationStatus = "NameEnMustBeUnique";
                        return career;
                    }
                }
                throw;
            }
        }
        public SystemParameters_Careers Insert(SystemParameters_Careers postedCareer)
        { 
            var career = new SystemParameters_Careers
            {
                DisplayValue = postedCareer.DisplayValue,
                DisplayValueDesc = postedCareer.DisplayValueDesc,
                DisplayValueRequirements= postedCareer.DisplayValueRequirements,
                CareerLevel= postedCareer.CareerLevel,
                JobType = postedCareer.JobType,
                EducationLevel = postedCareer.EducationLevel,
                Vacancies = postedCareer.Vacancies,
                Image= postedCareer.Image,
                IsDeleted = postedCareer.IsDeleted,
                Show = Parameters.Show, 
                SalaryAverage = postedCareer.SalaryAverage,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId, 
            };
            _db.SystemParameters_Careers.Add(career);
            return Save(career);
        }
        public SystemParameters_Careers Edit(SystemParameters_Careers postedCareer)
        {
            var career = Get(postedCareer.Id);
            career.DisplayValue = postedCareer.DisplayValue;
            career.DisplayValueDesc = postedCareer.DisplayValueDesc;
            career.DisplayValueRequirements = postedCareer.DisplayValueRequirements;
            career.CareerLevel = postedCareer.CareerLevel;
            career.JobType = postedCareer.JobType;
            career.EducationLevel = postedCareer.EducationLevel;
            career.Vacancies = postedCareer.Vacancies;
            career.Experience = postedCareer.Experience;
            career.Image = postedCareer.Image;
            career.SalaryAverage= postedCareer.SalaryAverage; 
            career.IsDeleted = postedCareer.IsDeleted;
           // career.Show = postedCareer.Show; 
            career.LastModificationTime = Parameters.CurrentDateTime;
            career.LastModifierUserId = Parameters.UserId;
            return Save(career);
        }
        public SystemParameters_Careers Delete(SystemParameters_Careers postedCareer)
        {
            SystemParameters_Careers career = Get(postedCareer.Id);
            if (_db.SystemParameters_CareerForm.Any(p => p.CareerId == postedCareer.Id))
            {
                career.OperationStatus = "HasRelationship";
                return career;
            }

            career.IsDeleted = true;
            career.CreationTime = Parameters.CurrentDateTime;
            career.CreatorUserId = Parameters.UserId;
            return Save(career);
        }

    }
}
