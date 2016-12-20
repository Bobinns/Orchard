using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Core.Common.Fields;

namespace Derm.ImageSliderGallery {
    public class Migrations : DataMigrationImpl
    {

        public int Create()
        {
            // Creating table ImageSliderGalleryPartRecord
            SchemaBuilder.CreateTable("ImageSliderGalleryPartRecord", table => table
                .ContentPartRecord()
                .Column("Sort", DbType.Byte)
                .Column("GroupId", DbType.Int32)
            );

            // Creating table ImageSliderGalleryWidgetPartRecord
            SchemaBuilder.CreateTable("ImageSliderGalleryWidgetPartRecord", table => table
                .ContentPartRecord()
                .Column("Count", DbType.Byte)
                .Column("GroupId", DbType.Int32)
            );

            ContentDefinitionManager.AlterTypeDefinition("ImageSliderGallery", builder => builder
              .DisplayedAs("Image Slider Gallery")
              .WithPart("ImageSliderGalleryPart")
              .WithPart("CommonPart")
              .WithPart("IdentityPart")
              .WithPart("TitlePart")
          );

            ContentDefinitionManager.AlterTypeDefinition("ImageSliderGalleryWidget", builder => builder
              .DisplayedAs("Image Slider Gallery Widget")
              .WithPart("ImageSliderGalleryWidgetPart")
              .WithPart("CommonPart")
              .WithPart("IdentityPart")
              .WithPart("WidgetPart")
              .WithSetting("Stereotype", "Widget")
          );

            ContentDefinitionManager.AlterPartDefinition("ImageSliderGalleryPart", builder => builder
                .WithField("ForePicture", cfg => cfg.OfType("MediaLibraryPickerField")
                    .WithSetting("MediaLibraryPickerFieldSettings.DisplayedContentTypes", "Image")
                    .WithSetting("MediaLibraryPickerFieldSettings.Multiple", "False"))
                .WithField("LinkButton", cfg => cfg.OfType("LinkField")
                    .WithDisplayName("Button URl Link"))
                .WithField("Header1", cfg => cfg.OfType("InputField")
                    .WithDisplayName("Slide Heading 1"))
                .WithField("Header2", cfg => cfg.OfType("InputField")
                    .WithDisplayName("Slide Heading 2"))
                .WithField("Header3", cfg => cfg.OfType("InputField")
                    .WithDisplayName("Slide Header 3")
                    .WithSetting("TextFieldSettings.Flavor", "text"))
                .WithField("TextBody", cfg => cfg.OfType("TextField")
                    .WithDisplayName("Body text")
                    .WithSetting("TextFieldSettings.Flavor", "textarea"))
                .WithField("SliderType", field => field.OfType("EnumerationField")
                    .WithSetting("EnumerationFieldSettings.Options",
                        string.Join(System.Environment.NewLine, new[] { "Bounce In", "Bounce Out", "Fade In", "Rotate In Down", "Rotate In Up", "Zoom In", "Slide In", "Hinge" })))
            );

            ContentDefinitionManager.AlterTypeDefinition("ImageSliderGalleryGroup", builder => builder
              .DisplayedAs("Image Slider Gallery Group")
              .WithPart("CommonPart")
              .WithPart("IdentityPart")
              .WithPart("TitlePart")
           );

            return 1;
        }
        public int UpdateFrom1()
        {
            ContentDefinitionManager.AlterPartDefinition("ImageSliderGalleryPart", builder => builder
               .RemoveField("Header3")
               .WithField("Header3", cfg => cfg.OfType("InputField")
                    .WithDisplayName("Slide Heading 3")));

            return 2;
        }

        public int UpdateFrom2()
        {
            ContentDefinitionManager.AlterPartDefinition("ImageSliderGalleryPart", builder => builder
               .RemoveField("TextBody")
               .WithField("TextBody", cfg => cfg.OfType("TextField")
                    .WithDisplayName("Body text")
                    .WithSetting("TextFieldSettings.Flavor", "html")));
            return 3;
        }
    }
}