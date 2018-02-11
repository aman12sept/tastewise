using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TasteWise.Models;



namespace MVC_Design.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home


        BAINAEntities1 db = new BAINAEntities1();
        BAINAEntities2 db2 = new BAINAEntities2();
        DetailEntities db3 = new DetailEntities();

        public ActionResult Home()

        {
            try
            {
                String name = Session["name"].ToString();
                String phone = Session["phone"].ToString();
                String cart_val = Session["cart_val"].ToString();
                ViewBag.cart_val = cart_val;
                ViewBag.name = name;
                ViewBag.phone = phone;
               



                return View();
            }
            catch(Exception e)
            {
                ViewBag.name = "";
                ViewBag.phone = "";
                return View();
            }
           
        }
        public ActionResult Order( String Item_Name, String Qty, String Price)
        {

            try
            {
                String name = Session["name"].ToString();
                String phone = Session["phone"].ToString();
                
                ViewBag.name = name;
                ViewBag.phone = phone;

                if (Item_Name != null)
                {
                    Order order = new Order();

                    order.Item_Name = Item_Name;
                    order.Qty = Qty;
                    order.Price = Price;
                    order.Phone = phone;
                    db2.Orders.Add(order);
                    db2.SaveChanges();
                }

                var y = db2.Orders.ToList();

                var val = 0;
                foreach (var item1 in y)
                {
                    if (item1.Phone == phone)
                    {
                        val++;
                    }
                }
                Session["cart_val"] = val;
                String cart_val = Session["cart_val"].ToString();
                ViewBag.cart_val = cart_val;

                return View();
            }
            catch (Exception e)
            {
                ViewBag.name = "";
                ViewBag.phone = "";
                return View();
            }

           
        }
        public ActionResult Gallery()
        {

            return View();
        }
        public ActionResult Company()
        {
            return View();
        }


        public ActionResult Contact()
        {


            return View();

        }

        public ActionResult Signup(String phone, String name, String password)
        {
            Signup sing = new Signup();


            sing.phone = phone;
            sing.name = name;
            sing.password = password;
            db.Signups.Add(sing);
            db.SaveChanges();

            return RedirectToAction("Home", "Home");
        }

        public ActionResult Login(String phone, String password) {

            var x = db.Signups.ToList();
            foreach (var item in x)
            {
                if (item.phone == phone && item.password == password)
                {
                    ViewBag.name = item.name;
                    ViewBag.phone = item.phone;
                    Session["name"] = item.name;
                    Session["phone"] = item.phone;


                    var y = db2.Orders.ToList();

                    var val = 0;
                    foreach (var item1 in y)
                    {
                        if (item1.Phone == phone)
                        {
                            val++;
                        }
                    }
                    Session["cart_val"] = val;


                }
            }


            return RedirectToAction("Home", "Home"/*, new { name =  ViewBag.name, phone = ViewBag.phone}*/);
        }

        public ActionResult Logout()
        {
            Session["name"] = "";
            Session["phone"] = "";
            Session["cart_val"] = "";
            return RedirectToAction("Home", "Home"/*, new { name =  ViewBag.name, phone = ViewBag.phone}*/);
        }

        public ActionResult cart()
        {
            try
            {
                String name = Session["name"].ToString();
                String phone = Session["phone"].ToString();
                String cart_val = Session["cart_val"].ToString();
                ViewBag.cart_val = cart_val;
                ViewBag.name = name;
                ViewBag.phone = phone;

                Order ord = new Order();
                List<Order> order_list = new List<Order>();
                var x = db2.Orders.ToList();
                var total = 0;
                foreach (var item in x)
                {
                    if (item.Phone == phone)
                    {
                        int price = Int32.Parse(item.Price);
                        total = price + total;
                        order_list.Add(item);
                    }
                }
                ViewBag.Total = total;

                return View(order_list.ToList());
            }
            catch (Exception e)
            {
                ViewBag.name = "";
                ViewBag.phone = "";
                return View();
            }
        }

        public ActionResult Delete(int id, string phone)
        {
            
                 Order Del_Ord = db2.Orders.Find(id);
            db2.Orders.Remove(Del_Ord);
            db2.SaveChanges();

            var y = db2.Orders.ToList();

            var val = 0;
            foreach (var item1 in y)
            {
                if (item1.Phone == phone)
                {
                    val++;
                }
            }
            Session["cart_val"] = val;
            return RedirectToAction("Home", "cart");
        }


        public ActionResult details(string name, string phone, string email, string address, string landmark, string pin, string log_phone)
        {

            Detail det = new Detail();
            det.Name = name;
            det.Phone = phone;
            det.Email = email;
            det.Address = address;
            det.Landmark = landmark;
            det.pin = pin;
            det.log_phone = log_phone;

            db3.Details.Add(det);
            db3.SaveChanges();

            return View();
        }

    }
}
