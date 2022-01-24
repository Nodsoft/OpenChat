using System.ComponentModel;
using System.Globalization;

namespace Nodsoft.OpenChat.Common;

public static class Utilities
{
    public static T? TryParse<T>(this string inValue)
    {
        TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

        return (T?)(converter.ConvertFromString(null, CultureInfo.InvariantCulture, inValue) ?? default(T));
    }
}
