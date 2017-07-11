using System.ComponentModel;

namespace Sample.UserControls
{
    /// <summary>
    /// Interaction logic for LogViewer.xaml
    /// </summary>
    public partial class LogViewer
    {
        public LogViewer()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
                DataContext = new LogViewerViewModel();
        }
    }
}
