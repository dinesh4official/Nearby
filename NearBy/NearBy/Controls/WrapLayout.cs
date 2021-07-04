using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using NearByCore.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace NearBy.Controls
{
    [Preserve(AllMembers = true)]
    public class WrapLayout : Layout<View>
    {
        #region Bindable Properties

        public static readonly BindableProperty OrientationProperty = BindableProperty.Create(nameof(Orientation),
            typeof(StackOrientation), typeof(WrapLayout), propertyChanged: OnSizeChanged,
            defaultValue: StackOrientation.Vertical);

        public static readonly BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing),
            typeof(double), typeof(WrapLayout), propertyChanged: OnSizeChanged, defaultValue: 6D);

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource),
            typeof(IList), typeof(WrapLayout), propertyChanged: ItemsSource_OnPropertyChanged);

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate),
            typeof(DataTemplate), typeof(WrapLayout), propertyChanged: OnSizeChanged);


        public static readonly BindableProperty TemplateSelectorProperty =
            BindableProperty.Create(nameof(TemplateSelector),
                typeof(DataTemplateSelector), typeof(WrapLayout), propertyChanged: OnSizeChanged);

        #endregion

        #region Constructor

        public WrapLayout()
        {

        }

        #endregion

        #region Properties

        public IList ItemsSource
        {
            get => (IList) GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate) GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public StackOrientation Orientation
        {
            get => (StackOrientation) GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public double Spacing
        {
            get => (double) GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }

        public DataTemplateSelector TemplateSelector
        {
            get => (DataTemplateSelector) GetValue(TemplateSelectorProperty);
            set => SetValue(TemplateSelectorProperty, value);
        }

        #endregion

        #region Override Methods

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            if (Orientation == StackOrientation.Vertical)
            {
                double colWidth = 0;
                var yPos = y;
                var xPos = x;

                foreach (var child in Children.Where(c => c.IsVisible))
                {
                    var request = child.Measure(width, height);

                    var childWidth = request.Request.Width;
                    var childHeight = request.Request.Height;

                    colWidth = Math.Max(colWidth, childWidth);

                    if (yPos + childHeight > height)
                    {
                        yPos = y;
                        xPos += colWidth + Spacing;
                        colWidth = 0;
                    }

                    var region = new Rectangle(xPos, yPos, childWidth, childHeight);
                    LayoutChildIntoBoundingRegion(child, region);
                    yPos += region.Height + Spacing;
                }
            }
            else
            {
                double rowHeight = 0;
                var yPos = y;
                var xPos = x;

                double max = 0;

                foreach (var child in Children.Where(c => c.IsVisible))
                {
                    var request = child.Measure(width, height);
                    max = Math.Max(max, request.Request.Width);
                }

                foreach (var child in Children.Where(c => c.IsVisible))
                {
                    var request = child.Measure(width, height);

                    var childWidth = request.Request.Width;
                    var childHeight = request.Request.Height;

                    rowHeight = Math.Max(rowHeight, childHeight);

                    if (xPos + childWidth > width)
                    {
                        xPos = x;
                        yPos += rowHeight + Spacing;
                        rowHeight = 0;
                    }

                    var region = new Rectangle(xPos, yPos, childWidth, childHeight);
                    LayoutChildIntoBoundingRegion(child, region);
                    xPos += region.Width + Spacing;
                }
            }
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            if (WidthRequest > 0)
                widthConstraint = Math.Min(widthConstraint, WidthRequest);

            if (HeightRequest > 0)
                heightConstraint = Math.Min(heightConstraint, HeightRequest);

            var internalWidth = double.IsPositiveInfinity(widthConstraint)
                ? double.PositiveInfinity
                : Math.Max(0, widthConstraint);
            var internalHeight = double.IsPositiveInfinity(heightConstraint)
                ? double.PositiveInfinity
                : Math.Max(0, heightConstraint);

            return Orientation == StackOrientation.Vertical
                ? DoVerticalMeasure(internalWidth, internalHeight)
                : DoHorizontalMeasure(internalWidth, internalHeight);
        }

        #endregion

        #region Callback Methods

        static void ItemsSource_OnPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is WrapLayout wrapLayout)
            {
                if (oldvalue != null)
                {
                    var coll = (INotifyCollectionChanged)oldvalue;
                    coll.CollectionChanged -= wrapLayout.OnCollectionChanged;
                }

                if (newvalue != null)
                {
                    var coll = (INotifyCollectionChanged)newvalue;
                    coll.CollectionChanged += wrapLayout.OnCollectionChanged;
                }
            }
            else
            {
                throw new Exception(AppResources.BindableObjectError);
            }
        }

        static void OnSizeChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is WrapLayout wrapLayout)
            {
                wrapLayout.OnSizeChanged();
            }
            else
            {
                throw new Exception(AppResources.BindableObjectError);
            }
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    var list = sender as IList;
                    if (list == null) return;
                    foreach (var item in list)
                    {
                        var child = ItemTemplate.CreateContent() as View;
                        if (child == null)
                            return;

                        child.BindingContext = item;
                        Children.Add(child);
                    }
                    return;
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in args.NewItems)
                    {
                        var child = ItemTemplate.CreateContent() as View;
                        if (child == null)
                            return;

                        child.BindingContext = item;
                        Children.Add(child);
                    }
                    return;
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Move:
                    //TODO: Handle These
                    break;
            }
        }

        #endregion

        #region Methods

        SizeRequest DoHorizontalMeasure(double widthConstraint, double heightConstraint)
        {
            var rowCount = 1;
            double width = 0;
            double height = 0;
            double minWidth = 0;
            double minHeight = 0;
            double widthUsed = 0;

            foreach (var item in Children.Where(c => c.IsVisible))
            {
                var size = item.Measure(widthConstraint, heightConstraint);

                height = Math.Max(height, size.Request.Height);

                var newWidth = width + size.Request.Width + Spacing;

                if (newWidth > widthConstraint)
                {
                    rowCount++;
                    widthUsed = Math.Max(width, widthUsed);
                    width = size.Request.Width;
                }
                else
                {
                    width = newWidth;
                }

                minHeight = Math.Max(minHeight, size.Minimum.Height);
                minWidth = Math.Max(minWidth, size.Minimum.Width);
            }

            if (rowCount <= 1)
                return new SizeRequest(new Size(width, height), new Size(minWidth, minHeight));

            width = Math.Max(width, widthUsed);
            height = (height + Spacing) * rowCount;

            return new SizeRequest(new Size(width, height), new Size(minWidth, minHeight));
        }

        SizeRequest DoVerticalMeasure(double widthConstraint, double heightConstraint)
        {
            var columnCount = 1;

            double width = 0;
            double height = 0;
            double minWidth = 0;
            double minHeight = 0;
            double heightUsed = 0;

            foreach (
                var size in
                Children.Where(c => c.IsVisible).Select(item => item.Measure(widthConstraint, heightConstraint)))
            {
                width = Math.Max(width, size.Request.Width + Spacing);

                var newHeight = height + size.Request.Height + Spacing;

                if (newHeight > heightConstraint)
                {
                    columnCount++;
                    heightUsed = Math.Max(height, heightUsed);
                    height = size.Request.Height;
                }
                else
                {
                    height = newHeight;
                }

                minHeight = Math.Max(minHeight, size.Minimum.Height);
                minWidth = Math.Max(minWidth, size.Minimum.Width);
            }

            if (columnCount <= 1)
                return new SizeRequest(new Size(width, height), new Size(minWidth, minHeight));

            height = Math.Max(height, heightUsed);
            width *= columnCount; // take max width
            width -= Spacing;
            return new SizeRequest(new Size(width, height), new Size(minWidth, minHeight));
        }

        private void OnSizeChanged()
        {
            ForceLayout();
        }

        #endregion
    }
}