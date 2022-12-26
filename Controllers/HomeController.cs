using Bio.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bio.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        NorthwindEntities db = new NorthwindEntities();
        public ActionResult Index()
        {
            var model = db.Orders.ToList();
            return View(model);
            
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            var model = db.Order_Details.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Yeni()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Yeni(Orders order)
        {
            if (order.OrderID == 0) //for insert
            {
                db.Orders.Add(order);
            }
            else
            {
                var updateData=db.Orders.Find(order.OrderID);
                if(updateData==null)
                {
                    return HttpNotFound();
                }
                updateData.ShipName = order.ShipName;
            }

            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        public ActionResult Update(int id)
        {
            var model = db.Orders.Find(id);
           if (model==null)
            {
                return HttpNotFound();

            }
            return View("Yeni", model);

        }
        public ActionResult Delete(int id)
        {
            var delete = db.Orders.Find(id);
            if (delete == null)
            {

                return HttpNotFound();
            }
            db.Orders.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
    //public ActionResult Delete(int id)
    //{
    //    var delete = DB.ToDoLists.Find(id);
    //    if (delete == null)
    //    {

    //        return HttpNotFound();
    //    }
    //    DB.ToDoLists.Remove(delete);
    //    DB.SaveChanges();
    //    return RedirectToAction("Index", "Home");
    //}
}