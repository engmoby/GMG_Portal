using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMG_Portal.Data;
using Heloper;

namespace GMG_Portal.Business.Logic.SystemParameters
{
    public class AdminLogic
    {
        private readonly GMG_Portal_DBEntities1 _db;

        public AdminLogic()
        {
            _db = new GMG_Portal_DBEntities1();
        }
        public List<Systemparameters_Admin> GetAllWithDeleted()
        {
            return _db.Systemparameters_Admin.OrderBy(p => p.IsDeleted).ToList();
        }
        public Systemparameters_Admin GetAll()
        {
            return _db.Systemparameters_Admin.FirstOrDefault(p => p.IsDeleted != true);
        }
        public Systemparameters_Admin Get(int id)
        {
            return _db.Systemparameters_Admin.Find(id);
        }
        public Systemparameters_Admin Login(Systemparameters_Admin admin)
        {
            return _db.Systemparameters_Admin.FirstOrDefault(x => x.UserName == admin.UserName && x.PassWd == admin.PassWd && x.IsDeleted==false);

        }
        private Systemparameters_Admin Save(Systemparameters_Admin admin)
        {
            try
            {
                _db.SaveChanges();
                admin.OperationStatus = "Succeded";
                return admin;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.ToString().Contains("IX_Countries_Ar"))
                    {
                        admin.OperationStatus = "NameArMustBeUnique";
                        return admin;
                    }
                    else if (e.InnerException.ToString().Contains("IX_Countries_En"))
                    {
                        admin.OperationStatus = "NameEnMustBeUnique";
                        return admin;
                    }
                }
                throw;
            }
        }
        public Systemparameters_Admin Insert(Systemparameters_Admin postedAdmin)
        {

            var admin = new Systemparameters_Admin()
            {
                DisplayName = postedAdmin.DisplayName,
                UserName = postedAdmin.UserName,
                Email = postedAdmin.Email,
                PassWd = postedAdmin.PassWd,
                Phone = postedAdmin.Phone,
                Department = postedAdmin.Department,
                DisplayFront = postedAdmin.DisplayFront,
                DateOfBirth = postedAdmin.DateOfBirth,
                IsDeleted = postedAdmin.IsDeleted,
                Show = Parameters.Show,
                CreationTime = Parameters.CurrentDateTime,
                CreatorUserId = Parameters.UserId,
            };
            _db.Systemparameters_Admin.Add(admin);
            return Save(admin);
        }
        public Systemparameters_Admin Edit(Systemparameters_Admin postedAdmin)
        {
            Systemparameters_Admin admin = Get(postedAdmin.Id);
            admin.DisplayName = postedAdmin.DisplayName;
            admin.UserName = postedAdmin.UserName;
            admin.PassWd = postedAdmin.PassWd;
            admin.Phone = postedAdmin.Phone;
            admin.Email = postedAdmin.Email;
            admin.Department = postedAdmin.Department;
            admin.DisplayFront = postedAdmin.DisplayFront;
            admin.DateOfBirth = postedAdmin.DateOfBirth;
            admin.IsDeleted = postedAdmin.IsDeleted;
            admin.Show = postedAdmin.Show;
            admin.LastModificationTime = Parameters.CurrentDateTime;
            admin.LastModifierUserId = Parameters.UserId;
            return Save(admin);
        }
        public Systemparameters_Admin Delete(Systemparameters_Admin postedAdmin)
        {
            Systemparameters_Admin admin = Get(postedAdmin.Id);

            admin.IsDeleted = true;
            admin.CreationTime = Parameters.CurrentDateTime;
            admin.CreatorUserId = Parameters.UserId;
            return Save(admin);
        }

    }
}
