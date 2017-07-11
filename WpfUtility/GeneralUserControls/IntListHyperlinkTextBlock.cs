using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WpfUtility.GeneralUserControls
{
    public class IntListHyperlinkTextBlock : TextBlock
    {
        public delegate void IntAsSenderHandler(int sender, EventArgs e);
        public event IntAsSenderHandler HyperlinkClickedEvent;

        public List<int> ItemSource
        {
            get => (List<int>)GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }

        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register(nameof(ItemSource), typeof(List<int>),
                typeof(IntListHyperlinkTextBlock), new FrameworkPropertyMetadata(new List<int>(),
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    UpdateHyperlinks));

        private static IntListHyperlinkTextBlock _self;
        public IntListHyperlinkTextBlock()
        {
            _self = this;
        }

        private static void UpdateHyperlinks(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ApplyHyperLinks(d as IntListHyperlinkTextBlock);
        }

        private static void ApplyHyperLinks(IntListHyperlinkTextBlock tb)
        {
            tb.Inlines.Clear();
            for (var index = 0; index < tb.ItemSource.Count; index++)
            {
                var item = tb.ItemSource[index];
                var hyperlink = new Hyperlink(new Run(item.ToString())) { Tag = item };
                hyperlink.Click += HyperlinkOnClick;
                tb.Inlines.Add(hyperlink);
                if (index < tb.ItemSource.Count - 1)
                {
                    tb.Inlines.Add(new Run(", "));
                }
            }
        }

        private static void HyperlinkOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            _self.HyperlinkOnClick(sender);
        }

        private void HyperlinkOnClick(object sender)
        {
            var tb = sender as Hyperlink;
            if(int.TryParse(tb?.Tag.ToString(), out int articleNumber))
                HyperlinkClickedEvent?.Invoke(articleNumber, EventArgs.Empty);
        }
    }
}