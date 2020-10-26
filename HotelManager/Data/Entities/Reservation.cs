using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Reservation : BaseEntity
    {
        public Reservation()
        {
            this.AccomodatedClients = new List<Client>(10);
        }
        public string RoomNumber { get; set; }
        public string User { get; set; }
        public List<Client> AccomodatedClients { get; set; }
        public DateTime AccomodationDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public bool BreakFastIncluded { get; set; }
        public bool Allinclusive { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
