using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace WpfUtility.GeneralUserControls
{
    /// <summary>
    ///     Interaction logic for Paging.xaml
    /// </summary>
    public partial class Paging
    {
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(nameof(CurrentPage), typeof(int),
                typeof(Paging), new FrameworkPropertyMetadata(1));

        public static readonly DependencyProperty TotalPagesProperty =
            DependencyProperty.Register(nameof(TotalPages), typeof(int),
                typeof(Paging), new FrameworkPropertyMetadata(1));

        public static readonly DependencyProperty EntriesPerPageProperty =
            DependencyProperty.Register(nameof(EntriesPerPage), typeof(int),
                typeof(Paging), new FrameworkPropertyMetadata(100));

        public static readonly DependencyProperty TotalEntriesProperty =
            DependencyProperty.Register(nameof(TotalEntries), typeof(int),
                typeof(Paging), new FrameworkPropertyMetadata(0));

        public static DependencyProperty GoToFirstPageCommandProperty
            = DependencyProperty.Register(
                nameof(GoToFirstPageCommand),
                typeof(ICommand),
                typeof(Paging));

        public static DependencyProperty GoToPreviousPageCommandProperty
            = DependencyProperty.Register(
                nameof(GoToPreviousPageCommand),
                typeof(ICommand),
                typeof(Paging));

        public static DependencyProperty GoToNextPageCommandProperty
            = DependencyProperty.Register(
                nameof(GoToNextPageCommand),
                typeof(ICommand),
                typeof(Paging));

        public static DependencyProperty GoToLastPageCommandProperty
            = DependencyProperty.Register(
                nameof(GoToLastPageCommand),
                typeof(ICommand),
                typeof(Paging));

        /// <summary>
        ///     Constructor of the paging control
        /// </summary>
        public Paging()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Gets or sets the value of the current page
        /// </summary>
        public int CurrentPage
        {
            get => (int) GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        /// <summary>
        ///     Gets or sets the value of the total page
        /// </summary>
        public int TotalPages
        {
            get => (int) GetValue(TotalPagesProperty);
            set => SetValue(TotalPagesProperty, value);
        }

        /// <summary>
        ///     Gets or sets the value of the entries per page
        /// </summary>
        public int EntriesPerPage
        {
            get => (int) GetValue(EntriesPerPageProperty);
            set => SetValue(EntriesPerPageProperty, value);
        }

        /// <summary>
        ///     Gets or sets the value of the total entries
        /// </summary>
        public int TotalEntries
        {
            get => (int) GetValue(TotalEntriesProperty);
            set => SetValue(TotalEntriesProperty, value);
        }

        /// <summary>
        ///     Gets or sets the command of the GoToFirstPageCommand
        /// </summary>
        public ICommand GoToFirstPageCommand
        {
            get => (ICommand) GetValue(GoToFirstPageCommandProperty);
            set => SetValue(GoToFirstPageCommandProperty, value);
        }

        /// <summary>
        ///     Gets or sets the command of the GoToPreviousPageCommand
        /// </summary>
        public ICommand GoToPreviousPageCommand
        {
            get => (ICommand) GetValue(GoToPreviousPageCommandProperty);
            set => SetValue(GoToPreviousPageCommandProperty, value);
        }

        /// <summary>
        ///     Gets or sets the command of the GoToNextPageCommand
        /// </summary>
        public ICommand GoToNextPageCommand
        {
            get => (ICommand) GetValue(GoToNextPageCommandProperty);
            set => SetValue(GoToNextPageCommandProperty, value);
        }

        /// <summary>
        ///     Gets or sets the command of the GoToLastPageCommand
        /// </summary>
        public ICommand GoToLastPageCommand
        {
            get => (ICommand) GetValue(GoToLastPageCommandProperty);
            set => SetValue(GoToLastPageCommandProperty, value);
        }

        /// <summary>
        ///     Event which is triggered when the key is pressed down (just Enter)
        /// </summary>
        /// <param name="sender">Which control triggered the event</param>
        /// <param name="e">Which key triggered this event</param>
        private void NumberTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var tBox = (NumberTextBox) sender;
                var prop = TextBox.TextProperty;

                var binding = BindingOperations.GetBindingExpression(tBox, prop);
                binding?.UpdateSource();
            }
        }
    }
}