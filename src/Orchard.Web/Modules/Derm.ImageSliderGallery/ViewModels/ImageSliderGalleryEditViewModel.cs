using System.Collections.Generic;
using System.Web.Mvc;

namespace Derm.ImageSliderGallery.ViewModels
{
    public class ImageSliderGalleryEditViewModel
    {
        public byte Sort{ get; set; }
        public IList<SelectListItem> Groups { get; set; }
        public int GroupId { get; set; }
    }
}