using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Derm.ImageSliderGallery.Models;

namespace Derm.ImageSliderGallery.Handlers
{
    public class ImageSliderGalleryWidgetHandler : ContentHandler
    {
        public ImageSliderGalleryWidgetHandler(IRepository<ImageSliderGalleryWidgetPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}