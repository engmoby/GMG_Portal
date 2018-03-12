using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<Career> GetAllWithDeleted()
        {
            var returnList = new List<Career>(); 
            var getCareersList = _db.Careers.ToList();
            foreach (var caeerCareerse in getCareersList)
            {

                var getCareerForms = _db.SystemParameters_CareerForm.Where(p => p.CareerId== caeerCareerse.Id).ToList();
           
                returnList.Add(new Career
                {
                    Id = caeerCareerse.Id,
                    Title = caeerCareerse.Title,
                    Description = caeerCareerse.Description, 
                    Experience = caeerCareerse.Experience, 
                    EducationLevel = caeerCareerse.EducationLevel,  
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
        public List<Career> GetAll()
        {
            return _db.Careers.Where(p => p.IsDeleted != true).ToList();
        }
        public Career Get(int id)
        {
            return _db.Careers.Find(id);
        }
        private Career Save(Career obj)
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
        public Career Insert(Career postedCareer)
        { 
            var obj = new Career
            {
                Title = postedCareer.Title,
                Description = postedCareer.Description, 
                CareerLevel= postedCareer.CareerLevel,
                JobType = postedCareer.JobType,
                EducationLevel = postedCareer.EducationLevel,
                Vacancies = postedCareer.Vacancies,
                Experience = postedCareer.Experience,
                Image = postedCareer.Image,
                IsDeleted = postedCareer.IsDeleted, 
                SalaryAverage = postedCareer.SalaryAverage,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId, 
            };
            _db.Careers.Add(obj);
            return Save(obj);
        }
        public Career Edit(Career postedCareer)
        {
            var obj = Get(postedCareer.Id);
            obj.Title = postedCareer.Title;
            obj.Description = postedCareer.Description; 
            obj.CareerLevel = postedCareer.CareerLevel;
            obj.JobType = postedCareer.JobType;
            obj.EducationLevel = postedCareer.EducationLevel;
            obj.Vacancies = postedCareer.Vacancies;
            obj.Experience = postedCareer.Experience;
            obj.Image = postedCareer.Image;
            obj.SalaryAverage= postedCareer.SalaryAverage; 
            obj.IsDeleted = postedCareer.IsDeleted; 
            obj.LastModificationTime = Parameters.CurrentDateTime;
            obj.LastModifierUserId = Parameters.UserId;
            return Save(obj);
        }
        public Career Delete(Career postedCareer)
        {
            Career obj = Get(postedCareer.Id);
            //if (_db.SystemParameters_CareerForm.Any(p => p.CareerId == postedCareer.Id))
            //{
            //    obj.OperationStatus = "HasRelationship";
            //    return obj;
            //}

            obj.IsDeleted = true;
            obj.CreationTime = Parameters.CurrentDateTime;
            obj.CreatorUserId = Parameters.UserId; 
            return Save(obj);
        }

    }
}
