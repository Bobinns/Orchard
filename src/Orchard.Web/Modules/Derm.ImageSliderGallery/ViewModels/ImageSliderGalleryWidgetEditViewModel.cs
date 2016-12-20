using System.Collections.Generic;
using System.Web.Mvc;

namespace Derm.ImageSliderGallery.ViewModels
{
    public class ImageSliderGalleryWidgetEditViewModel
    {
        public byte Count { get; set; }
        public IList<SelectListItem> Groups { get; set; }
        public int GroupId { get; set; }

        //public IList<SelectListItem> SliderTypes
        //{
        //    get
        //    {
        //        return new List<SelectListItem>() { 
        //        new SelectListItem() {Value = "flyin", Text = "flyin", Selected = true },
        //        new SelectListItem() {Value = "cube", Text = "cube" }
        //    };
        //    }
        //}

        //public virtual string SliderType { get; set; }
    }
}