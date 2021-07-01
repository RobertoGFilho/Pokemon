using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Translate the text automatically
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Translate(this string text)
        {
            if (text != null)
            {
                var assembly = typeof(StringExtensions).GetTypeInfo().Assembly;
                var assemblyName = assembly.GetName();
                ResourceManager resourceManager = new ResourceManager($"{assemblyName.Name}.Resources.Resources", assembly);
                var lg = CultureInfo.CurrentCulture.Name;
                return resourceManager.GetString(text, new CultureInfo(lg));
            }

            return null;
        }
    }
}
