
using Orchard.ContentManagement.Records;
using System.Collections.Generic;
using System.Web.Mvc;
namespace Derm.ImageSliderGallery.Models
{
    public class ImageSliderGalleryWidgetPartRecord : ContentPartRecord
    {
        public virtual byte Count { get; set; }
        public virtual int GroupId { get; set; }
        //public virtual string SliderType { get; set; }
    }
}