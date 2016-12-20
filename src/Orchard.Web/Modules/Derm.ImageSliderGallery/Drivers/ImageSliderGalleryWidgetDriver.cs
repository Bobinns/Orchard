using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Core.Title.Models;
using Orchard.MediaLibrary.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Derm.ImageSliderGallery.Models;
using Derm.ImageSliderGallery.ViewModels;
using Orchard.Core.Common.Fields;

namespace Derm.ImageSliderGallery.Drivers
{
    public class ImageSliderGalleryWidgetDriver : ContentPartDriver<ImageSliderGalleryWidgetPart>
    {
        private readonly IContentManager _contentManager;

        public ImageSliderGalleryWidgetDriver(IContentManager contentManager)
        {
            _contentManager = contentManager;
        }


        protected override DriverResult Display(ImageSliderGalleryWidgetPart part, string displayType, dynamic shapeHelper)
        {
            var items = _contentManager.Query<ImageSliderGalleryPart, ImageSliderGalleryPartRecord>("ImageSliderGallery").Where(i => i.GroupId == part.GroupId).OrderBy(i => i.Sort).List().Select(i => new ImageSliderGalleryWidgetViewModel()
             {
                 ForeGroundImagePath = ((MediaLibraryPickerField)i.Fields.Single(f => f.Name == "ForePicture")).MediaParts.FirstOrDefault() == null ? "" : ((MediaLibraryPickerField)i.Fields.Single(f => f.Name == "ForePicture")).MediaParts.First().MediaUrl,
                 Title = i.Get<TitlePart>().Title,
                 //BackGroundImagePath = ((MediaLibraryPickerField)i.Fields.Single(f=>f.Name == "BackPicture")).MediaParts.FirstOrDefault() == null ? "" : ((MediaLibraryPickerField)i.Fields.Single( f => f.Name=="BackPicture")).MediaParts.First().MediaUrl,
                 TextBody = i.Fields.Single(f => f.Name == "TextBody").Storage.Get<string>(""),
                 HeadTitle1 = i.Fields.Single(f => f.Name == "Header1").Storage.Get<string>(""),
                 HeadTitle2 = i.Fields.Single(f => f.Name == "Header2").Storage.Get<string>(""),
                 HeadTitle3 = i.Fields.Single(f => f.Name == "Header3").Storage.Get<string>(""),
                 LinkUrl = i.Fields.Single(f => f.Name == "LinkButton").Storage.Get<string>(""),
                 SliderType = i.Fields.Single(f => f.Name == "SliderType").Storage.Get<string>("")
                 //MaxTextWidth = i.Fields.Single(f => f.Name == "MaxTextWidth").Storage.Get<string>("")
                 //Caption = ((MediaLibraryPickerField)i.Fields.Single(f => f.Name == "Picture")).MediaParts.FirstOrDefault() == null ? "" : ((MediaLibraryPickerField)i.Fields.Single(f => f.Name == "Picture")).MediaParts.First().Caption,
                 //AlternateText = ((MediaLibraryPickerField)i.Fields.Single(f => f.Name == "Picture")).MediaParts.FirstOrDefault() == null ? "" : ((MediaLibraryPickerField)i.Fields.Single(f => f.Name == "Picture")).MediaParts.First().AlternateText
             });

            return ContentShape("Parts_ImageSliderGalleryWidget",
                () => shapeHelper.Parts_ImageSliderGalleryWidget(
                    SlideItems: items
                    //SliderType: part.SliderType
                    ));
        }

        protected override DriverResult Editor(ImageSliderGalleryWidgetPart part, dynamic shapeHelper)
        {

            var model = new ImageSliderGalleryWidgetEditViewModel();

            var list = _contentManager.Query(VersionOptions.Latest, "ImageSliderGalleryGroup").List().Select(i => new { id = i.Id, value = i.Get<TitlePart>().Title });

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

            model.Count = part.Count;
            model.GroupId = part.GroupId;
            //model.SliderType = part.SliderType;
            return ContentShape("Parts_ImageSliderGalleryWidget_Edit",
              () => shapeHelper.EditorTemplate(
                  TemplateName: "Parts/ImageSliderGalleryWidget",
                  Model: model,
                  Prefix: Prefix));

        }

        protected override DriverResult Editor(ImageSliderGalleryWidgetPart part, Orchard.ContentManagement.IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }

        protected override void Importing(ImageSliderGalleryWidgetPart part, Orchard.ContentManagement.Handlers.ImportContentContext context)
        {
            string count = context.Attribute(part.PartDefinition.Name, "Count");
            byte countNumber;
            if (count != null && byte.TryParse(count, out countNumber))
            {
                part.Count = countNumber;
            }

            string groupId = context.Attribute(part.PartDefinition.Name, "GroupId");
            byte groupIdNumber;
            if (groupId != null && byte.TryParse(groupId, out groupIdNumber))
            {
                part.GroupId = groupIdNumber;
            }
        }

        protected override void Exporting(ImageSliderGalleryWidgetPart part, Orchard.ContentManagement.Handlers.ExportContentContext context)
        {
            context.Element(part.PartDefinition.Name).SetAttributeValue("Count", part.Count);
            context.Element(part.PartDefinition.Name).SetAttributeValue("GroupId", part.GroupId);
        }
    }
}