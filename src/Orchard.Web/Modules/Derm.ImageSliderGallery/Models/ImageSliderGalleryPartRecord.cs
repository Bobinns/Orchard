using Orchard.ContentManagement.Records;

namespace Derm.ImageSliderGallery.Models
{
    public class ImageSliderGalleryPartRecord : ContentPartRecord
    {
        public virtual byte Sort { get; set; }
        public virtual int GroupId { get; set; }
    }
}