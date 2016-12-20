using Orchard.Mvc.Routes;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace Derm.ImageSliderGallery
{
    public class Routes : IRouteProvider
    {
        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }


        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[] {
                 new RouteDescriptor {
                    Route = new Route(
                        "Admin/Slider",
                        new RouteValueDictionary {
                            {"area", "Derm.ImageSliderGallery"},
                            {"controller", "Admin"},
                            {"action", "Items"}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "Derm.ImageSliderGallery"}
                        },
                        new MvcRouteHandler())
                }, new RouteDescriptor {
                    Route = new Route(
                        "Admin/Group",
                        new RouteValueDictionary {
                            {"area", "Derm.ImageSliderGallery"},
                            {"controller", "Admin"},
                            {"action", "Groups"}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "Derm.ImageSliderGallery"}
                        },
                        new MvcRouteHandler())
                }
            };
        }
    }
}