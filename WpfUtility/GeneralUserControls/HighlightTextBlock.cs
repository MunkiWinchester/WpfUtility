using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfUtility.GeneralUserControls
{
    public class HighlightTextBlock : TextBlock
    {
        public new static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string),
                typeof(HighlightTextBlock), new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    UpdateHighlighting));

        public static readonly DependencyProperty HighlightPhraseProperty =
            DependencyProperty.Register(nameof(HighlightPhrase), typeof(string),
                typeof(HighlightTextBlock), new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    UpdateHighlighting));

        public static readonly DependencyProperty HighlightBrushProperty =
            DependencyProperty.Register(nameof(HighlightBrush), typeof(Brush),
                typeof(HighlightTextBlock), new FrameworkPropertyMetadata(Brushes.Yellow,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    UpdateHighlighting));

        public static readonly DependencyProperty HighlightForeGroundProperty =
            DependencyProperty.Register(nameof(HighlightForeGround), typeof(Brush),
                typeof(HighlightTextBlock), new FrameworkPropertyMetadata(Brushes.Black,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    UpdateHighlighting));

        public static readonly DependencyProperty IsCaseSensitiveProperty =
            DependencyProperty.Register(nameof(IsCaseSensitive), typeof(bool),
                typeof(HighlightTextBlock), new FrameworkPropertyMetadata(false,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    UpdateHighlighting));

        /// <summary>
        ///     Text of the TextBlock
        /// </summary>
        public new string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        ///     Phrase which should be highlighted in the TextBlock
        /// </summary>
        public string HighlightPhrase
        {
            get => (string) GetValue(HighlightPhraseProperty);
            set => SetValue(HighlightPhraseProperty, value);
        }

        /// <summary>
        ///     Brush that is used to highlight the phrase
        /// </summary>
        public Brush HighlightBrush
        {
            get => (Brush) GetValue(HighlightBrushProperty);
            set => SetValue(HighlightBrushProperty, value);
        }

        /// <summary>
        ///     Brush that is used to highlight the phrase
        /// </summary>
        public Brush HighlightForeGround
        {
            get => (Brush) GetValue(HighlightForeGroundProperty);
            set => SetValue(HighlightForeGroundProperty, value);
        }

        /// <summary>
        ///     If the highlighting is case sensitive
        /// </summary>
        public bool IsCaseSensitive
        {
            get => (bool) GetValue(IsCaseSensitiveProperty);
            set => SetValue(IsCaseSensitiveProperty, value);
        }

        /// <summary>
        ///     Method which is invoked trough the dependency
        /// </summary>
        /// <param name="d">Object, in our case this control :)</param>
        /// <param name="e">The event which "triggered" the method</param>
        private static void UpdateHighlighting(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PrepareHighlight(d as HighlightTextBlock);
        }

        /// <summary>
        ///     Base method to highlight the phrase in the TextBlock
        /// </summary>
        /// <param name="tb">This usercontrol, contains the phrase and the text</param>
        private static void PrepareHighlight(HighlightTextBlock tb)
        {
            var highlightPhrase = tb.HighlightPhrase;
            var text = tb.Text;

            // clear the inlines, we don't want to dupe sth.
            tb.Inlines.Clear();
            // nothing to highlight? take the text without highlights
            if (string.IsNullOrEmpty(highlightPhrase))
            {
                tb.Inlines.Add(text);
            }
            // sth. to highlight? we have to go deeper
            else
            {
                // find the index of the phrase
                var index = text.IndexOf(highlightPhrase,
                    tb.IsCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);

                //if phrase doesn't exist
                if (index < 0)
                    // nothing to highlight? take the text without highlights
                    tb.Inlines.Add(text);
                // else start to go deeper!
                else
                    ApplyHighlight(tb, 0);
            }
        }

        /// <summary>
        ///     Deeper method to highlight the phrase in the TextBlock (recursive)
        /// </summary>
        /// <param name="tb">This usercontrol, contains the phrase and the text</param>
        /// <param name="index">Index to indicate the start "point" in the text</param>
        private static void ApplyHighlight(HighlightTextBlock tb, int index)
        {
            var highlightPhrase = tb.HighlightPhrase;
            var text = tb.Text;

            // find (new) index of the highlight phrase
            var newIndex = text.IndexOf(highlightPhrase, index,
                tb.IsCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);

            //if highlightPhrase doesn't occurs after start of text
            if (newIndex > index)
            {
                // cut the text in to a fitting piece
                var insertText = text.Substring(index, newIndex - index);
                //add the text that exists before highlightPhrase, with no background highlighting
                tb.Inlines.Add(insertText);
            }

            //if highlightPhrase doesn't exist in text
            if (newIndex < 0)
            {
                var remainingText = text.Substring(index);
                //add text, with no background highlighting, to tb.Inlines
                tb.Inlines.Add(remainingText);
            }
            else
            {
                var insertText = text.Substring(newIndex, highlightPhrase.Length);
                //add the highlightPhrase, using substring to get the casing as it appears in text, with a background, to tb.Inlines
                tb.Inlines.Add(new Run(insertText)
                {
                    Background = tb.HighlightBrush,
                    Foreground = tb.HighlightForeGround
                });

                //move index to the end of the matched highlightPhrase
                newIndex += highlightPhrase.Length;

                //if the end of the matched highlightPhrase occurs before the end of text
                if (newIndex < text.Length)
                    // phrase could appear multiple times, so check again
                {
                    ApplyHighlight(tb, newIndex);
                }
                else
                {
                    var remainingText = text.Substring(newIndex);
                    //add the text that exists after highlightPhrase, with no background highlighting, to tb.Inlines
                    tb.Inlines.Add(remainingText);
                }
            }
        }
    }
}