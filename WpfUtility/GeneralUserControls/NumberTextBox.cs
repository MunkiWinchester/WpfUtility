using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfUtility.GeneralUserControls
{
    /// <inheritdoc />
    /// <summary>
    /// Text box for only numbers
    /// </summary>
    public class NumberTextBox : TextBox
    {
        private static NumberTextBox _this;

        /// <summary>
        /// Maximum value that can be entered
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(nameof(Maximum), typeof(int),
                typeof(NumberTextBox), new FrameworkPropertyMetadata(int.MaxValue, PropertyChangedCallback));

        /// <summary>
        /// Minimum value that can be entered
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(nameof(Minimum), typeof(int),
                typeof(NumberTextBox), new FrameworkPropertyMetadata(0, PropertyChangedCallback));

        /// <inheritdoc />
        /// <summary>
        /// Default constructor
        /// </summary>
        public NumberTextBox()
        {
            _this = this;
            TextChanged += OnTextChanged;
            KeyDown += OnKeyDown;
        }

        /// <summary>
        /// Text of the TextBlock
        /// </summary>
        public new string Text
        {
            get => base.Text;
            set => base.Text = LeaveOnlyNumbers(value);
        }

        /// <summary>
        /// The maximum number that can be entered
        /// </summary>
        public int Maximum
        {
            get => (int) GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        /// <summary>
        /// The minimum number that can be entered
        /// </summary>
        public int Minimum
        {
            get => (int) GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        /// <summary>
        /// Check if the input is a number
        /// </summary>
        /// <param name="inKey">Pressed Key</param>
        /// <returns>True or false</returns>
        private static bool IsNumberKey(Key inKey)
        {
            if (inKey < Key.D0 || inKey > Key.D9)
                if (inKey < Key.NumPad0 || inKey > Key.NumPad9)
                    return false;
            return true;
        }

        /// <summary>
        /// Method which is invoked trough the dependency
        /// </summary>
        /// <param name="dependencyObject">This contains the NumberTextBox ("this")</param>
        /// <param name="dependencyPropertyChangedEventArgs">The event which "triggered" the method</param>
        private static void PropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            _this.Text = _this.LeaveOnlyNumbers(_this.Text);
        }

        /// <summary>
        /// Check if the input is a "delete commad"
        /// </summary>
        /// <param name="inKey">Pressed Key</param>
        /// <returns>True or false</returns>
        private static bool IsDelBackspaceOrEnterKey(Key inKey)
        {
            return inKey == Key.Delete || inKey == Key.Back || inKey == Key.Enter;
        }

        /// <summary>
        /// Remove every non numeric value
        /// </summary>
        /// <param name="inString">Entered string</param>
        /// <returns>Purged string</returns>
        private string LeaveOnlyNumbers(string inString)
        {
            var tmp = inString;
            foreach (var c in inString)
                if (!Regex.IsMatch(c.ToString(), "^[0-9]*$"))
                    tmp = tmp.Replace(c.ToString(), "");
            if (int.TryParse(tmp, out var number))
            {
                if (number > Maximum)
                    return Maximum.ToString();
                if (number < Minimum)
                    return Minimum.ToString();
            }

            return tmp;
        }

        /// <summary>
        /// Event which is triggered when a key is pressed down
        /// </summary>
        /// <param name="sender">Which control triggered this event</param>
        /// <param name="e">Which key triggered this event</param>
        protected void OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !IsNumberKey(e.Key) && !IsDelBackspaceOrEnterKey(e.Key);
        }

        /// <summary>
        /// Event which is triggered when the text is changed
        /// </summary>
        /// <param name="sender">Which control triggered this event</param>
        /// <param name="e"></param>
        protected void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            base.Text = LeaveOnlyNumbers(Text);
        }
    }
}