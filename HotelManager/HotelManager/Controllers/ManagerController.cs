using Data;
using Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace HotelManager.Controllers
{
    public class ManagerController : Controller
    {
        private readonly int allinclusivePrice = 150;
        private readonly int breakfastPrice = 15;
        // GET: Manager
        public ActionResult ManagerSearchRoom(FormCollection collection)
        {
            List<Room> rooms = new List<Room>();
            using (HmDbContext context = new HmDbContext())
            {
                rooms = new List<Room>(context.Rooms.OrderBy(x => x.Occupation).ToList());
            }
            return View(rooms);
        }
        public ActionResult ManagerSearchReservation()
        {
            List<Reservation> reservations = new List<Reservation>();
            using (HmDbContext context = new HmDbContext())
            {
                foreach (var reservation in context.Reservations.Where(x => x.DischargeDate > System.DateTime.Now))
                {
                    context.Reservations.Remove(reservation);
                }
                context.SaveChanges();
                reservations = new List<Reservation>(context.Reservations.OrderBy(x => x.DischargeDate).ToList());
            }
            return View(reservations);
        }

        //[Route("Manager/ManagerNewReservationSave/{model}/{isForSave}")]
        public ActionResult ManagerNewReservationSave(Reservation reservation)
        {
            bool isClientForAddition = false;
            decimal total = 0;
            int occupantsCount = 0;
            using (HmDbContext context = new HmDbContext())
            {
                if (!context.Rooms.Where(x => x.Number == reservation.RoomNumber).Any())
                {
                    return RedirectToAction("ManagerSearchReservation");
                }
                if (context.Rooms.Where(x => x.Number == reservation.RoomNumber).Where(x => x.Occupation == true).Any())
                {
                    return RedirectToAction("ManagerSearchReservation");
                }
                if (context.Reservations.Where(x => x.RoomNumber.Equals(reservation.RoomNumber)).Count() > 1)
                {
                    return RedirectToAction("ManagerSearchReservation");
                }
                PropertyInfo[] properties = typeof(Client).GetProperties();
                List<Client> occ = new List<Client>();
                foreach (Client occupant in reservation.AccomodatedClients)
                {
                    if (occupant == null) continue;
                    /*bool flag = false;
                    foreach (PropertyInfo property in properties)
                    {
                        if (property.GetValue(occupant).Equals(null) || property.GetValue(occupant).Equals(""))
                        { flag = true; break; }
                    }*/
                    if (occupant.Email == null || occupant.Email == "") continue;
                    if (occupant.Number == null || occupant.Number == "") continue;
                    if (occupant.FirstName == null || occupant.FirstName == "") continue;
                    if (occupant.SureName == null || occupant.SureName == "") continue;

                    //if (flag) continue;
                    occ.Add(occupant);
                    occupantsCount++;
                    if (occupant.Adult)
                    {
                        total += context.Rooms.First(x => x.Number == reservation.RoomNumber).AdultBedPrice;
                    }
                    else
                    {
                        total += context.Rooms.First(x => x.Number == reservation.RoomNumber).ChildBedPrice;
                    }
                }
                reservation.AccomodatedClients = occ;
                if (occupantsCount > context.Rooms.First(x => x.Number == reservation.RoomNumber).Capacity) return View("ManagerAddNewReservation");
                if (reservation.Allinclusive) total += allinclusivePrice;
                if (reservation.BreakFastIncluded) total += (breakfastPrice * occupantsCount);
                total *= decimal.Parse((reservation.DischargeDate - reservation.AccomodationDate).TotalDays.ToString());
                reservation.TotalPrice = total;
                /*if (isForSave)
                {*/
                context.Reservations.Add(reservation);
                context.Rooms.First(x => x.Number == reservation.RoomNumber).Occupation = true;
                context.SaveChanges();
                if (!context.Clients.Where(x => x.Email.ToLower().Equals(reservation.User.ToLower())).Any())
                {
                    isClientForAddition = true;
                }
                /*}*/
            }
            /*if (isForSave)
            {*/
            if (isClientForAddition)
            {
                Client client = new Client();
                if (reservation.User.Contains("@"))
                {
                    client.Email = reservation.User;
                }
                else
                {
                    List<string> str = reservation.User.Split(' ').ToList();
                    if (str.Count == 2)
                    {
                        client.FirstName = str.First();
                        client.SureName = str.Last();
                    }
                }
                return View("ManagerAddNewClient", client);
            }
            /*}*/
            return RedirectToAction("ManagerSearchReservation");
        }
        /*public ActionResult ManagerAddNewClient(Client client)
        {
            return View(client);
        }*/
        public ActionResult ManagerSearchClient()
        {
            List<Client> clients = new List<Client>();
            using (HmDbContext context = new HmDbContext())
            {
                clients = new List<Client>(context.Clients.OrderBy(x => x.FirstName).ToList());
            }
            return View(clients);
        }
        public ActionResult ManagerAddNewClientSave(Client client)
        {
            using (HmDbContext context = new HmDbContext())
            {
                context.Clients.Add(client);
                context.SaveChanges();
            }
            return RedirectToAction("ManagerSearchReservation");
        }
        public ActionResult ManagerNewReservation()
        {
            return View();
        }
    }
}