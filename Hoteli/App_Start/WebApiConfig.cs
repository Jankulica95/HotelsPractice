using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using Hoteli.Interfaces;
using Hoteli.Models;
using Hoteli.Repository;
using Hoteli.Resolver;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Serialization;

namespace Hoteli
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //CORS

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Tracing
            config.EnableSystemDiagnosticsTracing();

            //Unity
            var container = new UnityContainer();
            container.RegisterType<IHotelRepository, HotelRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ILanacHotelaRepository, LanacHotelaRepository>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityResolver(container);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Hotel, HotelDTO>();
                cfg.CreateMap<LanacHotela, LanacHotelaDTO>();

            });
        }
    }
}
