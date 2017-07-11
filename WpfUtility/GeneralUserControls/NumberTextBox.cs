using System.Windows.Controls;
using System.Windows.Input;

namespace WpfUtility.GeneralUserControls
{
    public class NumberTextBox : TextBox
    {
        public NumberTextBox()
        {
            TextChanged += OnTextChanged;
            KeyDown += OnKeyDown;
        }

        public new string Text
        {
            get => base.Text;
            set => base.Text = LeaveOnlyNumbers(value);
        }

        /// <summary>
        /// Check if the input is a number
        /// </summary>
        /// <param name="inKey">Pressed Key</param>
        /// <returns>True or false</returns>
        private static bool IsNumberKey(Key inKey)
        {
            if (inKey < Key.D0 || inKey > Key.D9)
            {
                if (inKey < Key.NumPad0 || inKey > Key.NumPad9)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Check if the input is a "delete commad"
        /// </summary>
        /// <param name="inKey">Pressed Key</param>
        /// <returns>True or false</returns>
        private static bool IsDelOrBackspaceKey(Key inKey)
        {
            return inKey == Key.Delete || inKey == Key.Back;
        }

        /// <summary>
        /// Remove every non numeric value
        /// </summary>
        /// <param name="inString">Entered string</param>
        /// <returns>Purged string</returns>
        private static string LeaveOnlyNumbers(string inString)
        {
            var tmp = inString;
            foreach (var c in inString)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(c.ToString(), "^[0-9]*$"))
                {
                    tmp = tmp.Replace(c.ToString(), "");
                }
            }
            return tmp;
        }

        protected void OnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !IsNumberKey(e.Key) && !IsDelOrBackspaceKey(e.Key);
        }

        protected void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            base.Text = LeaveOnlyNumbers(Text);
        }
    }
}