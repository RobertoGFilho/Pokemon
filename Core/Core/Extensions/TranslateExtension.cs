using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Core.Extensions
{
    //<Label Text = "{extensions:Translate Hello}" />

    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return null;

            var text = Text.Translate();

            return text;
        }
    }
}
