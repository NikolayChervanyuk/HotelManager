using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace Data.Entities
{
    [Table("UserTable")]
    [BindRequired]
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string SureName { get; set; }
        public string IDnum { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime Assignment { get; set; }
        public bool IsActive { get; set; }
    }
}
