using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Room : BaseEntity
    {
        public int Capacity { get; set; }
        public enum Type { TwoSingleBeds, Apartment, DoubleBed, Penthouse, Мaisonette }
        public bool Occupation { get; set; }
        public decimal AdultBedPrice { get; set; }
        public decimal ChildBedPrice { get; set; }
        public string Number { get; set; }
    }
}
