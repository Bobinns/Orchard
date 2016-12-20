using Orchard.ContentManagement;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Derm.ImageSliderGallery.Models
{
    public class ImageSliderGalleryWidgetPart : ContentPart<ImageSliderGalleryWidgetPartRecord>
    {
        [Required]
        public byte Count
        {
            get
            {
                if (Record.Count == 0)
                {
                    return 3;
                }
                return Record.Count;
            }
            set { Record.Count = value; }
        }

        [Required]
        public int GroupId
        {
            get { return Record.GroupId; }
            set { Record.GroupId = value; }
        }

        //public string SliderType
        //{
        //    get { return Record.SliderType; }
        //    set { Record.SliderType = value; }
        //}
    }
}