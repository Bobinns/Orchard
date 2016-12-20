using Orchard;
using Orchard.ContentManagement;
using Orchard.DisplayManagement;
using Orchard.Localization;
using Orchard.UI.Admin;
using System.Linq;
using System.Web.Mvc;
using Derm.ImageSliderGallery.Models;

namespace Derm.ImageSliderGallery.Controllers
{
    [ValidateInput(false), Admin]
    public class AdminController : Controller
    {
        private readonly IContentManager _contentManager;

        public AdminController(IContentManager contentManager, IShapeFactory shapeFactory, IOrchardServices services)
        {
            _contentManager = contentManager;
            Shape = shapeFactory;
            Services = services;
        }

        dynamic Shape { get; set; }

        public IOrchardServices Services { get; set; }
        public Localizer T { get; set; }


        public ActionResult Items()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageSlider, T("Not allowed to manage slide items")))
                return new HttpUnauthorizedResult();

            var items = _contentManager.Query<ImageSliderGalleryPart, ImageSliderGalleryPartRecord>().OrderBy(i => i.Sort).List();

            var list = Shape.List();

            list.AddRange(items.Select(i => _contentManager.BuildDisplay(i, "SummaryAdmin")));

            dynamic viewModel = Shape.ViewModel();
            viewModel.ContentItems(list);
            viewModel.NumberOfItems(items.Count());

            return View(viewModel);
        }

        public ActionResult Groups()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageSlider, T("Not allowed to manage slide items")))
                return new HttpUnauthorizedResult();

            var items = _contentManager.Query("ImageSliderGalleryGroup").List();

            var list = Shape.List();

            list.AddRange(items.Select(i => _contentManager.BuildDisplay(i, "SummaryAdmin")));

            dynamic viewModel = Shape.ViewModel();
            viewModel.ContentItems(list);
            viewModel.NumberOfItems(items.Count());

            return View(viewModel);
        }
    }
}