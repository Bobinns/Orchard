using Orchard.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Derm.ImageSliderGallery
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();
            manifest.DefineStyle("Animate").SetUrl("animate.min.css", "animate.css");
            manifest.DefineScript("Bootstrp").SetUrl("bootstrap.min.js", "bootstrap.js").SetDependencies("jQuery");
            //manifest.DefineStyle("settingsmin").SetUrl("settingsmin.css");
        }
    }
}