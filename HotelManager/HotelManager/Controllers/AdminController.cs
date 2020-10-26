using Data;
using Data.Entities;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Globalization;
using System.Reflection;

namespace HotelManager.Controllers
{
    public class AdminController : Controller
    {
        private IUserRepository userRepository;
        public AdminController()
        {
            userRepository = new UserRepository(new HmDbContext());
        }
        [HttpGet]
        [Route("Controllers/Admin")]
        public List<User> GetUsers()
        {
            return userRepository.GetUsers().ToList();
        }

        public ActionResult AdminSearch(FormCollection collection)
        {

            List<User> users = new List<User>();
            using (HmDbContext context = new HmDbContext())
            {
                string filterString = Request.Form["textfilter"];
                if (filterString != null)
                {
                    filterString = filterString.ToLower();
                    switch (Request.Form["filters"])
                    {
                        case "username":
                            users = new List<User>(context.Users.Where(x => x.Username.ToLower().Contains(filterString)).ToList());
                            break;
                        case "name":
                            users = new List<User>(context.Users.Where(x => x.Name.ToLower().Contains(filterString)).ToList());
                            break;
                        case "midname":
                            users = new List<User>(context.Users.Where(x => x.MiddleName.ToLower().Contains(filterString)).ToList());
                            break;
                        case "surename":
                            users = new List<User>(context.Users.Where(x => x.SureName.ToLower().Contains(filterString)).ToList());
                            break;
                        case "email":
                            users = new List<User>(context.Users.Where(x => x.Email.ToLower().Contains(filterString)).ToList());
                            break;
                        default:
                            users = new List<User>(context.Users.OrderBy(x => x.Name).ToList());
                            break;
                    }
                }
                else { users = new List<User>(context.Users.OrderBy(x => x.Name).ToList()); }
            }
            return View(users);
        }
        public ActionResult AdminCreate(FormCollection collection)
        {
            if (Request.Form.Count == 0) return View();
            foreach (string form in Request.Form)
                if (Request.Form[form].Equals(null) || Request.Form[form].ToString() == "") return View();

            User user = new User();
            user.Username = Request.Form["username"];
            user.Password = Request.Form["password"];
            user.Name = Request.Form["name"];
            user.MiddleName = Request.Form["midname"];
            user.SureName = Request.Form["surename"];
            user.IDnum = Request.Form["idnum"];
            user.PhoneNumber = Request.Form["phonenumber"];
            user.Email = Request.Form["email"];
            user.IsActive = true;
            user.Assignment = DateTime.Now;
            using (HmDbContext context = new HmDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            return View();
        }

        public ActionResult AdminEdit(User user)
        {

            /*int i = 0;
            PropertyInfo[] properties = typeof(User).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                property.SetValue(user, collection.GetValues(i++));
            }*/

            using (HmDbContext context = new HmDbContext())
            {
                var contexUser = (from users in context.Users
                                  where users.ID == user.ID
                                  select users).FirstOrDefault();
                contexUser.Username = user.Username;
                contexUser.Password = user.Password;
                contexUser.Name = user.Name;
                contexUser.MiddleName = user.MiddleName;
                contexUser.SureName = user.SureName;
                contexUser.IDnum = user.IDnum;
                contexUser.IsActive = user.IsActive;
                contexUser.PhoneNumber = user.PhoneNumber;
                contexUser.Email = user.Email;
                //contextUser.IsActive = user.IsActive;
                //contextUser.RetireDate = user.RetireDate;
                context.SaveChanges();
            }
            return Redirect("~/Admin/AdminSearch");
        }
        public ActionResult AdminEditFindRecordById(int id)
        {
            using (HmDbContext context = new HmDbContext())
            {
                var users = new List<User>(context.Users.Where(u => u.ID == id).ToList());
                if (users.Count == 1)
                {
                    return View(users.First());
                }
                //else throw new InvalidOperationException("More than 1 user with the same Id found!");
            }
            return Redirect("~/Admin/AdminSearch");
        }
        public ActionResult AdminDelete(int id)
        {
            using (HmDbContext context = new HmDbContext())
            {
                var users = new List<User>(context.Users.Where(u => u.ID == id).ToList());
                if (users.Count == 1)
                {
                    context.Users.Remove(users.First());
                    context.SaveChanges();
                }
                //else throw new InvalidOperationException("More than 1 user with the same Id found!");
            }

            return Redirect("~/Admin/AdminSearch");
        }

        public ActionResult AdminSearchRoom(FormCollection collection)
        {
            List<Room> rooms = new List<Room>();
            using (HmDbContext context = new HmDbContext())
            {
                rooms = new List<Room>(context.Rooms.OrderBy(x => x.Occupation).ToList());
            }
            return View(rooms);
        }
        public ActionResult AdminCreateRoom(FormCollection collection)
        {
            if (Request.Form.Count == 0) return View();
            foreach (string form in Request.Form)
                if (Request.Form[form].Equals(null) || Request.Form[form].ToString() == "") return View();

            Room room = new Room();
            room.Capacity = int.Parse(Request.Form["capacity"]);
            //room.Type.Request.Form["roomtype"];
            room.AdultBedPrice = decimal.Parse(Request.Form["adultbedprice"]);
            room.ChildBedPrice = decimal.Parse(Request.Form["childbedprice"]);
            room.Number = Request.Form["number"];
            using (HmDbContext context = new HmDbContext())
            {
                context.Rooms.Add(room);
                context.SaveChanges();
            }
            return View();
        }
        
        public ActionResult AdminEditRoom(Room room)
        {
            using (HmDbContext context = new HmDbContext())
            {
                /*var contexRoom = (from rooms in context.Rooms
                                  where rooms.ID.Equals(rooms.ID)
                                  select rooms).FirstOrDefault();*/
                Room contextRoom = new Room();
                foreach (var rm in context.Rooms)
                {
                    if (room.ID.Equals(rm.ID))
                    {
                        contextRoom = rm;
                    }
                }
                contextRoom.Capacity = room.Capacity;
                contextRoom.AdultBedPrice = room.AdultBedPrice;
                contextRoom.ChildBedPrice = room.ChildBedPrice;
                contextRoom.Number = room.Number;
                context.SaveChanges();
            }
            return Redirect("~/Admin/AdminSearchRoom");
        }
        public ActionResult AdminEditFindRoomById(int id)
        {
            using (HmDbContext context = new HmDbContext())
            {
                var rooms = new List<Room>(context.Rooms.Where(r => r.ID == id).ToList());
                if (rooms.Count == 1)
                {
                    return View(rooms.First());
                }
                //else throw new InvalidOperationException("More than 1 user with the same Id found!");
            }
            return Redirect("~/Admin/AdminSearchRooms");
        }
        public ActionResult AdminDeleteRoom(int id)
        {
            using (HmDbContext context = new HmDbContext())
            {
                var rooms = new List<Room>(context.Rooms.Where(r => r.ID == id).ToList());
                if (rooms.Count == 1)
                {
                    context.Rooms.Remove(rooms.First());
                    context.SaveChanges();
                }
            }

            return Redirect("~/Admin/AdminSearchRoom");
        }
    }
}