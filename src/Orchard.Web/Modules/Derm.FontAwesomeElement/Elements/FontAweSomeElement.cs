using Orchard.Localization;
using Orchard.Layouts.Framework.Elements;
using Orchard.Layouts.Helpers;

namespace FontAweSomeElements.Elements
{
    public class FontAweSomeElement : Element
    {

        public virtual string Content
        {
            get { return this.Retrieve(x => x.Content); }
            set { this.Store(x => x.Content, value); }
        }

        public override string Category
        {
            get { return "Content"; }
        }

        public override string ToolboxIcon
        {
            get { return "\uf0d6"; }
        }

        public override LocalizedString Description
        {
            get { return T("Derm Description Section Element"); }
        }

        public string FontUrl
        {
            get { return this.Retrieve(x => x.FontUrl); }
            set { this.Store(x => x.FontUrl, value); }
        }

    }
}