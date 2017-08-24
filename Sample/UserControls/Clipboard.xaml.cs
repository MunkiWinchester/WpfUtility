using System.ComponentModel;

namespace Sample.UserControls
{
    /// <summary>
    ///     Interaction logic for Clipboard.xaml
    /// </summary>
    public partial class Clipboard
    {
        public Clipboard()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
                DataContext = new ClipboardViewModel();
        }
    }
}