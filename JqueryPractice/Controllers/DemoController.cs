using System.Net;
using System.Web.Mvc;

namespace JqueryPractice.Controllers
{
    public class DemoController : Controller
    {
        // 1. RedirectResult: Redirects to an external URL (Slide 2)
        public RedirectResult ExternalRedirect()
        {
            return Redirect("https://mccit.net");
        }

        // 2. RedirectToRouteResult: Redirects to another action using route dictionary (Slide 2 & 3)
        public RedirectToRouteResult RouteRedirect()
        {
            return RedirectToRoute(new { controller = "Home", action = "Contact" });
        }

        // 3. RedirectToAction: Redirects to a specified controller and action (Slide 3)
        public ActionResult ActionRedirect()
        {
            return RedirectToAction("InsertUserDetails", "User");
        }

        // 4. HttpStatusCodeResult: Returns custom status code and message (Slide 26)
        public HttpStatusCodeResult UnauthorizedStatusCode()
        {
            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "You are not authorized to access this controller action.");
        }

        // 5. HttpStatusCodeResult: BadGateway (502) (Slide 26)
        public HttpStatusCodeResult BadGateway()
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadGateway, "I have no idea what this error means.");
        }

        // 6. HttpUnauthorizedResult: Specific 401 result (Slide 28)
        public HttpStatusCodeResult UnauthorizedResult()
        {
            return new HttpUnauthorizedResult("You are not authorized to access this controller action.");
        }

        // 7. HttpNotFoundResult: 404 Not Found helper (Slide 29)
        public HttpNotFoundResult NotFoundAction()
        {
            return HttpNotFound("We didn't find that action, sorry!");
        }
    }
}
