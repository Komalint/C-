using System;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace SimpleWebApiDemo
{
    // =====================================================================================
    // WebApiConfig.cs - Configuration for your Web API
    // =====================================================================================
    // This file defines how URLs map to your C# Controller classes (Routing),
    // and how data (JSON) is formatted when sent back to the client.
    // =====================================================================================
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // ---------------------------------------------------------------------------------
            // 1. Enable Attribute Routing
            // ---------------------------------------------------------------------------------
            // Attribute routing allows you to put custom route attributes directly above your
            // controller methods, for example: [Route("api/students/toppers")]
            config.MapHttpAttributeRoutes();

            // ---------------------------------------------------------------------------------
            // 2. Default Convention-Based Route
            // ---------------------------------------------------------------------------------
            // This is the universal rule for how ASP.NET finds the right controller:
            // Template: "api/{controller}/{id}"
            //
            // Examples:
            //   - A GET request to "api/students" calls StudentsController
            //   - A GET request to "api/students/5" calls StudentsController and passes id = 5
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // ---------------------------------------------------------------------------------
            // 3. Configure JSON as Default Output
            // ---------------------------------------------------------------------------------
            // By default, ASP.NET Web API sends XML when opened in standard web browsers.
            // In modern web development, JSON is almost always preferred!
            // By removing the XmlFormatter, our API will ALWAYS return clean JSON.
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Format JSON with indentation (so it's pretty and easy to read)
            config.Formatters.JsonFormatter.SerializerSettings.Formatting =
                Newtonsoft.Json.Formatting.Indented;

            // Use camelCase property names in JSON:
            // e.g. C# "StudentName" becomes JSON "studentName"
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
        }
    }
}
