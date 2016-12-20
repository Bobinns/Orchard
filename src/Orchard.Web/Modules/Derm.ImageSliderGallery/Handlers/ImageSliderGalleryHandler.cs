using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Derm.ImageSliderGallery.Models;

namespace Derm.ImageSliderGallery.Handlers
{
    public class ImageSliderGalleryHandler : ContentHandler
    {
        public ImageSliderGalleryHandler(IRepository<ImageSliderGalleryPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}