using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Admin : BaseEntity
    {
        /*public Admin(){}
        public Admin(Admin admin)
        {
            this.Username = admin.Username;
            this.Password = admin.Password;
        }*/
        [DisplayName("User Name")]
        [Required(ErrorMessage = "Username required!")]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password required!")]
        public string Password { get; set; }
    }
}
