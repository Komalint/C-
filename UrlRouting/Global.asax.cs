using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace UrlRouting
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RegisterCustomRoutes(RouteTable.Routes);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public static void RegisterCustomRoutes(RouteCollection routes)
        {
            // 1. URL Routing with Parameters (Articles Search & Viewer)
            routes.MapPageRoute(
                "RouteForArticleSearch",
                "Articles/Search",
                "~/Route1.aspx"
            );

            routes.MapPageRoute(
                "RouteForArticle",
                "Articles/{id}/{title}",
                "~/Route2.aspx"
            );

            // 2. AJAX Partial Page Updates (UpdatePanel Username Checker)
            routes.MapPageRoute(
                "RouteForCheckUsername",
                "Ajax/CheckUsername",
                "~/CheckUnameByUpdatePanel.aspx"
            );

            // 3. Session State Management (from PracticeSet1)
            routes.MapPageRoute(
                "RouteForSessionLogin",
                "Session/Login",
                "~/SessionLogin.aspx"
            );

            routes.MapPageRoute(
                "RouteForSessionDashboard",
                "Session/Dashboard",
                "~/SessionDashboard.aspx"
            );

            // 4. QueryString and Cookie State Transfer (from QueryString project)
            routes.MapPageRoute(
                "RouteForStateUser",
                "State/User",
                "~/StateUser.aspx"
            );

            routes.MapPageRoute(
                "RouteForStateDashboard",
                "State/Dashboard",
                "~/StateDashboard.aspx"
            );

            // 5. Data Validation & Database Entry (from PracticeSet2)
            routes.MapPageRoute(
                "RouteForProductEntry",
                "Products/Add",
                "~/ProductEntry.aspx"
            );

            // 6. ViewState, Session Counter & Control Events (from PracticeSet2 & WebApplication2)
            routes.MapPageRoute(
                "RouteForStateCounter",
                "State/Counter",
                "~/StateCounter.aspx"
            );

            // 7. Search History Page -> from Master Page to Content Page
            routes.MapPageRoute(
                "SearchHistory",
                "SearchHistory",
                "~/SearchHistory.aspx"
            );
        }
    }
}