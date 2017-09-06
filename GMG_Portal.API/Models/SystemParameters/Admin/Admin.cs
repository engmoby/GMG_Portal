using System;

namespace GMG_Portal.API.Models.SystemParameters.Admin
{
    public class Admin
    {
        public int Id { get; set; }
        public Guid? SGuid { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public bool? Show { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string PassWd { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public string Department { get; set; }
        public string Icon { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? CreatorUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int? DeleterUserId { get; set; }
        public string DisplayFront { get; set; }
        public string OperationStatus { get; set; }
    }
}