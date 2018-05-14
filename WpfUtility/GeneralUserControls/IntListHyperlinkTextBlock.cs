using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WpfUtility.GeneralUserControls
{
    /// <inheritdoc />
    /// <summary>
    /// HighlightTextBlock can highlight particullar parts of a text.
    /// </summary>
    public class IntListHyperlinkTextBlock : TextBlock
    {
        /// <summary>
        /// Custom event to give the integer as sender to the subscribed event
        /// </summary>
        /// <param name="sender">Interger which (as hyperlink) was clicked</param>
        /// <param name="e">EventArgs of the event</param>
        public delegate void IntAsSenderHandler(int sender, EventArgs e);

        /// <summary>
        /// Property of the item source
        /// </summary>
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register(nameof(ItemSource), typeof(List<int>),
                typeof(IntListHyperlinkTextBlock), new FrameworkPropertyMetadata(new List<int>(),
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    UpdateHyperlinks));

        /// <summary>
        /// Variable with itself in it, to use it in static content
        /// </summary>
        private static IntListHyperlinkTextBlock _self;

        /// <summary>
        /// Constructor for IntListHyperlinkTextBlock
        /// </summary>
        public IntListHyperlinkTextBlock()
        {
            _self = this;
        }

        /// <summary>
        /// Gets or sets the value of the item source.
        /// </summary>
        public List<int> ItemSource
        {
            get => (List<int>) GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }

        /// <summary>
        /// Event which is triggered when one of the hyperlinks is clicked
        /// </summary>
        public event IntAsSenderHandler HyperlinkClickedEvent;


        /// <summary>
        /// Method which is invoked trough the dependency
        /// </summary>
        /// <param name="dependencyObject">This contains the IntListHyperlinkTextBlock ("this")</param>
        /// <param name="dependencyPropertyChangedEventArgs">The event which "triggered" the method</param>
        private static void UpdateHyperlinks(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ApplyHyperLinks(dependencyObject as IntListHyperlinkTextBlock);
        }

        /// <summary>
        /// Base method to apply to apply the hyperlinks to the list in the TextBlock
        /// </summary>
        /// <param name="tb">This usercontrol, contains the item source</param>
        private static void ApplyHyperLinks(IntListHyperlinkTextBlock tb)
        {
            tb.Inlines.Clear();
            if (tb.ItemSource == null) return;
            for (var index = 0; index < tb.ItemSource.Count; index++)
            {
                var item = tb.ItemSource[index];
                var hyperlink = new Hyperlink(new Run(item.ToString())) {Tag = item};
                hyperlink.Click += HyperlinkOnClick;
                tb.Inlines.Add(hyperlink);
                if (index < tb.ItemSource.Count - 1)
                    tb.Inlines.Add(new Run(", "));
            }
        }

        /// <summary>
        /// Event which is triggered when one of the hyperlinks is clicked
        /// </summary>
        /// <param name="sender">Hyperlink which was clicked</param>
        /// <param name="routedEventArgs">The event which "triggered" the method</param>
        private static void HyperlinkOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            _self.HyperlinkOnClick(sender);
        }

        /// <summary>
        /// Event which is triggered when one of the hyperlinks is clicked
        /// </summary>
        /// <param name="sender">Hyperlink which was clicked</param>
        private void HyperlinkOnClick(object sender)
        {
            var tb = sender as Hyperlink;
            if (int.TryParse(tb?.Tag.ToString(), out var articleNumber))
                HyperlinkClickedEvent?.Invoke(articleNumber, EventArgs.Empty);
        }
    }
}