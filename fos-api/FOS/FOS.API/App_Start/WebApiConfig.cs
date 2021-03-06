﻿using FOS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using Unity;

namespace FOS.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/form-data"));
            // Web API routesD:\new fos\fos-api\FOS\FOS.API\App_Start\WebApiConfig.cs
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            // resolve customauth
            config.Filters.Add((IAuthenticationFilter)UnityConfig.Container.Resolve<ICustomAuthentication>());
            //config.MessageHandlers.Add(new CrossDomainHandler());
            //config.MessageHandlers.Add(new CustomLogHandler());
        }
    }
}
