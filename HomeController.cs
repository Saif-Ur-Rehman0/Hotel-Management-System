using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using db_connectivity.Models;
namespace db_connectivity.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Login()
        {

            return View();
        }

        public ActionResult SignUP()
        {

            return View();
        }

        public ActionResult Staff()
        {

            return View();
        }
        public ActionResult Manager()
        {
            List<Staff> users = CRUD.getAllStaff();
            Console.Write(users);
            return View(users);
        }
        public ActionResult Room_Booker(string id)
        {
            List<Roomers> users = CRUD.getroomRecords(id);
            Console.Write(users);
            return View(users);
        }
        [HttpGet]
        public ActionResult Cook(string id)
        {
            List<Cookers> users = CRUD.getCookRecords(id);
            Console.Write(users);
            return View(users);
        }

        public ActionResult Choosen(String custid, String userId, String name, String choice)
        {
           int result = CRUD.savecookchoice(name,custid, userId,choice);

           // return RedirectToAction("Cook", new System.Web.Routing.RouteValueDictionary(new { action = "Cook", id = userId }));

            List<Cookers> users = CRUD.getCookRecords(userId);
            Console.Write(users);
            return View("Cook",(object)users);
        }

        public ActionResult Add_Staff()
        {

            return View();
        }
        [HttpGet]
        public ActionResult RoomCheck(String custid, String staffid, String Rtype, String status, String chdate, String choutdate,String guests)
        {
            Roomers roomie = new Roomers();
            roomie.Custid = custid;
            roomie.Staffid = staffid;
            roomie.Roomtype = Rtype;
            roomie.noguests = guests;
            roomie.stat = status;
            roomie.Checkindate = chdate;
            roomie.checkoutdate = choutdate;
             roomie.RoomId = CRUD.FreeRoom(roomie.Roomtype).ToString();
            if (roomie.RoomId == "-1")
            {
                String data = "Something went wrong while connecting with the database.";
                return View("Room_Booker",(object)data);
            }

            return View(roomie);
        }

        public ActionResult savechoice(String custid, String staffid, String id ,String Rtype, String status, String chdate, String choutdate, String guests, String choice)
        {
            Roomers roomer = new Roomers();
            roomer.Custid = custid;
            roomer.Staffid = staffid;
            roomer.RoomId = id;
            roomer.Roomtype = Rtype;
            roomer.noguests = guests;
            roomer.stat = status;
            roomer.Checkindate = chdate;
            roomer.checkoutdate = choutdate;
            roomer.stat = choice;
            int result = CRUD.bookroom(roomer);
            if (result == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("Room_Booker", (object)data);
            }
            else if (result == 0)
            {
                String data = "Room couldn't be Booked";
                return View("Room_Booker", (object)data);
            }

            List<Roomers> users = CRUD.getroomRecords(staffid);
            Console.Write(users);
            return View("Room_Booker", (object)users);
        }

        public ActionResult Remove_Staff()
        {

            return View();
        }
        public ActionResult Update_Staff()
        {

            return View();
        }
        public ActionResult Customer()
        {

            return View();
        }
        public ActionResult RoomType()
        {

            return View();
        }
        public ActionResult FoodType()
        {

            return View();
        }

        public ActionResult BookRoom()
        {

            return View();
        }
        public ActionResult Feedback()
        {

            return View();
        }
        public ActionResult Enter_Feedback()
        {

            return View();
        }
        public ActionResult OrderFood(string foodid)
        {

            return View();
        }
        public ActionResult Signer(String Fname, String Lname, String id,String CNIC, String password, String phoneno, String optradio )
        {
            string name = Fname;
            name += " ";
            name += Lname;
            int result = CRUD.Signup( name, id,CNIC, password, phoneno, optradio);

            if (result == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("SignUP", (object)data);
            }
            else if (result == 0)
            {

                String data = "User already exists";
                return View("SignUP", (object)data);
            }
            return RedirectToAction("Index");

        }

        public ActionResult authenticate(String userId, String password)
        {
            int result = CRUD.Login(userId, password);
            if (result == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("Login", (object)data);
            }
            else if (result == 0)
            {

                String data = "Incorrect Credentials";
                return View("Login", (object)data);
            }

            return RedirectToAction("Customer");

        }
        public ActionResult authenticateStaff(String userId, String password, String optradio)
        {
            int result = CRUD.Check_staff(userId, password, optradio);
            //YAHA Procedure call karna hy k staff hy k nahi database mein
            if (result == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("Staff", (object)data);
            }
            else if (result == 0)
            {
                String data = "Incorrect Credentials";
                return View("Staff", (object)data);
            }

            if (optradio == "Manager")
            {
                List<Staff> data = CRUD.getAllStaff();
                return View("Manager", (object)data);
            }
            else if (optradio == "Room Booker")
            {
                
                          return RedirectToAction("Room_Booker", new System.Web.Routing.RouteValueDictionary(
                new { action = "Room_Booker", id = userId }));

            }
            else if (optradio == "Cook")
            {
                return RedirectToAction("Cook", new System.Web.Routing.RouteValueDictionary(
    new { action = "Cook", id = userId }));
            }


            return RedirectToAction("homePage");

        }

        public ActionResult Managertasks(String optradio)
        {

            if (optradio == "Add Staff")
            {
                return View("Add_Staff");
            }
            else if (optradio == "Remove Staff")
            {
                return View("Remove_Staff");
            }
            else if (optradio == "Update Staff")
            {
                return View("Update_Staff");
            }


            return RedirectToAction("Manager");

        }
        public ActionResult Addstaff(String Fname, String Lname, String id, String CNIC, String password, String phoneno, String optradio, String position, String salary)
        {
            string name = Fname;
            name += " ";
            name += Lname;
            //YAHA Add staff member wala procedure call karna hy
            int result = CRUD.addstaff(salary, name, id, CNIC, password, phoneno, optradio, position);

            if (result == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("Add_Staff", (object)data);
            }
            else if (result == 0)
            {

                String data = "Staff already exists";
                return View("Add_Staff", (object)data);
                
            }


            return RedirectToAction("Manager");

        }
        public ActionResult Removestaff(String id, String position)
        {

            //YAHA Remove staff member wala procedure call karna hy
            int result = CRUD.Removestaff(id, position);

            if (result == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("Remove_Staff", (object)data);
            }
            else if (result == 0)
            {

                String data = "Staff doesn't exists";
                return View("Remove_Staff", (object)data);

            }
            return RedirectToAction("Manager");
        }

        public ActionResult Updatestaff(String Fname, String Lname, String id, String CNIC, String password, String phoneno, String optradio, String position, String salary)
        {
            string name = Fname;
            name += " ";
            name += Lname;
            //YAHA Update staff member wala procedure call karna hy
            int result = CRUD.updatestaff(salary, name, id, CNIC, password, phoneno, optradio, position);

            if (result == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("Update_Staff", (object)data);
            }
            else if (result == 0)
            {

                String data = "Staff doesn't exists";
                return View("Update_Staff", (object)data);

            }


            return RedirectToAction("Manager");

        }

        public ActionResult homePage()
        {
        
            List<Staff> users = CRUD.getAllStaff();
            if (users == null)
            {
                RedirectToAction("Login");
            }
            Console.Write(users);
            return View(users);
        }
        public ActionResult Give_Feedback(String Id,String password,String About, String Comment, String rate)
        {
            int result = CRUD.Login(Id, password);
            if (result == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("Enter_Feedback", (object)data);
            }
            else if (result == 0)
            {

                String data = "Incorrect Credentials";
                return View("Enter_Feedback", (object)data);
            }
            int result2 = CRUD.EnterFeedack(Id, About, Comment, rate);
            if (result2 == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("Enter_Feedback", (object)data);
            }
            else if (result2 == 0)
            {

                String data = "Incorrect Credentials";
                return View("Enter_Feedback", (object)data);
            }
            String data2 = "Feedback Recorded";
            return View("Enter_Feedback", (object)data2);
        }
        public ActionResult RequestForFood(String Id, String password, String roomid, String quantity, String food)
        {
            int result = CRUD.Login(Id, password);
            if (result == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("OrderFood", (object)data);
            }
            else if (result == 0)
            {

                String data = "Incorrect Credentials";
                return View("OrderFood", (object)data);
            }
            int result2 = CRUD.checkcustomer(Id, roomid);
            if (result2 == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("OrderFood", (object)data);
            }
            else if (result2 == 0)
            {

                String data = "You are not living in Room Number: ";
                data += roomid;
                return View("OrderFood", (object)data);
            }
            int result3 = CRUD.Request_for_food(Id, food, quantity);
            if (result3 == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("OrderFood", (object)data);
            }

            String data1 = "Your request for Food is sent to the Cook, you will be informed through sms shortly.";
            return View("OrderFood", (object)data1);
        }
        public ActionResult RequestForRoom(String Id, String password, String checkindate, String checkoutdate, String numguests, String rtype)
        {
            int result = CRUD.Login(Id, password);
            if (result == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("BookRoom", (object)data);
            }
            else if (result == 0)
            {

                String data = "Incorrect Credentials";
                return View("BookRoom", (object)data);
            }
            int result2 = CRUD.checkdate(checkindate, checkoutdate);
            if (result2 == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("BookRoom", (object)data);
            }
            else if (result2 == 0)
            {

                String data = "Date sequence in not correct.";
                return View("BookRoom", (object)data);
            }
            int result1 = CRUD.Request_for_room(Id, rtype, checkindate, checkoutdate, numguests);
            if (result1 == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("BookRoom", (object)data);
            }
            else if (result1 == 0)
            {

                String data = "Room of your choice is not available.";
                return View("BookRoom", (object)data);
            }

            String data1 = "Your request for Room is sent to the Room Booker, you will be informed through sms shortly.";
            return View("BookRoom", (object)data1);
        }
    }
}