using MVConFramework.Models;
using System.Web.Mvc;

namespace MVConFramework.Controllers
{
    public class StoresController : Controller
    {
        StoresRepo repo = new StoresRepo();

        // GET: Stores
        public ActionResult Index()
        {
            return View(repo.GetAllStores());
        }

        // GET: Stores/Details/5
        public ActionResult Details(int id)
        {
            return View(repo.GetStoreById(id));
        }

        // GET: Stores/Create
        public ActionResult Create()
        {
            return View(new Stores());
        }

        // POST: Stores/Create
        [HttpPost]
        public ActionResult Create(Stores store)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repo.AddStore(store);

                    TempData["Message"] = "Store Created Successfully";

                    return RedirectToAction("Index");
                }

                return View(store);
            }
            catch
            {
                return View(store);
            }
        }

        // GET: Stores/Edit/5
        public ActionResult Edit(int id)
        {
            Stores store = repo.GetStoreById(id);

            return View(store);
        }

        // POST: Stores/Edit/5
        [HttpPost]
        public ActionResult Edit(Stores store)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repo.UpdateStore(store);

                    TempData["Message"] = "Store Updated Successfully";

                    return RedirectToAction("Index");
                }

                return View(store);
            }
            catch
            {
                return View(store);
            }
        }

        // GET: Stores/Delete/5
        public ActionResult Delete(int id)
        {
            Stores store = repo.GetStoreById(id);

            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                repo.DeleteStore(id);

                TempData["Message"] = "Store Deleted Successfully";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}