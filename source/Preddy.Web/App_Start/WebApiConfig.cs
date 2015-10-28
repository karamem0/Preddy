using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Preddy {

    public static class WebApiConfig {

        public static void Register(HttpConfiguration config) {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{Controller}/{Id}",
                new { Id = RouteParameter.Optional }
            );
        }

    }

}
