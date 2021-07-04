using System;
using System.Globalization;
using NearByCore.Extensions;
using NearByCore.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace NearBy.Helper.Converters
{
    [Preserve(AllMembers = true)]
    public class PricingLevelConverter : IValueConverter
    {
        #region Fields

        const string D1 = "$";

        const string D2 = "$$";

        const string D3 = "$$$";

        const string D4 = "$$$$";

        #endregion

        #region Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value as int?).IsNull())
                throw new ArgumentException(AppResources.ErrorInteger, nameof(value));

            switch ((int)value)
            {
                case 1:
                    return D1;
                case 2:
                    return D2;
                case 3:
                    return D3;
                case 4:
                    return D4;
                default:
                    return AppResources.NoPriceDetails;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
                throw new ArgumentException(AppResources.ErrorString, nameof(value));

            switch ((string)value)
            {
                case D1:
                    return 1;
                case D2:
                    return 2;
                case D3:
                    return 3;
                case D4:
                    return 4;
                default:
                    return -1;
            }
        }

        #endregion
    }
}
