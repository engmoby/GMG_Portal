using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class LanguagesLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public LanguagesLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<Systemparameters_Languages> GetAllWithDeleted()
        {
            return _db.Systemparameters_Languages.OrderBy(p => p.IsDeleted).ToList();
        }
        public List<Systemparameters_Languages> GetAll()
        {
            return _db.Systemparameters_Languages.Where(p => p.IsDeleted != true).ToList();
        }
        public Systemparameters_Languages Get(int id)
        {
            return _db.Systemparameters_Languages.Find(id);
        }
        private Systemparameters_Languages Save(Systemparameters_Languages languages)
        {
            try
            {
                _db.SaveChanges();
                languages.OperationStatus = "Succeded";
                return languages;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        languages.OperationStatus = "NameArMustBeUnique";
                        return languages;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        languages.OperationStatus = "NameEnMustBeUnique";
                        return languages;
                    }
                }
                throw;
            }
        }
        public Systemparameters_Languages Insert(Systemparameters_Languages postedLanguage)
        {
            var language = new Systemparameters_Languages()
            {
                Name = postedLanguage.Name,
                DisplayName = postedLanguage.DisplayName,
                DisplayFront = postedLanguage.DisplayFront,
                IsDeleted = postedLanguage.IsDeleted,
                Show = Parameters.Show,
                Icon = postedLanguage.Icon,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId, 
            };
            _db.Systemparameters_Languages.Add(language);
            return Save(language);
        }
        public Systemparameters_Languages Edit(Systemparameters_Languages postedLanguage)
        {
            Systemparameters_Languages language = Get(postedLanguage.Id);
            language.Name = postedLanguage.Name;
            language.DisplayName = postedLanguage.DisplayName;
            language.DisplayFront = postedLanguage.DisplayFront;
            language.IsDeleted = postedLanguage.IsDeleted;
            language.Show = postedLanguage.Show;
            language.Icon = postedLanguage.Icon;
            language.LastModificationTime = Parameters.CurrentDateTime;
            language.LastModifierUserId = Parameters.UserId;
            return Save(language);
        }
        public Systemparameters_Languages Delete(Systemparameters_Languages postedLanguage)
        {
            Systemparameters_Languages language = Get(postedLanguage.Id);
            if (_db.Systemparameters_Languages.Any(p => p.Id == postedLanguage.Id && p.IsDeleted != true))
            {
                language.OperationStatus = "HasRelationship";
                return language;
            }

            language.IsDeleted = true;
            language.CreationTime = Parameters.CurrentDateTime;
            language.CreatorUserId = Parameters.UserId;
            return Save(language);
        }

    }
}
