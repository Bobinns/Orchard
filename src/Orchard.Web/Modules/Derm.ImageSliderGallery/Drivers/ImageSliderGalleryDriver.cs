using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Core.Title.Models;
using Orchard.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Derm.ImageSliderGallery.Models;
using Derm.ImageSliderGallery.ViewModels;

namespace Derm.ImageSliderGallery.Drivers
{
    public class ImageSliderGalleryDriver : ContentPartDriver<ImageSliderGalleryPart>
    {
        private readonly IContentManager _contentManager;

        public ImageSliderGalleryDriver(IOrchardServices services, IContentManager contentManager)
        {
            Services = services;
            _contentManager = contentManager;
        }

        public Localizer T { get; set; }

        public IOrchardServices Services { get; set; }

        protected override DriverResult Display(ImageSliderGalleryPart part, string displayType, dynamic shapeHelper)
        {
            if (!Services.Authorizer.Authorize(Permissions.ViewSlider, T("Not allowed to view slide items")))
                return new DriverResult();

            var group = _contentManager.Get(part.GroupId);

            ImageSliderGalleryViewModel model = new ImageSliderGalleryViewModel();

            if (group != null)
                model.Group = group.Get<TitlePart>().Title;


            model.Sort = part.Sort;


            return ContentShape("Parts_ImageSliderGallery",
                () => shapeHelper.Parts_ImageSliderGallery(Item: model));
        }

        protected override DriverResult Editor(ImageSliderGalleryPart part, dynamic shapeHelper)
        {
            var model = new ImageSliderGalleryEditViewModel();

            var list = _contentManager.Query("ImageSliderGalleryGroup").List().Select(i => new { id = i.Id, value = i.Get<TitlePart>().Title });

            model.Groups = new List<SelectListItem>();

            foreach (var item in list)
            {
                if (item.id == part.GroupId)
                {
                    model.Groups.Add(new SelectListItem() { Selected = true, Text = item.value, Value = item.id.ToString() });
                }
                else
                {
                    model.Groups.Add(new SelectListItem() { Selected = false, Text = item.value, Value = item.id.ToString() });
                }
            }

            model.Sort = part.Sort;
            model.GroupId = part.GroupId;


            return ContentShape("Parts_ImageSliderGallery_Edit",
              () => shapeHelper.EditorTemplate(
                  TemplateName: "Parts/ImageSliderGallery",
                  Model: model,
                  Prefix: Prefix));

        }

        protected override DriverResult Editor(ImageSliderGalleryPart part, Orchard.ContentManagement.IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }

        protected override void Importing(ImageSliderGalleryPart part, Orchard.ContentManagement.Handlers.ImportContentContext context)
        {
            string sort = context.Attribute(part.PartDefinition.Name, "Sort");
            byte sortNumber;
            if (sort != null && byte.TryParse(sort, out sortNumber))
            {
                part.Sort = sortNumber;
            }
            string groupId = context.Attribute(part.PartDefinition.Name, "GroupId");
            byte groupIdNumber;
            if (groupId != null && byte.TryParse(groupId, out groupIdNumber))
            {
                part.GroupId = groupIdNumber;
            }
        }

        protected override void Exporting(ImageSliderGalleryPart part, Orchard.ContentManagement.Handlers.ExportContentContext context)
        {
            context.Element(part.PartDefinition.Name).SetAttributeValue("Sort", part.Sort);
            context.Element(part.PartDefinition.Name).SetAttributeValue("GroupId", part.GroupId);
        }


    }
}