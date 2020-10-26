using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Client : BaseEntity
    {
        public string FirstName { get; set; }
        public string SureName { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public bool Adult { get; set; }

    }
}
