using Orchard.Layouts.Framework.Display;
using Orchard.Layouts.Framework.Drivers;
using Orchard.Layouts.Helpers;
using Orchard.Layouts.Services;
using FontAweSomeElements.ViewModels;
using FontAweSomeElements.Elements;

namespace FontAweSomeElements.Drivers
{
    public class FontAweSomeElementDriver : ElementDriver<FontAweSomeElement>
    {
        private readonly IElementFilterProcessor _processor;

        public FontAweSomeElementDriver(IElementFilterProcessor processor)
        {
            _processor = processor;
        }

        protected override EditorResult OnBuildEditor(FontAweSomeElement element, ElementEditorContext context)
        {
            var viewModel = new FontAweSomeViewModel
            {
                Content = element.Content,
                FontUrl = element.FontUrl
            };

            var editor = context.ShapeFactory.EditorTemplate(TemplateName: "Elements.FontAweSome", Model: viewModel);

            if (context.Updater != null)
            {
                context.Updater.TryUpdateModel(viewModel, context.Prefix, null, null);
                element.Content = viewModel.Content;
                element.FontUrl = viewModel.FontUrl;
            }

            return Editor(context, editor);
        }

        protected override void OnDisplaying(FontAweSomeElement element, ElementDisplayingContext context)
        {
            context.ElementShape.ProcessedContent = _processor.ProcessContent(element.Content, "text", context.GetTokenData());
            context.ElementShape.FontUrl = element.FontUrl;
        }
    }
}