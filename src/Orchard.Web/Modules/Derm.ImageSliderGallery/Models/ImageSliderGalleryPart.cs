﻿using Orchard.ContentManagement;
using System.ComponentModel.DataAnnotations;

namespace Derm.ImageSliderGallery.Models
{
    public class ImageSliderGalleryPart : ContentPart<ImageSliderGalleryPartRecord>
    {
        [Required]
        public byte Sort
        {
            get
            {
                if (Record.Sort == 0)
                    return 1;
                return Record.Sort;
            }
            set { Record.Sort = value; }
        }

        [Required]
        public int GroupId
        {
            get
            {
                return Record.GroupId;
            }
            set
            {
                Record.GroupId = value;
            }
        }
    }
}