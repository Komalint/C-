using MVConFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace MVConFramework.Controllers
{
    public class OrdersController : Controller
    {
        public OrdersRepo or = new OrdersRepo();


        // GET: Orders *********************
        public ActionResult OrderList()
        {
            var orders = or.GetAllOrders();
            return View(orders);
        }

        // GET: Orders/Details/5 ***************************
        public ActionResult OrderDetails(int id, int? storeId)
        {
            ViewBag.StoreId = storeId;

            var orders = or.GetOrderById(id);

            if (orders == null)
            {
                return HttpNotFound();
            }

            return View(orders);
        }

        // GET : Orders Details Based on store
        public ActionResult OrderDetailOfStoreId(int id)
        {
            var orders = or.GetOrdersByStoreId(id);

            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View(new Orders());
        }

        // POST: Orders/Create *****************
        [HttpPost]
        public ActionResult Create(Orders ord)
        {
            try
            {
                string result = or.AddOrder(ord);

                TempData["Message"] = result;

                return RedirectToAction("OrderList");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(ord);
            }
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int id, int?storeId)
        {
            ViewBag.StoreId = storeId;
            Orders order = or.GetOrderById(id);
            if(order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5 ***************
        [HttpPost]
        public ActionResult Edit(Orders ord, int? storeId)
        {
            try
            {
                string result = or.UpdateOrder(ord);

                TempData["Message"] = result;

                if (storeId.HasValue) { 
                return RedirectToAction(
                "OrderDetailOfStoreId",
                new { id = storeId });
                }

                return RedirectToAction("OrderList");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.StoreId = storeId;
                return View(ord);
            }
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int id, int? storeId)
        {
            ViewBag.StoreId = storeId;

            Orders order = or.GetOrderById(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5 **********************
        [HttpPost]
        public ActionResult Delete(Orders od, int? storeId)
        {
            try
            {
                string result = or.DeleteOrder(od.OrderId);

                TempData["Message"] = result;

                if (storeId.HasValue)
                {
                    return RedirectToAction(
                        "OrderDetailOfStoreId",
                        new { id = storeId });
                }

                return RedirectToAction("OrderList");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.StoreId = storeId;

                return View();
            }
        }
    }
}
