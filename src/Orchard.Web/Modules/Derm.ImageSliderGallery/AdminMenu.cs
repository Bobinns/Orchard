using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.UI.Navigation;
using Derm.ImageSliderGallery.Models;

namespace Derm.ImageSliderGallery
{
    public class AdminMenu : INavigationProvider
    {
        private readonly IContentManager _contentManager;

        public Localizer T { get; set; }

        public string MenuName { get { return "admin"; } }

        public AdminMenu(IContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public void GetNavigation(NavigationBuilder builder)
        {
            builder.Add(T("Slider Gallery"), "1.5", BuildMenu);
        }

        private void BuildMenu(NavigationItemBuilder menu)
        {
            menu.Add(T("New Group"), "1",
                       item =>
                       item.Action("Create", "Admin", new { area = "Contents", id = "ImageSliderGalleryGroup" }).Permission(Permissions.CreateSlider));

            menu.Add(T("New Group"), "1",
                       item =>
                       item.Action("Create", "Admin", new { area = "Contents", id = "ImageSliderGalleryGroup" }).Permission(Permissions.CreateSlider));

            if (SliderGroupItemExists())
            {
                menu.Add(T("Manage Groups"), "1.1",
                         item =>
                         item.Action("Groups", "Admin", new { area = "Derm.ImageSliderGallery" }));

                menu.Add(T("New Item"), "1.2",
                         item =>
                         item.Action("Create", "Admin", new { area = "Contents", id = "ImageSliderGallery" }));
            }

            if (SliderGroupItemExists() && SliderItemExists())
            {
                menu.Add(T("Manage Items"), "1.1",
                         item =>
                         item.Action("Items", "Admin", new { area = "Derm.ImageSliderGallery" }).Permission(Permissions.ManageSlider));
            }

        }

        private bool SliderItemExists()
        {
            return _contentManager.Query<ImageSliderGalleryPart, ImageSliderGalleryPartRecord>("ImageSliderGallery").Count() > 0;
        }

        private bool SliderGroupItemExists()
        {
            return _contentManager.Query("ImageSliderGalleryGroup").Count() > 0;
        }
    }
}