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

        public Paging()
        {
            InitializeComponent();
        }

        public int CurrentPage
        {
            get => (int) GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        public int TotalPages
        {
            get => (int) GetValue(TotalPagesProperty);
            set => SetValue(TotalPagesProperty, value);
        }

        public int EntriesPerPage
        {
            get => (int) GetValue(EntriesPerPageProperty);
            set => SetValue(EntriesPerPageProperty, value);
        }

        public int TotalEntries
        {
            get => (int) GetValue(TotalEntriesProperty);
            set => SetValue(TotalEntriesProperty, value);
        }

        public ICommand GoToFirstPageCommand
        {
            get => (ICommand) GetValue(GoToFirstPageCommandProperty);
            set => SetValue(GoToFirstPageCommandProperty, value);
        }

        public ICommand GoToPreviousPageCommand
        {
            get => (ICommand) GetValue(GoToPreviousPageCommandProperty);
            set => SetValue(GoToPreviousPageCommandProperty, value);
        }

        public ICommand GoToNextPageCommand
        {
            get => (ICommand) GetValue(GoToNextPageCommandProperty);
            set => SetValue(GoToNextPageCommandProperty, value);
        }

        public ICommand GoToLastPageCommand
        {
            get => (ICommand) GetValue(GoToLastPageCommandProperty);
            set => SetValue(GoToLastPageCommandProperty, value);
        }

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