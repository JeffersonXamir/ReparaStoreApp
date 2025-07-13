using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ReparaStoreApp.WPF.Converters
{
    public class MultiBooleanAndConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = values.All(v => v is bool b && b);

            // Si el parámetro es "invert" (sin importar mayúsculas/minúsculas), invierte el resultado
            if (parameter is string p && p.Equals("INVERT", StringComparison.OrdinalIgnoreCase))
            {
                result = !result;
            }

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}
