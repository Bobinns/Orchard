using Orchard;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using Derm.ImageSliderGallery.Models;

namespace Derm.ImageSliderGallery.Drivers
{
    public class ImageSliderGalleryGroupDriver : ContentPartDriver<ImageSliderGalleryGroupPart>
    {
        public ImageSliderGalleryGroupDriver(IOrchardServices services)
        {
            Services = services;
        }

        public Localizer T { get; set; }

        public IOrchardServices Services { get; set; }

        protected override DriverResult Display(ImageSliderGalleryGroupPart part, string displayType, dynamic shapeHelper)
        {
            if (!Services.Authorizer.Authorize(Permissions.ViewSlider, T("Not allowed to view slide items")))
                return new DriverResult();

            return ContentShape("Parts_ImageSliderGalleryGroup",
                   () => shapeHelper.Parts_ImageSliderGalleryGroup(ContentPart: part, ContentItem: part.ContentItem));
        }

        protected override DriverResult Editor(ImageSliderGalleryGroupPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_ImageSliderGalleryGroup_Edit",
              () => shapeHelper.EditorTemplate(
                  TemplateName: "Parts/ImageSliderGalleryGroup",
                  Model: part,
                  Prefix: Prefix));

        }

        protected override DriverResult Editor(ImageSliderGalleryGroupPart part, Orchard.ContentManagement.IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}