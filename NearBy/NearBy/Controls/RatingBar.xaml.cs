using NearByCore.Constants;
using NearByCore.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace NearBy.Controls
{
    [Preserve(AllMembers = true)]
    public partial class RatingBar : ContentView
    {
        #region Bindable Properties

        public static readonly BindableProperty AverageRatingProperty = BindableProperty.Create(nameof(AverageRating),
            typeof(decimal), typeof(RatingBar), 0m, propertyChanged: OnPropertyChanged);

        public static readonly BindableProperty NumberOfRatingsProperty = BindableProperty.Create(nameof(NumberOfRatings),
            typeof(int), typeof(RatingBar), 0, propertyChanged: OnPropertyChanged);

        #endregion

        #region Constructors

        public RatingBar()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        public decimal AverageRating
        {
            get => (decimal) GetValue(AverageRatingProperty);
            set => SetValue(AverageRatingProperty, value);
        }

        public decimal NumberOfRatings
        {
            get => (int)GetValue(NumberOfRatingsProperty);
            set => SetValue(NumberOfRatingsProperty, value);
        }

        #endregion

        #region Methods

        private static void OnPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var ratingBar = (RatingBar) bindable;

            ratingBar.UpdateReviewDisplay();
        }

        private ImageSource GetSource(int ratingThreshold)
        {
            if (AverageRating >= ratingThreshold)
                return AppConstants.StarImage;

            return AverageRating > ratingThreshold - 1
                ? AppConstants.HalfStarImage
                : AppConstants.NoStarImage;
        }

        private void UpdateReviewDisplay()
        {
            var rated = AverageRating > 0m;

            FFirstStar.IsVisible = FSecondStar.IsVisible = FThirdStar.IsVisible =
                FFourthStar.IsVisible = FFifthStar.IsVisible = rated;

            if (rated)
            {
                FFirstStar.Source = GetSource(1);
                FSecondStar.Source = GetSource(2);
                FThirdStar.Source = GetSource(3);
                FFourthStar.Source = GetSource(4);
                FFifthStar.Source = GetSource(5);
            }

            FAverageRating.Text = rated
                ? AverageRating.ToString("(#.#)")
                : AppResources.Unrated;

            if (!rated || NumberOfRatings == 0)
            {
                FNumberOfRatings.IsVisible = false;
            }
            else
            {
                FNumberOfRatings.IsVisible = true;
                FNumberOfRatings.Text = $"({NumberOfRatings})";
            }

        }

        #endregion
    }
}